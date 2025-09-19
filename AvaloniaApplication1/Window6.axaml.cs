using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Context;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class Window6 : Window
    {
        private readonly User _user;
        private readonly UserService _userService;
        private readonly Window _parentWindow;

        public Window6(User user, Window parantwindow)
        {
            InitializeComponent();
            _user = user;
            _parentWindow = parantwindow;

            var context = new User009Context();
            _userService = new UserService(context);

            LoadUserData();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LoadUserData()
        {
            try
            {
                var userWithDetails = _userService.GetUserWithDetails(_user.Id);
                var profile = new ProfileViewModel(userWithDetails);

                DataContext = profile;
            }
            catch (System.Exception ex)
            {
                var fallbackProfile = new ProfileViewModel(_user)
                {
                    CurrentTarif = "Ошибка загрузки",
                    TarifPrice = "0₽/мес",
                    TarifDescription = "Не удалось загрузить информацию о тарифе",
                    Balance = 0
                };
                DataContext = fallbackProfile;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            _parentWindow?.Close();
            Close();
        }
    }
}