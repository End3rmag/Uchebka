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
    public partial class Window5 : Window
    {
        public event EventHandler TarifConnected;
        private readonly User _user;
        private readonly UserService _userService;
        private TextBlock _balanceInfo;
        private TextBlock _errorText;

        // Конструктор без параметров
        public Window5()
        {
            InitializeComponent();
        }

        // Основной конструктор
        public Window5(TarifViewModel tarif, User user) : this()
        {
            _user = user;

            var context = new User009Context();
            _userService = new UserService(context);

            // Устанавливаем DataContext
            DataContext = tarif;

            InitializeComponents();
            UpdateBalanceInfo(tarif);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeComponents()
        {
            _balanceInfo = this.FindControl<TextBlock>("BalanceInfo");
            _errorText = this.FindControl<TextBlock>("ErrorText");
        }

        private void UpdateBalanceInfo(TarifViewModel tarif)
        {
            try
            {
                var userBalance = _userService.GetUserBalance(_user.Id);

                _balanceInfo.Text = $"Ваш баланс: {userBalance}₽";

                if (userBalance < tarif.PricePerMonth)
                {
                    _errorText.Text = $"Недостаточно средств. Нужно: {tarif.PricePerMonth}₽";
                    _errorText.Foreground = Avalonia.Media.Brushes.Red;
                }
                else
                {
                    _errorText.Text = "Достаточно средств для подключения";
                    _errorText.Foreground = Avalonia.Media.Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                _balanceInfo.Text = "Ошибка получения баланса";
                _errorText.Text = ex.Message;
                _errorText.Foreground = Avalonia.Media.Brushes.Red;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tarif = DataContext as TarifViewModel;
                if (tarif == null) return;

                var userBalance = _userService.GetUserBalance(_user.Id);

                if (userBalance < tarif.PricePerMonth)
                {
                    _errorText.Text = "Недостаточно средств для подключения";
                    _errorText.Foreground = Avalonia.Media.Brushes.Red;
                    return;
                }

                var success = _userService.ConnectTarif(_user.Id, tarif.Id);

                if (success)
                {
                    _errorText.Text = "Тариф успешно подключен!";
                    _errorText.Foreground = Avalonia.Media.Brushes.Green;

                    TarifConnected?.Invoke(this, EventArgs.Empty);
                    Close();
                }
                else
                {
                    _errorText.Text = "Ошибка при подключении тарифа";
                    _errorText.Foreground = Avalonia.Media.Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                _errorText.Text = $"Ошибка: {ex.Message}";
                _errorText.Foreground = Avalonia.Media.Brushes.Red;
            }
        }
    }
}