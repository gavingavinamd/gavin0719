using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Signup03.Models
{
    public partial class Gavin01DBContext : DbContext
    {
        public Gavin01DBContext()
        {
        }

        public Gavin01DBContext(DbContextOptions<Gavin01DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblActiveItem> TblActiveItems { get; set; } = null!;
        public virtual DbSet<TblSignup> TblSignups { get; set; } = null!;
        public virtual DbSet<TblSignupItem> TblSignupItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-HMED35I9;Initial Catalog=Gavin01DB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblActiveItem>(entity =>
            {
                entity.HasKey(e => e.CItemId);

                entity.ToTable("tblActiveItem");

                entity.Property(e => e.CItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("cItemID");

                entity.Property(e => e.CActiveDt)
                    .HasMaxLength(50)
                    .HasColumnName("cActiveDt");

                entity.Property(e => e.CItemName)
                    .HasMaxLength(50)
                    .HasColumnName("cItemName");
            });

            modelBuilder.Entity<TblSignup>(entity =>
            {
                entity.HasKey(e => e.CId);

                entity.ToTable("tblSignup");

                entity.Property(e => e.CId).HasColumnName("cID");

                entity.Property(e => e.CCreateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("cCreateDT");

                entity.Property(e => e.CEmail)
                    .HasMaxLength(50)
                    .HasColumnName("cEmail");

                entity.Property(e => e.CMobile)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cMobile");

                entity.Property(e => e.CName)
                    .HasMaxLength(20)
                    .HasColumnName("cName");
            });

            modelBuilder.Entity<TblSignupItem>(entity =>
            {
                entity.HasKey(e => new { e.CSignupId, e.CItemId });

                entity.ToTable("tblSignupItem");

                entity.Property(e => e.CSignupId).HasColumnName("cSignupID");

                entity.Property(e => e.CItemId).HasColumnName("cItemID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
