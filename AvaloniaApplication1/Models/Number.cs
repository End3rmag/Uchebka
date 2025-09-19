using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Number
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Phone { get; set; } = null!;

    public decimal? Balanse { get; set; }

    public string? Status { get; set; }

    public int ContractId { get; set; }

    public virtual ICollection<NumbTarif> NumbTarifs { get; set; } = new List<NumbTarif>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Usage> Usages { get; set; } = new List<Usage>();

    public virtual User? User { get; set; }
}
