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

        private void TextBox3_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox3.Visibility = Visibility.Collapsed;
            TextBox1.Visibility = Visibility.Visible;
            TextBox1.Focus();
        }

        private void TextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                TextBox1.Visibility = Visibility.Collapsed;
                TextBox3.Visibility = Visibility.Visible;
            }
        }

        private void TextBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox2.Text))
            {
                TextBox2.Visibility = Visibility.Collapsed;
                TextBox4.Visibility = Visibility.Visible;
            }
        }

        private void TextBox4_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox4.Visibility = Visibility.Collapsed;
            TextBox2.Visibility = Visibility.Visible;
            TextBox2.Focus();
        }
    }
}
