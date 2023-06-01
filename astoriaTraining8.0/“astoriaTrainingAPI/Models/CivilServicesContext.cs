using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace _astoriaTrainingAPI.Models
{
    public partial class CivilServicesContext : DbContext
    {
     
        public CivilServicesContext(DbContextOptions<CivilServicesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cources> Cources { get; set; }
        public virtual DbSet<EnrollCources> EnrollCources { get; set; }
        public virtual DbSet<StudentDetails> StudentDetails { get; set; }
        public virtual DbSet<StudentMaster> StudentMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ASTORIA-LT43;Database=CivilServices;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cources>(entity =>
            {
                entity.HasKey(e => e.CourceId)
                    .HasName("PK__Cources__4A7C9E5D233D9D24");

                entity.Property(e => e.CourceId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CourceDuration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EnrollCources>(entity =>
            {
                entity.HasKey(e => e.CourceId)
                    .HasName("PK__EnrollCo__4A7C9E5DF68A9DF9");

                entity.Property(e => e.CourceId).ValueGeneratedNever();

                entity.Property(e => e.CourceDuration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<StudentDetails>(entity =>
            {
                entity.HasKey(e => e.StdRollNo)
                    .HasName("PK__StudentD__6007A33C80E27B05");

                entity.Property(e => e.StdRollNo).HasColumnName("Std_RollNo");

                entity.Property(e => e.StdCourceId).HasColumnName("Std_CourceId");

                entity.Property(e => e.StdFname)
                    .IsRequired()
                    .HasColumnName("Std_FName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StdGender)
                    .IsRequired()
                    .HasColumnName("Std_Gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StdJoiningDate)
                    .HasColumnName("Std_JoiningDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.StdLname)
                    .IsRequired()
                    .HasColumnName("Std_LName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StdResignationDate)
                    .HasColumnName("Std_ResignationDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.StdCource)
                    .WithMany(p => p.StudentDetails)
                    .HasForeignKey(d => d.StdCourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentDe__Std_C__3E52440B");
            });

            modelBuilder.Entity<StudentMaster>(entity =>
            {
                entity.HasKey(e => e.StdRollNo)
                    .HasName("PK__StudentM__6007A33C2B46AB34");

                entity.Property(e => e.StdRollNo).HasColumnName("Std_RollNo");

                entity.Property(e => e.StdCourceId)
                    .IsRequired()
                    .HasColumnName("Std_CourceId")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StdFname)
                    .IsRequired()
                    .HasColumnName("Std_FName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StdGender)
                    .IsRequired()
                    .HasColumnName("Std_Gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StdJoiningDate)
                    .HasColumnName("Std_JoiningDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.StdLname)
                    .IsRequired()
                    .HasColumnName("Std_LName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StdResignationDate)
                    .HasColumnName("Std_ResignationDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.StdCource)
                    .WithMany(p => p.StudentMaster)
                    .HasForeignKey(d => d.StdCourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentMa__Std_C__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
