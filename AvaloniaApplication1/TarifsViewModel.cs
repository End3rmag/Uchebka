namespace AvaloniaApplication1.Models
{
    public class TarifViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal PricePerMonth { get; set; }
        public string? Description { get; set; }
        public int? InternetIncluded { get; set; }
        public int? MinutesIncluded { get; set; }
        public int? SmsIncluded { get; set; }

        public string FormattedPrice => $"{PricePerMonth}₽/мес";
        public string FormattedInternet => InternetIncluded.HasValue ? $"{InternetIncluded} ГБ" : "Безлимит";
        public string FormattedMinutes => MinutesIncluded.HasValue ? $"{MinutesIncluded} мин" : "Безлимит";
        public string FormattedSms => SmsIncluded.HasValue ? $"{SmsIncluded} SMS" : "Безлимит";
    }
}