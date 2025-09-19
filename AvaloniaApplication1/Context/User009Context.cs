using System;
using System.Collections.Generic;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1.Context;

public partial class User009Context : DbContext
{
    public User009Context()
    {
    }

    public User009Context(DbContextOptions<User009Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NumbTarif> NumbTarifs { get; set; }

    public virtual DbSet<Number> Numbers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tarif> Tarifs { get; set; }

    public virtual DbSet<Usage> Usages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=89.111.169.167;Port=5432;Database=user009;Username=user009;Password=User.009;SSL Mode=Disable;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.IsRead, "idx_notifications_read");

            entity.HasIndex(e => e.UserId, "idx_notifications_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FromAdminId).HasColumnName("from_admin_id");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.ReadAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("read_at");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.FromAdmin).WithMany(p => p.NotificationFromAdmins)
                .HasForeignKey(d => d.FromAdminId)
                .HasConstraintName("notifications_from_admin_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<NumbTarif>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("numb_tarifs_pkey");

            entity.ToTable("numb_tarifs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("activated_date");
            entity.Property(e => e.NumberId).HasColumnName("number_id");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TarifsId).HasColumnName("tarifs_id");

            entity.HasOne(d => d.Number).WithMany(p => p.NumbTarifs)
                .HasForeignKey(d => d.NumberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("numb_tarifs_number_id_fkey");

            entity.HasOne(d => d.Tarifs).WithMany(p => p.NumbTarifs)
                .HasForeignKey(d => d.TarifsId)
                .HasConstraintName("numb_tarifs_tarifs_id_fkey");
        });

        modelBuilder.Entity<Number>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("numbers_pkey");

            entity.ToTable("numbers");

            entity.HasIndex(e => e.Phone, "numbers_phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balanse)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("balanse");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasDefaultValueSql("'active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Numbers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("numbers_user_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.NumberId).HasColumnName("number_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasDefaultValueSql("'success'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Number).WithMany(p => p.Payments)
                .HasForeignKey(d => d.NumberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("payments_number_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tarif>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tarifs_pkey");

            entity.ToTable("tarifs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.InternetIncuded).HasColumnName("internet_incuded");
            entity.Property(e => e.IsArhived)
                .HasDefaultValue(false)
                .HasColumnName("is_arhived");
            entity.Property(e => e.MinIncuded).HasColumnName("min_incuded");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PricePerMonth)
                .HasPrecision(10, 2)
                .HasColumnName("price_per_month");
            entity.Property(e => e.SmsIncuded).HasColumnName("sms_incuded");
        });

        modelBuilder.Entity<Usage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usages_pkey");

            entity.ToTable("usages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.NumberId).HasColumnName("number_id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasOne(d => d.Number).WithMany(p => p.Usages)
                .HasForeignKey(d => d.NumberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usages_number_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "users_phone_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.UserLastName)
                .HasMaxLength(30)
                .HasColumnName("user_last_name");
            entity.Property(e => e.UserMidName)
                .HasMaxLength(30)
                .HasColumnName("user_mid_name");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .HasColumnName("user_name");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("fk_role_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
