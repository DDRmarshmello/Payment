using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BACK.Models;

public partial class PayContext : DbContext
{
    public PayContext()
    {
    }

    public PayContext(DbContextOptions<PayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmDesc> EmDescs { get; set; }

    public virtual DbSet<EmIng> EmIngs { get; set; }

    public virtual DbSet<EmTypeDesc> EmTypeDescs { get; set; }

    public virtual DbSet<EmTypeIng> EmTypeIngs { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=Pay;user=root;password=ddr210615", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.38-mysql"), x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<EmDesc>(entity =>
        {
            entity.HasKey(e => e.NumEntry).HasName("PRIMARY");

            entity
                .ToTable("em_desc")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.NumEmp, "FK_Em_desc_Empleado");

            entity.HasIndex(e => e.EmTypeDesc, "FK_Em_desc_type");

            entity.Property(e => e.NumEntry).HasColumnName("Num_entry");
            entity.Property(e => e.AplicationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.EmTypeDesc).HasColumnName("em_type_desc");
            entity.Property(e => e.NumEmp).HasColumnName("Num_emp");

            entity.HasOne(d => d.EmTypeDescNavigation).WithMany(p => p.EmDescs)
                .HasForeignKey(d => d.EmTypeDesc)
                .HasConstraintName("FK_Em_desc_type");

            entity.HasOne(d => d.NumEmpNavigation).WithMany(p => p.EmDescs)
                .HasForeignKey(d => d.NumEmp)
                .HasConstraintName("FK_Em_desc_Empleado");
        });

        modelBuilder.Entity<EmIng>(entity =>
        {
            entity.HasKey(e => e.NumEntry).HasName("PRIMARY");

            entity
                .ToTable("em_ing")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.NumEmp, "FK_Em_Ing_Empleado");

            entity.HasIndex(e => e.EmTypeIng, "FK_Em_Ing_ING");

            entity.Property(e => e.NumEntry).HasColumnName("Num_entry");
            entity.Property(e => e.AplicationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.EmTypeIng).HasColumnName("em_type_ing");
            entity.Property(e => e.NumEmp).HasColumnName("Num_emp");

            entity.HasOne(d => d.EmTypeIngNavigation).WithMany(p => p.EmIngs)
                .HasForeignKey(d => d.EmTypeIng)
                .HasConstraintName("FK_Em_Ing_ING");

            entity.HasOne(d => d.NumEmpNavigation).WithMany(p => p.EmIngs)
                .HasForeignKey(d => d.NumEmp)
                .HasConstraintName("FK_Em_Ing_Empleado");
        });

        modelBuilder.Entity<EmTypeDesc>(entity =>
        {
            entity.HasKey(e => e.NumEntry).HasName("PRIMARY");

            entity
                .ToTable("em_type_desc")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.NumEntry).HasColumnName("Num_entry");
            entity.Property(e => e.Configuracion).HasColumnName("configuracion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(191)
                .HasColumnName("descripcion");
            entity.Property(e => e.NumQuotas).HasColumnName("Num_quotas");
            entity.Property(e => e.ThisLoan).HasColumnName("this_loan");
            entity.Property(e => e.ThisPercentage)
                .HasDefaultValueSql("'0'")
                .HasColumnName("this_Percentage");
        });

        modelBuilder.Entity<EmTypeIng>(entity =>
        {
            entity.HasKey(e => e.NumEntry).HasName("PRIMARY");

            entity
                .ToTable("em_type_ing")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.NumEntry).HasColumnName("Num_entry");
            entity.Property(e => e.Configuracion).HasColumnName("configuracion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(191)
                .HasColumnName("descripcion");
            entity.Property(e => e.ThisChristmasSalary).HasColumnName("this_Christmas_salary");
            entity.Property(e => e.ThisExempt).HasColumnName("this_exempt");
            entity.Property(e => e.ThisLaborBenefits).HasColumnName("this_labor_benefits");
            entity.Property(e => e.ThisTax).HasColumnName("this_tax");
            entity.Property(e => e.ThisTaxMedical).HasColumnName("this_tax_medical");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.NumEntry).HasName("PRIMARY");

            entity
                .ToTable("empleado")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.NumEntry).HasColumnName("Num_entry");
            entity.Property(e => e.Apellido).HasMaxLength(191);
            entity.Property(e => e.Cargo).HasColumnName("cargo");
            entity.Property(e => e.Cedula).HasMaxLength(191);
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime(3)")
                .HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(191);
            entity.Property(e => e.Salary).HasPrecision(10, 2);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
