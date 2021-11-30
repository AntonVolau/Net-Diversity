using MultyLib;
using System.Windows;

namespace NET_Diversity_WPF_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Label2.Content = Greetings.Hello(TextBox2.Text);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = string.Concat("Hello ", TextBox1.Text, '!');
        }
    }
}
