using Avalonia.Controls;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Auth(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            new Window1().Show();
            Close();
        }
    }
}