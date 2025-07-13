using DayOff.Persistence.Entities;
using DayOff.Persistence.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace DayOff.Persistence.Context;

public partial class DayOffDbContext : DbContext
{
    public DayOffDbContext()
    {
    }

    public DayOffDbContext(DbContextOptions<DayOffDbContext> options)
        : base(options)
    {
    }



    public virtual DbSet<Dy_DayOffBalance> DyDayOffBalances { get; set; }

    public virtual DbSet<Dy_DayOffHistory> DyDayOffHistories { get; set; }

    public virtual DbSet<Dy_DayOffPolicy> DyDayOffPolicies { get; set; }

    public virtual DbSet<Dy_DayOffRequest> DyDayOffRequests { get; set; }

    public virtual DbSet<Dy_DayOffType> DyDayOffTypes { get; set; }

    public virtual DbSet<Dy_Department> DyDepartments { get; set; }

    public virtual DbSet<Dy_Gender> DyGenders { get; set; }

    public virtual DbSet<Dy_Holiday> DyHolidays { get; set; }

    public virtual DbSet<Dy_Notification> DyNotifications { get; set; }

    public virtual DbSet<Dy_Role> DyRoles { get; set; }

    public virtual DbSet<Dy_Title> DyTitles { get; set; }

    public virtual DbSet<Dy_User> DyUsers { get; set; }

    public virtual DbSet<Vw_WeeklyDayOffStat> VwWeeklyDayOffStats { get; set; }
    public virtual DbSet<Vw_DayOffTypeWithGender> VwDayOffTypesWithGender { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseOracle("User Id=C##USER1;Password=123;Data Source=localhost:1521/FREE");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("C##USER1");

        modelBuilder.Entity<Dy_DayOffType>(entity =>
        {
            entity.ToTable("DY_DAY_OFF_TYPES", schema: "C##USER1");

            entity.HasKey(e => e.DyOffId);

            entity.Property(e => e.DyOffId).HasColumnName("DY_OFF_ID").HasColumnType("NUMBER");
            entity.Property(e => e.DyOffName).HasColumnName("DY_OFF_NAME").HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.IsGenderSpecific).HasColumnName("IS_GENDER_SPECIFIC").HasColumnType("NUMBER(1)").HasDefaultValueSql("0");
            entity.Property(e => e.AllowedGenderId).HasColumnName("ALLOWED_GENDER_ID").HasColumnType("NUMBER");
            entity.Property(e => e.IsPartialAllowed).HasColumnName("IS_PARTIAL_ALLOWED").HasColumnType("NUMBER(1)").HasDefaultValueSql("1");

            entity.HasOne(d => d.AllowedGender)
                  .WithMany(p => p.DyDayOffTypes)
                  .HasForeignKey(d => d.AllowedGenderId)
                  .HasConstraintName("FK_DAY_OFF_TYPES_GENDER");
        });

        modelBuilder.Entity<Vw_DayOffTypeWithGender>(e =>
        {
            e.HasNoKey();
            e.ToView("VW_DAY_OFF_TYPES_WITH_GENDER");
        });



