using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.Models;
namespace ProyectoFactura.DAL.DataContext;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Detallexfactura> Detallexfacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Mesero> Meseros { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //modelBuilder
        //    .HasDefaultSchema("USER_FACTURA")
        //    .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Identificacion).HasName("SYS_C008316");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDENTIFICACION");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
        });

        modelBuilder.Entity<Detallexfactura>(entity =>
        {
            entity.HasKey(e => e.Iddetallexfactura).HasName("SYS_C008342");

            entity.ToTable("DETALLEXFACTURA");

            entity.Property(e => e.Iddetallexfactura)
                .HasColumnType("NUMBER")
                .HasColumnName("IDDETALLEXFACTURA");
            entity.Property(e => e.Idsupervisor)
                .HasColumnType("NUMBER")
                .HasColumnName("IDSUPERVISOR");
            entity.Property(e => e.Nrofactura)
                .HasColumnType("NUMBER")
                .HasColumnName("NROFACTURA");
            entity.Property(e => e.Plato)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLATO");
            entity.Property(e => e.Valor)
                .HasColumnType("NUMBER")
                .HasColumnName("VALOR");

            entity.HasOne(d => d.IdsupervisorNavigation).WithMany(p => p.Detallexfacturas)
                .HasForeignKey(d => d.Idsupervisor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETALLE_SUPERVISOR");

            entity.HasOne(d => d.NrofacturaNavigation).WithMany(p => p.Detallexfacturas)
                .HasForeignKey(d => d.Nrofactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETALLE_FACTURA");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Nrofactura).HasName("SYS_C008334");

            entity.ToTable("FACTURA");

            entity.Property(e => e.Nrofactura)
                .HasColumnType("NUMBER")
                .HasColumnName("NROFACTURA");
            entity.Property(e => e.Fecha)
                .HasColumnType("DATE")
                .HasColumnName("FECHA");
            entity.Property(e => e.Idcliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDCLIENTE");
            entity.Property(e => e.Idmesero)
                .HasColumnType("NUMBER")
                .HasColumnName("IDMESERO");
            entity.Property(e => e.Nromesa)
                .HasColumnType("NUMBER")
                .HasColumnName("NROMESA");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_CLIENTE");

            entity.HasOne(d => d.IdmeseroNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Idmesero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_MESERO");

            entity.HasOne(d => d.NromesaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Nromesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_MESA");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Nromesa).HasName("SYS_C008329");

            entity.ToTable("MESA");

            entity.Property(e => e.Nromesa)
                .HasColumnType("NUMBER")
                .HasColumnName("NROMESA");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Puestos)
                .HasColumnType("NUMBER")
                .HasColumnName("PUESTOS");
            entity.Property(e => e.Reservada)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'0' ")
                .IsFixedLength()
                .HasColumnName("RESERVADA");
        });

        modelBuilder.Entity<Mesero>(entity =>
        {
            entity.HasKey(e => e.Idmesero).HasName("SYS_C008321");

            entity.ToTable("MESERO");

            entity.Property(e => e.Idmesero)
                .HasColumnType("NUMBER")
                .HasColumnName("IDMESERO");
            entity.Property(e => e.Antiguedad)
                .HasColumnType("NUMBER")
                .HasColumnName("ANTIGUEDAD");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.Edad)
                .HasColumnType("NUMBER")
                .HasColumnName("EDAD");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.Idsupervisor).HasName("SYS_C008326");

            entity.ToTable("SUPERVISOR");

            entity.Property(e => e.Idsupervisor)
                .HasColumnType("NUMBER")
                .HasColumnName("IDSUPERVISOR");
            entity.Property(e => e.Antiguedad)
                .HasColumnType("NUMBER")
                .HasColumnName("ANTIGUEDAD");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.Edad)
                .HasColumnType("NUMBER")
                .HasColumnName("EDAD");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
