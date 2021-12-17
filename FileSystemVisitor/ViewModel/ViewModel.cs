using FileSystemVisitor.Commands;
using FileSystemVisitor.Models;
using FileSystemVisitor.Notifications;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FileSystemVisitor.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void Log(string message, TimeSpan timeSpan);
        public static event Log DoLog;
        
        // readonly ResourceDictionary _iconDictionary = Application.LoadComponent(new Uri("/FileSystemVisitor", UriKind.RelativeOrAbsolute)) as ResourceDictionary;

        public string CurrentDirectory { get; set; }
        public string NextDirectory { get; set; }
        public string PreviousDirectory { get; set; }
        public string ParentDirectory { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<FileDetailsModel> FavouriteFolders { get; set; }
        public ObservableCollection<FileDetailsModel> NavigatedFolderFiles { get; set; }
        public ObservableCollection<FileDetailsModel> ConnectedDevices { get; set; }
        public ObservableCollection<SubMenuItemDetails> HomeTabSubMenuCollection { get; set; }
        public ObservableCollection<SubMenuItemDetails> ViewHomeTabSubMenuCollection { get; set; }
        public ObservableCollection<string> PathHistoryCollection { get; set; }
        private ObservableCollection<LogEntry> _logEntries;
        public ObservableCollection<LogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = new ObservableCollection<LogEntry>();
                OnPropertyChanged(nameof(LogEntries));
            }
        }
        internal int position = 0;
        public bool CanGoBack { get; set; }
        public bool CanGoForward { get; set; }
        public bool IsAtRoot { get; set; }
        internal bool _pathDisrupted;
        private string _dateFrom;

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }


        private string _logs;
        public string Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                OnPropertyChanged(nameof(Logs));
            }
        }
        public string DateFrom
        {
            get => _dateFrom;

            set
            {
                this._dateFrom = value;
                this.OnPropertyChanged(nameof(DateFrom));
            }
        }

        private string _dateTo;
        public string DateTo
        {
            get { return this._dateTo; }

            set
            {
                this._dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }
        public bool PathDisrupted
        {
            get => _pathDisrupted;
            set
            {
                _pathDisrupted = value;
                if (_pathDisrupted)
                {
                    var tempCollection = new ObservableCollection<string>();
                    for (int i = position; i < PathHistoryCollection.Count - 1; i++)
                    {
                        tempCollection.Add(PathHistoryCollection[i]);
                    }

                    foreach (var path in tempCollection)
                    {
                        PathHistoryCollection.Remove(path);
                    }
                    OnPropertyChanged(nameof(PathHistoryCollection));
                    _pathDisrupted = false;
                }
            }
        }
        internal ReadOnlyCollection<string> tempFolderCollection;

        BackgroundWorker bgGetFilesBackgroundWorker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        internal static string GetCreatedOn(string path)
        {
            try
            {
                if (FileSystem.DirectoryExists(path))
                {
                    return $"{FileSystem.GetDirectoryInfo(path).CreationTime.ToShortDateString()} {FileSystem.GetDirectoryInfo(path).CreationTime.ToShortTimeString()}";
                }
                return $"{FileSystem.GetFileInfo(path).CreationTime.ToShortDateString()} {FileSystem.GetFileInfo(path).CreationTime.ToShortTimeString()}";
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string GetModifiedOn(string path)
        {
            try
            {
                if (FileSystem.DirectoryExists(path))
                {
                    return $"{FileSystem.GetDirectoryInfo(path).LastWriteTime.ToShortDateString()} {FileSystem.GetDirectoryInfo(path).LastWriteTime.ToShortTimeString()}";
                }
                return $"{FileSystem.GetFileInfo(path).LastWriteTime.ToShortDateString()} {FileSystem.GetFileInfo(path).LastWriteTime.ToShortTimeString()}";
            }
            catch
            {
                return string.Empty;
            }
        }

        internal bool IsFileHidden(string fileName)
        {
            var attr = FileAttributes.Normal;
            try
            {
                attr = File.GetAttributes(fileName);
            }
            catch
            {
                throw;
            }

            return attr.HasFlag(FileAttributes.Hidden);
        }

        internal bool IsFileReadonly(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    return (FileSystem.GetDirectoryInfo(path).Attributes & FileAttributes.ReadOnly) != 0;
                return (FileSystem.GetFileInfo(path).Attributes & FileAttributes.ReadOnly) != 0;
            }
            catch (UnauthorizedAccessException ex)
            {
                return false;
            }
            catch (FileNotFoundException ex)
            {
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                return false;
            }
        }

        internal string GetFileExtension(string fileName)
        {
            if (fileName == null) return string.Empty;
            var extension = Path.GetExtension(fileName);
            var CultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = CultureInfo.TextInfo;
            var data = textInfo.ToTitleCase(extension.Replace(".", string.Empty));
            return data;
        }

        internal bool IsDirectory(string fileName)
        {
            var attr = FileAttributes.Normal;
            try
            {
                attr = File.GetAttributes(fileName);
            }
            catch
            {
                throw;
            }

            return attr.HasFlag(FileAttributes.Directory);
        }

        internal static readonly List<string> ImageExtensions = new List<string>()
        {
            ".jpg",
            ".jpeg",
            ".bmp",
            ".gif",
            ".png"
        };

        internal static readonly List<string> VideoExtensions = new List<string>()
        {
            ".mp4",
            ".m4v",
            ".mov",
            ".wmv",
            ".avi",
            ".avchd",
            ".f4v",
            ".swf",
            ".mkv",
            ".webm"
        };

        internal void LoadDirectory(FileDetailsModel fileDetailModel)
        {
            CanGoBack = position != 0;
            OnPropertyChanged(nameof(CanGoBack));
            NavigatedFolderFiles.Clear();
            tempFolderCollection = null;

            if (PathHistoryCollection != null && position > 0)
            {
                if (PathHistoryCollection.ElementAt(position) != fileDetailModel.Path)
                    PathDisrupted = true;
            }

            if (bgGetFilesBackgroundWorker.IsBusy)
                bgGetFilesBackgroundWorker.CancelAsync();

            bgGetFilesBackgroundWorker.RunWorkerAsync(fileDetailModel);
        }

        public ViewModel()
        {
            DoLog += AddLogMessage;
            LogEntries = new ObservableCollection<LogEntry>();
            ConnectedDevices = new ObservableCollection<FileDetailsModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                var name = string.IsNullOrEmpty(drive.VolumeLabel) ? "Локальный диск" : drive.VolumeLabel;
                ConnectedDevices.Add(new FileDetailsModel()
                {
                    Name = $"{name}({ drive.Name.Replace(@"\", "")})",
                    Path = drive.RootDirectory.FullName,
                    IsDirectory = true,
                });
            }

            HomeTabSubMenuCollection = new ObservableCollection<SubMenuItemDetails>();

            CurrentDirectory = @"C:\";


            OnPropertyChanged(nameof(CurrentDirectory));

            NavigatedFolderFiles = new ObservableCollection<FileDetailsModel>();

            bgGetFilesBackgroundWorker.DoWork += bgGetFilesBackgroundWorker_DoWork;
            bgGetFilesBackgroundWorker.ProgressChanged += bgGetFilesBackgroundWorker_ProgressChanged;
            bgGetFilesBackgroundWorker.RunWorkerCompleted += bgGetFilesBackgroundWorker_RunWorkerCompleted;

            LoadDirectory(new FileDetailsModel()
            {
                Path = CurrentDirectory
            });

            PathHistoryCollection = new ObservableCollection<string>();
            PathHistoryCollection.Add(CurrentDirectory);
            DateFrom = string.Empty;
            DateTo = string.Empty;
            CanGoBack = position != 0;
            OnPropertyChanged(nameof(CanGoBack));
        }

        private void bgGetFilesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void bgGetFilesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var fileOrFolder = (FileDetailsModel)e.Argument;

                tempFolderCollection = new ReadOnlyCollectionBuilder<string>(FileSystem.GetDirectories(fileOrFolder.Path)
                    .Concat(FileSystem.GetFiles(fileOrFolder.Path))).ToReadOnlyCollection();

                foreach (var filename in tempFolderCollection)
                {
                    bgGetFilesBackgroundWorker.ReportProgress(1, filename);
                }
                CurrentDirectory = fileOrFolder.Path;
                OnPropertyChanged(nameof(CurrentDirectory));

                var root = Path.GetPathRoot(fileOrFolder.Path);
                if (string.IsNullOrEmpty(CurrentDirectory) || CurrentDirectory == root)
                {
                    IsAtRoot = true;
                    OnPropertyChanged(nameof(IsAtRoot));
                }
                else
                {
                    IsAtRoot = false;
                    OnPropertyChanged(nameof(IsAtRoot));
                }
            }
            catch
            {
                DoLog?.Invoke("Access denied!", TimeSpan.Zero);
            }
        }

        private void bgGetFilesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var fileName = e.UserState.ToString();
            var file = new FileDetailsModel();
            file.Name = Path.GetFileName(fileName);
            file.FileExtension = GetFileExtension(fileName);
            file.Path = fileName;
            file.IsHidden = IsFileHidden(fileName);
            file.IsReadOnly = IsFileReadonly(fileName);
            file.IsDirectory = IsDirectory(fileName);
            file.IsImage = ImageExtensions.Contains(file.FileExtension.ToLower());
            file.IsVideo = VideoExtensions.Contains(file.FileExtension.ToLower());
            file.ModifiedOn = GetModifiedOn(file.Path);

            NavigatedFolderFiles.Add(file);
            OnPropertyChanged(nameof(NavigatedFolderFiles));
        }


        private ICommand _loadSubMenuCollectionsCommand;

        public ICommand LoadSubMenuCollectionsCommand => _loadSubMenuCollectionsCommand ??
                        (
                        _loadSubMenuCollectionsCommand = new Command(() =>
                        {
                            HomeTabSubMenuCollection = new ObservableCollection<SubMenuItemDetails>
                            {
                                new SubMenuItemDetails()
                                {
                                    Name = "Pin"
                                }
                            };
                        }));
        protected ICommand _getFileListCommand;

        internal void UpdatePathHistory(string path)
        {
            if (PathHistoryCollection != null && !string.IsNullOrEmpty(path))
            {
                PathHistoryCollection.Add(path);
                position++;
                OnPropertyChanged(nameof(PathHistoryCollection));
            }
        }

        public ICommand GetFileListCommand =>
            _getFileListCommand ?? (_getFileListCommand = new RelayCommand(parameter =>
            {
                var file = parameter as FileDetailsModel;
                if (file == null) return;

                if (Directory.Exists(file.Path))
                {
                    UpdatePathHistory(file.Path);
                    LoadDirectory(file);
                }
                else
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(file.Path));
                    }
                    catch (Win32Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.Source);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, ex.Source);
                    }
                }
            }));

        public ICommand _goToPrevousDirectoryCommand;

        public ICommand GoToPreviousDirectoryCommand => _goToPrevousDirectoryCommand ??
            (_goToPrevousDirectoryCommand = new Command(() =>
        {
            if (position >= 1)
            {
                position--;
                LoadDirectory(new FileDetailsModel()
                {
                    Path = PathHistoryCollection.ElementAt(position)
                });
            }
            CanGoForward = true;
            OnPropertyChanged(nameof(CanGoForward));

            PathDisrupted = false;
            OnPropertyChanged(nameof(PathDisrupted));
        }));

        public ICommand _goToNextDirectoryCommand;

        public ICommand GoToNextDirectoryCommand => _goToNextDirectoryCommand ??
            (_goToNextDirectoryCommand = new Command(() =>
            {
                if (position < PathHistoryCollection.Count - 1)
                {
                    position++;
                    LoadDirectory(new FileDetailsModel()
                    {
                        Path = PathHistoryCollection.ElementAt(position)
                    });
                }
                CanGoForward = position < PathHistoryCollection.Count - 1 && position != PathHistoryCollection.Count - 1;
                OnPropertyChanged(nameof(CanGoForward));
            }));

        internal ICommand _goToParentDirectoryCommand;

        public ICommand GoToParentDirectoryCommand => _goToParentDirectoryCommand ??
            (_goToParentDirectoryCommand = new Command(() =>
            {
                var ParentDirectory = string.Empty;
                PathDisrupted = true;

                var d = new DirectoryInfo(CurrentDirectory);

                if (d.Parent != null)
                {
                    ParentDirectory = d.Parent.FullName;
                    IsAtRoot = false;
                    OnPropertyChanged(nameof(IsAtRoot));
                }
                else if (d.Parent == null)
                {
                    IsAtRoot = true;
                    OnPropertyChanged(nameof(IsAtRoot));
                    return;
                }
                else
                {
                    ParentDirectory = d.Root.ToString().Split(Path.DirectorySeparatorChar)[1];
                }

                GetFileListCommand.Execute(new FileDetailsModel() { Path = ParentDirectory });
            }));

        internal ICommand _searchFilesByDate;

        public ICommand SearchFilesByDate => _searchFilesByDate ??
            (_searchFilesByDate = new Command(() =>
            {
                try
                {
                    var timeStart = DateTime.UtcNow;
                    var kektime = DateTime.UtcNow;
                    var zvezdec = timeStart - kektime;
                    RaiseCanExecuteChanged();
                    DoLog?.Invoke("Search started!", DateTime.UtcNow - timeStart);
                    DateTime fromDate = new DateTime(1950, 7, 15);
                    DateTime toDate = DateTime.Now;
                    DirectoryInfo info = new DirectoryInfo(CurrentDirectory);
                    if (!string.IsNullOrWhiteSpace(DateFrom))
                    {
                        fromDate = Convert.ToDateTime(DateFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(DateTo))
                    {
                        toDate = Convert.ToDateTime(DateTo);
                    }
                    var allFiles = info.GetFiles().Where(p => p.LastWriteTime.Date <= toDate.Date && p.LastWriteTime >= fromDate.Date);
                    var allFolders = info.GetDirectories().Where(p => p.LastWriteTime.Date <= toDate.Date && p.LastWriteTime >= fromDate.Date);
                    var allLines = allFolders.Cast<FileSystemInfo>().Concat(allFiles);
                    NavigatedFolderFiles.Clear();

                    foreach (var fileName in allLines)
                    {
                        DoLog?.Invoke($"Found {fileName.Name}!", DateTime.UtcNow - timeStart);
                        var file = new FileDetailsModel();
                        file.Name = Path.GetFileName(fileName.FullName);
                        file.FileExtension = GetFileExtension(fileName.FullName);
                        file.Path = fileName.FullName;
                        file.IsHidden = IsFileHidden(fileName.FullName);
                        file.IsReadOnly = IsFileReadonly(fileName.FullName);
                        file.IsDirectory = IsDirectory(fileName.FullName);
                        file.IsImage = ImageExtensions.Contains(file.FileExtension.ToLower());
                        file.IsVideo = VideoExtensions.Contains(file.FileExtension.ToLower());
                        file.ModifiedOn = GetModifiedOn(file.Path);
                        NavigatedFolderFiles.Add(file);
                        Thread.Sleep(TimeSpan.FromSeconds(1.0));
                        OnPropertyChanged(nameof(NavigatedFolderFiles));
                    }
                    DoLog?.Invoke("Search completed!", DateTime.UtcNow - timeStart);
                }
                catch
                {
                    DoLog?.Invoke("Search Aborted!", TimeSpan.Zero);
                }
            }));

        protected ICommand _searchFilesCommand;

        internal BackgroundWorker bgGetFoundFilesWorker = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
        internal static IEnumerable<string> EnumerateDirectories(string parentDirectory, string searchPattern, System.IO.SearchOption searchOption)
        {
            try
            {
                var directories = Enumerable.Empty<string>();
                if (searchOption == System.IO.SearchOption.AllDirectories)
                    directories = Directory.EnumerateDirectories(parentDirectory)
                        .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOption));
                return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern));
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }
        internal static IEnumerable<string> EnumerateFiles(string path, string searchPattern, System.IO.SearchOption searchOption)
        {
            try
            {
                var dirFiles = Enumerable.Empty<string>();
                if (searchOption == System.IO.SearchOption.AllDirectories)
                    dirFiles = Directory.EnumerateDirectories(path)
                        .SelectMany(x => EnumerateFiles(x, searchPattern, searchOption));
                return dirFiles.Concat(Directory.EnumerateFiles(path, searchPattern));
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        public ICommand SearchFilesCommand => _searchFilesCommand ??
            (_searchFilesCommand = new RelayCommand((parameter) =>
                {
                    var searchQuery = "test1";
                    if (string.IsNullOrWhiteSpace(searchQuery)) return;

                    NavigatedFolderFiles.Clear();
                    if (bgGetFilesBackgroundWorker != null && bgGetFilesBackgroundWorker.IsBusy)
                        bgGetFilesBackgroundWorker.CancelAsync();

                    bgGetFilesBackgroundWorker.DoWork += (o, args) =>
                      {
                          try
                          {
                              var directories = EnumerateDirectories(CurrentDirectory, searchQuery, System.IO.SearchOption.AllDirectories);
                              foreach (var directory in directories)
                              {
                                  bgGetFilesBackgroundWorker.ReportProgress(1, directory);
                                  //Task.Delay(TimeSpan.FromSeconds(1.0));
                              }
                              var files = EnumerateFiles(CurrentDirectory, searchQuery, System.IO.SearchOption.AllDirectories);
                              foreach (var file in files)
                              {
                                  bgGetFilesBackgroundWorker.ReportProgress(1, file);
                              }

                              bgGetFilesBackgroundWorker.ProgressChanged += bgGetFilesBackgroundWorker_ProgressChanged;
                              bgGetFilesBackgroundWorker.RunWorkerCompleted += bgGetFilesBackgroundWorker_RunWorkerCompleted;
                              if (!bgGetFilesBackgroundWorker.IsBusy)
                                  bgGetFilesBackgroundWorker.RunWorkerAsync();
                          }
                          catch
                          {
                              //ignore
                          }
                      };
                }));

        protected ICommand _cancellSearch;

        public ICommand CancellSearch => _cancellSearch ??
            (_cancellSearch = new Command(() =>
            {
                if (bgGetFilesBackgroundWorker.IsBusy)
                    bgGetFilesBackgroundWorker.CancelAsync();
                if (bgGetFilesBackgroundWorker.CancellationPending)
                    bgGetFilesBackgroundWorker.Dispose();
            }));

        private void AddLogMessage(string message, TimeSpan timeSpan)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                LogEntries.Add(new LogEntry
                {
                    Timestamp = timeSpan.TotalSeconds.ToString(),
                    Message = message
                });
            });
        }
    }
}