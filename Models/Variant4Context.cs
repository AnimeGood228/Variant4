using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Variant4.Models;

public partial class Variant4Context : DbContext
{
    public Variant4Context()
    {
    }

    public Variant4Context(DbContextOptions<Variant4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<PatientRequest> PatientRequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=1misa;Database=Variant4;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PatientRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PatientR__3214EC07F339910B");

            entity.HasIndex(e => e.Article, "UQ__PatientR__4943444ABB1BF9D1").IsUnique();

            entity.Property(e => e.Article).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.PatientRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PatientRe__UserI__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC071A73DF5C");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825BA23268E8").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
