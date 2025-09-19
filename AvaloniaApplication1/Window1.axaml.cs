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
    public partial class Window1 : Window
    {
        private TextBox _phoneTextBox;

        public Window1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _phoneTextBox = this.FindControl<TextBox>("PhoneTextBox");
        }

        private void Button_Click_1(object? sender, RoutedEventArgs e)
        {
            new Window3().Show();
            Close();
        }

        private void Button_Click_2(object? sender, RoutedEventArgs e)
        {
            new Window2().Show();
            Close();
        }

        private void Button_Click_Main(object? sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private async void LoginButton_Click(object? sender, RoutedEventArgs e)
        {
            var phoneNumber = _phoneTextBox.Text?.Trim();

            if (string.IsNullOrEmpty(phoneNumber))
            {
                ShowError("Введите номер телефона");
                return;
            }

            try
            {
                var authService = new AuthService();
                var user = authService.AuthenticateByMobileId(phoneNumber);

                if (user != null)
                {
                    var mainWindow = new Window7(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ShowError("Пользователь не найден");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка авторизации: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            var errorText = this.FindControl<TextBlock>("ErrorText");
            if (errorText != null)
            {
                errorText.Text = message;
                errorText.IsVisible = true;
            }
        }
    }
}