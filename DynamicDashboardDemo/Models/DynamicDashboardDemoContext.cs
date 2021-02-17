using System;
using DynamicDashboardDemo.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

#nullable disable

namespace DynamicDashboardDemo.Models
{
    public partial class DynamicDashboardDemoContext : DbContext
    {
        private readonly IOptions<DatabaseSettings> _databaseSettings;
        public DynamicDashboardDemoContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DynamicDashboardDemoContext(DbContextOptions<DynamicDashboardDemoContext> options, IOptions<DatabaseSettings> databaseSettings)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            _databaseSettings = databaseSettings;
        }

        public virtual DbSet<DashboardLinkedWidgets> DashboardLinkedWidgets { get; set; }
        public virtual DbSet<DashboardsInfo> DashboardsInfos { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<Widget> Widgets { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_databaseSettings.Value.SqlConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DashboardLinkedWidgets>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Placement)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DashboardsInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Dashboards_Info");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