        modelBuilder.Entity<Dy_DayOffBalance>(entity =>
        {
            entity.HasKey(e => e.DyOffBalanceId).HasName("SYS_C008597");

            entity.ToTable("DY_DAY_OFF_BALANCE");

            entity.Property(e => e.DyOffBalanceId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DY_OFF_BALANCE_ID");
            entity.Property(e => e.CarriedOverDays)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER")
                .HasColumnName("CARRIED_OVER_DAYS");
            entity.Property(e => e.TotalDays)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL_DAYS");
            entity.Property(e => e.UsedDays)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER")
                .HasColumnName("USED_DAYS");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Year)
                .HasColumnType("NUMBER")
                .HasColumnName("YEAR");

            entity.HasOne(d => d.User).WithMany(p => p.DyDayOffBalances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAY_OFF_BALANCE_USER");
        });

        modelBuilder.Entity<Dy_DayOffHistory>(entity =>
        {
            entity.HasKey(e => e.DyOffHistoryId).HasName("SYS_C008601");

            entity.ToTable("DY_DAY_OFF_HISTORY");

            entity.Property(e => e.DyOffHistoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DY_OFF_HISTORY_ID");
            entity.Property(e => e.ActionType)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("ACTION_TYPE");
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CHANGE_DATE");
            entity.Property(e => e.ChangedByUserId)
                .HasColumnType("NUMBER")
                .HasColumnName("CHANGED_BY_USER_ID");
            entity.Property(e => e.DayOffRequestId)
                .HasColumnType("NUMBER")
                .HasColumnName("DAY_OFF_REQUEST_ID");
            entity.Property(e => e.NewStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NEW_STATUS");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.OldStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("OLD_STATUS");

            entity.HasOne(d => d.ChangedByUser).WithMany(p => p.DyDayOffHistories)
                .HasForeignKey(d => d.ChangedByUserId)
                .HasConstraintName("FK_DAY_OFF_HISTORY_USER");

            entity.HasOne(d => d.DayOffRequest).WithMany(p => p.DyDayOffHistories)
                .HasForeignKey(d => d.DayOffRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAY_OFF_HISTORY_REQUEST");
        });

        modelBuilder.Entity<Dy_DayOffPolicy>(entity =>
        {
            entity.HasKey(e => e.DyPolicyId).HasName("SYS_C008584");

            entity.ToTable("DY_DAY_OFF_POLICIES");

            entity.Property(e => e.DyPolicyId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DY_POLICY_ID");
            entity.Property(e => e.DayOffTypeId)
                .HasColumnType("NUMBER")
                .HasColumnName("DAY_OFF_TYPE_ID");
            entity.Property(e => e.MaxConsecutiveDays)
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_CONSECUTIVE_DAYS");
            entity.Property(e => e.MaxDays)
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_DAYS");
            entity.Property(e => e.MaxSplitsPerYear)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_SPLITS_PER_YEAR");
            entity.Property(e => e.MinDays)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMBER")
                .HasColumnName("MIN_DAYS");

            entity.HasOne(d => d.DayOffType).WithMany(p => p.DyDayOffPolicies)
                .HasForeignKey(d => d.DayOffTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAY_OFF_POLICIES_TYPE");
        });

        modelBuilder.Entity<Dy_DayOffRequest>(entity =>
        {
            entity.HasKey(e => e.DyOffReqId).HasName("SYS_C008591");

            entity.ToTable("DY_DAY_OFF_REQUESTS");

            entity.Property(e => e.DyOffReqId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DY_OFF_REQ_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.DayOffTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DAY_OFF_TYPE_ID");
            entity.Property(e => e.DurationDays)
                .HasColumnType("NUMBER")
                .HasColumnName("DURATION_DAYS");
            entity.Property(e => e.DurationHours)
                .HasColumnType("NUMBER")
                .HasColumnName("DURATION_HOURS");
            entity.Property(e => e.EndDate)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("END_DATE");
            entity.Property(e => e.EndTime)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("END_TIME");
            entity.Property(e => e.FreeTravelDays)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER")
                .HasColumnName("FREE_TRAVEL_DAYS");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("REASON");
            entity.Property(e => e.RejectReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("REJECT_REASON");
            entity.Property(e => e.StartDate)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("START_DATE");
            entity.Property(e => e.StartTime)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("START_TIME");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'PENDING'")
                .HasColumnName("STATUS");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("DATE")
                .HasColumnName("UPDATED_AT");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.DayOffType).WithMany(p => p.DyDayOffRequests)
                .HasForeignKey(d => d.DayOffTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAY_OFF_REQUESTS_TYPE");

            entity.HasOne(d => d.User).WithMany(p => p.DyDayOffRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DAY_OFF_REQUESTS_USER");
        });

        modelBuilder.Entity<Dy_DayOffType>(entity =>
        {
            entity.HasKey(e => e.DyOffId).HasName("SYS_C008580");

            entity.ToTable("DY_DAY_OFF_TYPES");

            entity.Property(e => e.DyOffId)
                .HasColumnType("NUMBER")
                .HasColumnName("DY_OFF_ID");
            entity.Property(e => e.AllowedGenderId)
                .HasColumnType("NUMBER")
                .HasColumnName("ALLOWED_GENDER_ID");
            entity.Property(e => e.DyOffName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DY_OFF_NAME");
            entity.Property(e => e.IsGenderSpecific)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_GENDER_SPECIFIC");
            entity.Property(e => e.IsPartialAllowed)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_PARTIAL_ALLOWED");

            entity.HasOne(d => d.AllowedGender).WithMany(p => p.DyDayOffTypes)
                .HasForeignKey(d => d.AllowedGenderId)
                .HasConstraintName("FK_DAY_OFF_TYPES_GENDER");
        });

        modelBuilder.Entity<Dy_Department>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("SYS_C008562");

            entity.ToTable("DY_DEPARTMENTS");

            entity.HasIndex(e => e.DepName, "SYS_C008563").IsUnique();

            entity.Property(e => e.DepId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DEP_ID");
            entity.Property(e => e.DepName)
                .HasMaxLength(155)
                .IsUnicode(false)
                .HasColumnName("DEP_NAME");
        });

        modelBuilder.Entity<Dy_Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("SYS_C008557");

            entity.ToTable("DY_GENDERS");

            entity.HasIndex(e => e.GenderName, "SYS_C008558").IsUnique();

            entity.Property(e => e.GenderId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("GENDER_ID");
            entity.Property(e => e.GenderName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GENDER_NAME");
        });

        modelBuilder.Entity<Dy_Holiday>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("SYS_C008610");

            entity.ToTable("DY_HOLIDAYS");

            entity.Property(e => e.HolidayId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("HOLIDAY_ID");
            entity.Property(e => e.HolidayDate)
                .HasColumnType("DATE")
                .HasColumnName("HOLIDAY_DATE");
            entity.Property(e => e.HolidayName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("HOLIDAY_NAME");
            entity.Property(e => e.HolidayType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("'RESMI'\n")
                .HasColumnName("HOLIDAY_TYPE");
        });

        modelBuilder.Entity<Dy_Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("SYS_C008605");

            entity.ToTable("DY_NOTIFICATIONS");

            entity.Property(e => e.NotificationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("NOTIFICATION_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.IsRead)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_READ");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.DyNotifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_NOTIFICATIONS_USER");
        });

        modelBuilder.Entity<Dy_Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008559");

            entity.ToTable("DY_ROLES");

            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Dy_Title>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("SYS_C008566");

            entity.ToTable("DY_TITLES");

            entity.HasIndex(e => e.TitleName, "SYS_C008567").IsUnique();

            entity.Property(e => e.TitleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TITLE_ID");
            entity.Property(e => e.TitleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE_NAME");
        });

        modelBuilder.Entity<Dy_User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008573");

            entity.ToTable("DY_USERS");

            entity.HasIndex(e => e.TcNo, "SYS_C008574").IsUnique();

            entity.HasIndex(e => e.Email, "SYS_C008575").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Building)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BUILDING");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("DATE")
                .HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMBER")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.District)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("EMAIL");
            entity.Property(e => e.EmploymentDate)
                .HasColumnType("DATE")
                .HasColumnName("EMPLOYMENT_DATE");
            entity.Property(e => e.FirstName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GenderId)
                .HasColumnType("NUMBER")
                .HasColumnName("GENDER_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("1")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.LastName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Neighborhood)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NEIGHBORHOOD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STREET");
            entity.Property(e => e.TcNo)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("TC_NO");
            entity.Property(e => e.TitleId)
                .HasColumnType("NUMBER")
                .HasColumnName("TITLE_ID");

            entity.HasOne(d => d.Department).WithMany(p => p.DyUsers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_USERS_DEPT");

            entity.HasOne(d => d.Gender).WithMany(p => p.DyUsers)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_USERS_GENDER");

            entity.HasOne(d => d.Role).WithMany(p => p.DyUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERS_ROLE");

            entity.HasOne(d => d.Title).WithMany(p => p.DyUsers)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_USERS_TITLE");
        });

        modelBuilder.Entity<Vw_WeeklyDayOffStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_WEEKLY_DAY_OFF_STATS");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("DEPARTMENT_ID");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(155)
                .IsUnicode(false)
                .HasColumnName("DEPARTMENT_NAME");

            entity.Property(e => e.TotalRequests)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL_REQUESTS");

            entity.Property(e => e.WeekNumber)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("WEEK_NUMBER");

            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("YEAR");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}