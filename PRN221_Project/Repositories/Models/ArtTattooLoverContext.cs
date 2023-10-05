using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repositories.Models;

public partial class ArtTattooLoverContext : DbContext
{
    public ArtTattooLoverContext()
    {
    }

    public ArtTattooLoverContext(DbContextOptions<ArtTattooLoverContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentDetail> AppointmentDetails { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<CertificateArtist> CertificateArtists { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<TattooLover> TattooLovers { get; set; }

    public virtual DbSet<TattoosDesign> TattoosDesigns { get; set; }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        var strConn = config["ConnectionStrings:ArtTattooLoverDB"];
        return strConn;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA26C99BF76");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.TattooLoverId).HasColumnName("TattooLoverID");
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__Appointme__Sched__3B75D760");

            entity.HasOne(d => d.Studio).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Appointme__Studi__3C69FB99");

            entity.HasOne(d => d.TattooLover).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.TattooLoverId)
                .HasConstraintName("FK__Appointme__Tatto__3A81B327");
        });

        modelBuilder.Entity<AppointmentDetail>(entity =>
        {
            entity.HasKey(e => e.AppointmentDetailId).HasName("PK__Appointm__B475AC15EA5822C7");

            entity.ToTable("AppointmentDetail");

            entity.Property(e => e.AppointmentDetailId).HasColumnName("AppointmentDetailID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__3F466844");

            entity.HasOne(d => d.Service).WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__403A8C7D");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B70BF9BF574");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.MainStyleNavigation).WithMany(p => p.Artists)
                .HasForeignKey(d => d.MainStyle)
                .HasConstraintName("FK__Artists__MainSty__2A4B4B5E");

            entity.HasOne(d => d.Studio).WithMany(p => p.Artists)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Artists__StudioI__2B3F6F97");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__A15CBE8E2F79CF50");

            entity.ToTable("Certificate");

            entity.Property(e => e.CertificateId).HasColumnName("certificateID");
            entity.Property(e => e.CertificateName)
                .HasMaxLength(255)
                .HasColumnName("certificateName");
        });

        modelBuilder.Entity<CertificateArtist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Certificate_Artists");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.CertificateId).HasColumnName("certificateID");

            entity.HasOne(d => d.Artist).WithMany()
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Certifica__Artis__44FF419A");

            entity.HasOne(d => d.Certificate).WithMany()
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Certifica__certi__440B1D61");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Login__F3DBC573A0A0E230");

            entity.ToTable("Login");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.TattooLoverId).HasColumnName("TattooLoverID");

            entity.HasOne(d => d.Artist).WithMany(p => p.Logins)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Login__ArtistID__48CFD27E");

            entity.HasOne(d => d.Staff).WithMany(p => p.Logins)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Login__StaffID__49C3F6B7");

            entity.HasOne(d => d.TattooLover).WithMany(p => p.Logins)
                .HasForeignKey(d => d.TattooLoverId)
                .HasConstraintName("FK__Login__TattooLov__47DBAE45");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B6982BB6FEE");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Time).HasColumnType("date");

            entity.HasOne(d => d.Artist).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Schedule__Artist__37A5467C");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA9CA39D91");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceName).HasMaxLength(255);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.Studio).WithMany(p => p.Services)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Service__StudioI__31EC6D26");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7777D73B0");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.StaffName).HasMaxLength(255);
            entity.Property(e => e.StaffPhone).HasMaxLength(255);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.Studio).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Staff__StudioID__34C8D9D1");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("PK__Studio__4ACC3B501982F210");

            entity.ToTable("Studio");

            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Website).HasMaxLength(255);
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD147A02CE5941D");

            entity.ToTable("Style");

            entity.Property(e => e.StyleId).HasColumnName("StyleID");
            entity.Property(e => e.StyleName).HasMaxLength(255);
        });

        modelBuilder.Entity<TattooLover>(entity =>
        {
            entity.HasKey(e => e.TattooLoverId).HasName("PK__TattooLo__7FF7E1A18BC13510");

            entity.ToTable("TattooLover");

            entity.Property(e => e.TattooLoverId).HasColumnName("TattooLoverID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
        });

        modelBuilder.Entity<TattoosDesign>(entity =>
        {
            entity.HasKey(e => e.TattoosDesignId).HasName("PK__TattoosD__0454E0D05FC5BA40");

            entity.ToTable("TattoosDesign");

            entity.Property(e => e.TattoosDesignId).HasColumnName("TattoosDesignID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImgUri)
                .HasMaxLength(255)
                .HasColumnName("ImgURI");
            entity.Property(e => e.StyleId).HasColumnName("StyleID");
            entity.Property(e => e.TattoosDesignName).HasMaxLength(255);

            entity.HasOne(d => d.Artist).WithMany(p => p.TattoosDesigns)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__TattoosDe__Artis__2F10007B");

            entity.HasOne(d => d.Style).WithMany(p => p.TattoosDesigns)
                .HasForeignKey(d => d.StyleId)
                .HasConstraintName("FK__TattoosDe__Style__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
