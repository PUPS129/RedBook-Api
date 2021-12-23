using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public partial class RedBookContext : DbContext
    {
        public RedBookContext()
        {
        }

        public RedBookContext(DbContextOptions<RedBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Kingdoms> Kingdoms { get; set; }
        public virtual DbSet<SubClass> SubClass { get; set; }
        public virtual DbSet<Thing> Thing { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

//#error твой код говно (Ошибка 228)
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RedBook;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.KingdomId).HasColumnName("KingdomID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Kingdom)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.KingdomId)
                    .HasConstraintName("FK_Class_Kingdoms");
            });

            modelBuilder.Entity<Kingdoms>(entity =>
            {
                entity.HasKey(e => e.KingdomId)
                    .HasName("PK_Class");

                entity.Property(e => e.KingdomId).HasColumnName("KingdomID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SubClass>(entity =>
            {
                entity.Property(e => e.SubClassId).HasColumnName("SubClassID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SubClass)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_SubClass_Class");
            });

            modelBuilder.Entity<Thing>(entity =>
            {
                entity.Property(e => e.ThingId).HasColumnName("ThingID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SubClassId).HasColumnName("SubClassID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Thing)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Thing_Category");

                entity.HasOne(d => d.SubClass)
                    .WithMany(p => p.Thing)
                    .HasForeignKey(d => d.SubClassId)
                    .HasConstraintName("FK_Thing_SubClass");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
