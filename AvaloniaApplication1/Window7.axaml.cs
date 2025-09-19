using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class Window7 : Window
    {
        private ItemsControl _tarifsContainer;
        private readonly User _user;

        public Window7(User user)
        {
            InitializeComponent();
            _user = user;
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
                var connectWindow = new Window5(tarif, _user);
                connectWindow.TarifConnected += (s, args) =>
                {
                    LoadTarifs();
                };
                connectWindow.Show();
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new Window6(_user, this);
            profileWindow.Show();

        }
    }
}