using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using AvaloniaApplication1.Views;
using System;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        private ItemsControl _tarifsContainer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _tarifsContainer = this.FindControl<ItemsControl>("TarifsContainer");
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            LoadTarifs();
        }

        private void LoadTarifs()
        {
            var service = new TarifService();
            var tarifs = service.GetActiveTarifs();
            _tarifsContainer.ItemsSource = tarifs;
        }

        private void TarifCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is TarifViewModel tarif)
            {
                var detailsWindow = new Window4(tarif);
                detailsWindow.Show();
            }
        }

        private void Button_Click_auth(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
            Close();
        }
    }
}