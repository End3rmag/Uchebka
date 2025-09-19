using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Tarif
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PricePerMonth { get; set; }

    public string? Description { get; set; }

    public bool? IsArhived { get; set; }

    public int? InternetIncuded { get; set; }

    public int? MinIncuded { get; set; }

    public int? SmsIncuded { get; set; }

    public virtual ICollection<NumbTarif> NumbTarifs { get; set; } = new List<NumbTarif>();
}
