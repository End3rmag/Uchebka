using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? FromAdminId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual User? FromAdmin { get; set; }

    public virtual User User { get; set; } = null!;
}
