using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string UserName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string? UserMidName { get; set; }

    public int? Role { get; set; }

    public string? Login { get; set; }

    public virtual ICollection<Notification> NotificationFromAdmins { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Number> Numbers { get; set; } = new List<Number>();

    public virtual Role? RoleNavigation { get; set; }
}
