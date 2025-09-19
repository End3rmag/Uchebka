using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Usage
{
    public int Id { get; set; }

    public int? NumberId { get; set; }

    public string? Type { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }

    public virtual Number? Number { get; set; }
}
