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

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentDetail> AppointmentDetails { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<CertificateArtist> CertificateArtists { get; set; }

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
        var strConn = config["ConnectionStrings:ArtTattooLover"];
        return strConn;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__F3DBC57332E0473B");

            entity.ToTable("Account");

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

            entity.HasOne(d => d.Artist).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Account__ArtistI__47DBAE45");

            entity.HasOne(d => d.Staff).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Account__StaffID__48CFD27E");

            entity.HasOne(d => d.TattooLover).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TattooLoverId)
                .HasConstraintName("FK__Account__TattooL__46E78A0C");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA23414E8E6");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.TattooLoverId).HasColumnName("TattooLoverID");
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.Studio).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Appointme__Studi__3A81B327");

            entity.HasOne(d => d.TattooLover).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.TattooLoverId)
                .HasConstraintName("FK__Appointme__Tatto__398D8EEE");
        });

        modelBuilder.Entity<AppointmentDetail>(entity =>
        {
            entity.HasKey(e => e.AppointmentDetailId).HasName("PK__Appointm__B475AC15C0CE93A4");

            entity.ToTable("AppointmentDetail");

            entity.Property(e => e.AppointmentDetailId).HasColumnName("AppointmentDetailID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__3D5E1FD2");

            entity.HasOne(d => d.Schedule).WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__Appointme__Sched__3F466844");

            entity.HasOne(d => d.Service).WithMany(p => p.AppointmentDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__3E52440B");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B7060F8C65D");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.Studio).WithMany(p => p.Artists)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Artists__StudioI__2A4B4B5E");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__A15CBE8E7796633E");

            entity.ToTable("Certificate");

            entity.Property(e => e.CertificateId).HasColumnName("certificateID");
            entity.Property(e => e.CertificateName)
                .HasMaxLength(255)
                .HasColumnName("certificateName");
        });

        modelBuilder.Entity<CertificateArtist>(entity =>
        {
            entity
               // .HasNoKey()
                .ToTable("Certificate_Artists")
            .HasKey(x => new { x.ArtistId, x.CertificateId });
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.CertificateId).HasColumnName("certificateID");
            entity.Property(e => e.Urllink)
                .HasMaxLength(255)
                .HasColumnName("urllink");

            entity.HasOne(d => d.Artist).WithMany()
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Certifica__Artis__440B1D61");

            entity.HasOne(d => d.Certificate).WithMany()
                .HasForeignKey(d => d.CertificateId)
                .HasConstraintName("FK__Certifica__certi__4316F928");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B698A58C3C1");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Time).HasColumnType("date");

            entity.HasOne(d => d.Artist).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Schedule__Artist__36B12243");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EABD8067AB");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceName).HasMaxLength(255);

            entity.HasOne(d => d.Artist).WithMany(p => p.Services)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Service__ArtistI__30F848ED");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7596F0215");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.StaffName).HasMaxLength(255);
            entity.Property(e => e.StaffPhone).HasMaxLength(255);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");

            entity.HasOne(d => d.Studio).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK__Staff__StudioID__33D4B598");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("PK__Studio__4ACC3B5040AF08EB");

            entity.ToTable("Studio");

            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Website).HasMaxLength(255);
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD147A07F53CE66");

            entity.ToTable("Style");

            entity.Property(e => e.StyleId).HasColumnName("StyleID");
            entity.Property(e => e.StyleName).HasMaxLength(255);
        });

        modelBuilder.Entity<TattooLover>(entity =>
        {
            entity.HasKey(e => e.TattooLoverId).HasName("PK__TattooLo__7FF7E1A10815D5F0");

            entity.ToTable("TattooLover");

            entity.Property(e => e.TattooLoverId).HasColumnName("TattooLoverID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
        });

        modelBuilder.Entity<TattoosDesign>(entity =>
        {
            entity.HasKey(e => e.TattoosDesignId).HasName("PK__TattoosD__0454E0D04CE9D0FA");

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
                .HasConstraintName("FK__TattoosDe__Artis__2E1BDC42");

            entity.HasOne(d => d.Style).WithMany(p => p.TattoosDesigns)
                .HasForeignKey(d => d.StyleId)
                .HasConstraintName("FK__TattoosDe__Style__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
