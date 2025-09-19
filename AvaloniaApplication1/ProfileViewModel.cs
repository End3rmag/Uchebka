using System.Linq;

namespace AvaloniaApplication1.Models
{
    public class ProfileViewModel
    {
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string CurrentTarif { get; set; }
        public decimal Balance { get; set; }
        public string TarifDescription { get; set; }
        public string TarifPrice { get; set; }

        public ProfileViewModel(User user)
        {
            PhoneNumber = user.PhoneNumber;
            Login = user.Login ?? "Не указан";

            var activeNumber = user.Numbers.FirstOrDefault();
            if (activeNumber != null)
            {
                Balance = activeNumber.Balanse ?? 0;

                var activeTarif = activeNumber.NumbTarifs
                    .FirstOrDefault(nt => nt.Status == "active");

                if (activeTarif != null && activeTarif.Tarifs != null)
                {
                    CurrentTarif = activeTarif.Tarifs.Name;
                    TarifPrice = $"{activeTarif.Tarifs.PricePerMonth}₽/мес";
                    TarifDescription = activeTarif.Tarifs.Description;
                }
                else
                {
                    CurrentTarif = "Не подключен";
                    TarifPrice = "0₽/мес";
                    TarifDescription = "Тарифный план не активирован";
                }
            }
            else
            {
                Balance = 0;
                CurrentTarif = "Нет номера";
                TarifPrice = "0₽/мес";
                TarifDescription = "У вас нет активного номера";
            }
        }
    }
}