using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Context;

namespace AvaloniaApplication1.Services
{
    public class TarifService
    {
        public List<TarifViewModel> GetActiveTarifs()
        {
            try
            {
                using var context = new User009Context();

                return context.Tarifs
                    .Where(t => !t.IsArhived.HasValue || !t.IsArhived.Value)
                    .Select(t => new TarifViewModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                        PricePerMonth = t.PricePerMonth,
                        Description = t.Description,
                        InternetIncluded = t.InternetIncuded,
                        MinutesIncluded = t.MinIncuded,
                        SmsIncluded = t.SmsIncuded
                    })
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка БД: {ex.Message}");
                return new List<TarifViewModel>
                {
                    new TarifViewModel
                    {
                        Id = 1,
                        Name = "Базовый",
                        PricePerMonth = 300,
                        Description = "Базовый тарифный план",
                        InternetIncluded = 5,
                        MinutesIncluded = 100,
                        SmsIncluded = 50
                    },
                    new TarifViewModel
                    {
                        Id = 2,
                        Name = "Премиум",
                        PricePerMonth = 800,
                        Description = "Премиум тарифный план",
                        InternetIncluded = 20,
                        MinutesIncluded = 500,
                        SmsIncluded = 200
                    }
                };
            }
        }
    }
}