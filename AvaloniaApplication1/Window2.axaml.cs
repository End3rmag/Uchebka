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
    public partial class Window2 : Window
    {
        private TextBox _loginTextBox;
        private TextBox _passwordTextBox;

        public Window2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _loginTextBox = this.FindControl<TextBox>("LoginTextBox");
            _passwordTextBox = this.FindControl<TextBox>("PasswordTextBox");
        }

        private void Button_Click_Main(object? sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            new Window3().Show();
            Close();
        }

        private void Button_Click_1(object? sender, RoutedEventArgs e)
        {
            new Window1().Show();
            Close();
        }

        private async void LoginButton_Click(object? sender, RoutedEventArgs e)
        {
            var login = _loginTextBox.Text?.Trim();
            var password = _passwordTextBox.Text?.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ShowError("Заполните все поля");
                return;
            }

            try
            {
                var authService = new AuthService();
                var user = authService.AuthenticateByLogin(login, password);

                if (user != null)
                {
                    var mainWindow = new Window7(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ShowError("Неверный логин или пароль");
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