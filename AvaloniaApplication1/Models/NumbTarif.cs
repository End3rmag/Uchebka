using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class NumbTarif
{
    public int Id { get; set; }

    public int? NumberId { get; set; }

    public int? TarifsId { get; set; }

    public DateTime? ActivatedDate { get; set; }

    public string? Status { get; set; }

    public virtual Number? Number { get; set; }

    public virtual Tarif? Tarifs { get; set; }
}
