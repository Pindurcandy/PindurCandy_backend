using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PindurCandy.Models
{
    public partial class candyshopContext : DbContext
    {
        public candyshopContext()
        {
        }

        public candyshopContext(DbContextOptions<candyshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Felhasznalok> Felhasznaloks { get; set; }
        public virtual DbSet<Registry> Registries { get; set; }
        public virtual DbSet<Termekek> Termekeks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;database=candyshop;user=root;password=;sslmode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Felhasznalok>(entity =>
            {
                entity.ToTable("felhasznalok");

                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.HasIndex(e => e.FelhasznaloNev, "FelhasznaloNev")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Aktiv).HasColumnType("int(1)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("HASH");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnType("mediumblob")
                    .HasColumnName("image");

                entity.Property(e => e.Jogosultsag).HasColumnType("int(1)");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("SALT");

                entity.Property(e => e.TeljesNev)
                    .IsRequired()
                    .HasMaxLength(70);
            });

            modelBuilder.Entity<Registry>(entity =>
            {
                entity.ToTable("registry");

                entity.HasIndex(e => e.Key, "key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("HASH");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("SALT");

                entity.Property(e => e.TeljesNev)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Termekek>(entity =>
            {
                entity.ToTable("termekek");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Aktiv)
                    .HasColumnType("int(1)")
                    .HasColumnName("aktiv");

                entity.Property(e => e.Ar)
                    .HasColumnType("int(50)")
                    .HasColumnName("ar");

                entity.Property(e => e.Kep)
                    .IsRequired()
                    .HasColumnType("mediumblob")
                    .HasColumnName("kep");

                entity.Property(e => e.Leiras)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("leiras");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("link");

                entity.Property(e => e.TermekNev)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("termekNev");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
