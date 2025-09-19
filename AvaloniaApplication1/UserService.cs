using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Context;

namespace AvaloniaApplication1.Services
{
    public class UserService
    {
        private readonly User009Context _context;

        public UserService(User009Context context)
        {
            _context = context;
        }

        public User GetUserWithDetails(int userId)
        {
            return _context.Users
                .Include(u => u.Numbers)
                    .ThenInclude(n => n.NumbTarifs)
                    .ThenInclude(nt => nt.Tarifs)
                .Include(u => u.Numbers)
                    .ThenInclude(n => n.Payments)
                .FirstOrDefault(u => u.Id == userId);
        }

        public decimal GetUserBalance(int userId)
        {
            var user = _context.Users
                .Include(u => u.Numbers)
                .FirstOrDefault(u => u.Id == userId);

            return user?.Numbers.FirstOrDefault()?.Balanse ?? 0;
        }

        public bool ConnectTarif(int userId, int tarifId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var user = _context.Users
                    .Include(u => u.Numbers)
                    .FirstOrDefault(u => u.Id == userId);

                var number = user?.Numbers.FirstOrDefault();
                if (number == null) return false;

                var tarif = _context.Tarifs.Find(tarifId);
                if (tarif == null) return false;

                if (number.Balanse < tarif.PricePerMonth)
                    return false;

                number.Balanse -= tarif.PricePerMonth;

                var activeTarif = _context.NumbTarifs
                    .FirstOrDefault(nt => nt.NumberId == number.Id && nt.Status == "active");

                if (activeTarif != null)
                    activeTarif.Status = "inactive";

                var newNumbTarif = new NumbTarif
                {
                    NumberId = number.Id,
                    TarifsId = tarif.Id,
                    ActivatedDate = DateTime.Now,
                    Status = "active"
                };

                _context.NumbTarifs.Add(newNumbTarif);

                var payment = new Payment
                {
                    NumberId = number.Id,
                    Amount = tarif.PricePerMonth,
                    Status = "success",
                    Date = DateTime.Now
                };

                _context.Payments.Add(payment);

                _context.SaveChanges();
                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}