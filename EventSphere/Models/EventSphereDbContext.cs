using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EventSphere.ViewModels;

namespace EventSphere.Models;

public partial class EventSphereDbContext : DbContext
{
    public EventSphereDbContext() { }

    public EventSphereDbContext(DbContextOptions<EventSphereDbContext> options)
        : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
    public virtual DbSet<EventStatus> EventStatuses { get; set; }
    public virtual DbSet<EventType> EventTypes { get; set; }
    public virtual DbSet<Feedback> Feedbacks { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Organization> Organizations { get; set; }
    public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; }
    public virtual DbSet<OrganizerContact> OrganizerContacts { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Seat> Seats { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
    public virtual DbSet<User> Users { get; set; }

    // DTO'lar için DbSet tanımı
    public virtual DbSet<UserDto> UserDtos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=EFE\\EFE;Database=EventSphere;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDto>().HasNoKey().ToView(null);

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1B823CE993");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C8706713DB7E");

            entity.Property(e => e.IsPublic).HasDefaultValue(true);

            entity.HasOne(d => d.EventStatus).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Events__EventSta__534D60F1");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Events__EventTyp__52593CB8");

            entity.HasOne(d => d.Organization).WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Events__Organiza__5165187F");

            entity.HasOne(d => d.OrganizerUser).WithMany(p => p.Events).HasConstraintName("FK__Events__Organize__5441852A");
        });

        modelBuilder.Entity<EventRegistration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__EventReg__6EF588303E22CF9A");

            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Event).WithMany(p => p.EventRegistrations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventRegi__Event__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.EventRegistrations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventRegi__UserI__59063A47");
        });

        modelBuilder.Entity<EventStatus>(entity =>
        {
            entity.HasKey(e => e.EventStatusId).HasName("PK__EventSta__2EB6674C162A1E5C");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__A9216B1F6462BD44");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6F55B6DC1");

            entity.Property(e => e.SubmissionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Event).WithMany(p => p.Feedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__EventI__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks).HasConstraintName("FK__Feedback__UserID__628FA481");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E3268681D95");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__6754599E");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B729D81DED5");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrganizationMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Organiza__0CF04B38C8484154");

            entity.Property(e => e.JoinDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__Organ__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__UserI__4CA06362");
        });

        modelBuilder.Entity<OrganizerContact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Organize__5C6625BB19D7B601");

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizerContacts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizer__Organ__6E01572D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A34CC6A20");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Seats__311713D3C1F00514");

            entity.HasOne(d => d.Event).WithMany(p => p.Seats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seats__EventID__70DDC3D8");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D1E75B2774");

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__AssignedU__5DCAEF64");

            entity.HasOne(d => d.Event).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__EventID__5CD6CB2B");

            entity.HasOne(d => d.TaskStatus).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__TaskStatu__5EBF139D");
        });

        modelBuilder.Entity<TaskStatus>(entity =>
        {
            entity.HasKey(e => e.TaskStatusId).HasName("PK__TaskStat__C023DD0C8FBFFF46");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE1DB857C");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}