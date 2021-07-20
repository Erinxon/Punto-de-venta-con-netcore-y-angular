using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class PuntoDeVentaContext : DbContext
    {
        public PuntoDeVentaContext()
        {
        }

        public PuntoDeVentaContext(DbContextOptions<PuntoDeVentaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }
        public virtual DbSet<Direccion> Direccions { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.Cedula, "UQ__Clientes__B4ADFE38495A04D8")
                    .IsUnique();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDireccionNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdDireccion)
                    .HasConstraintName("FK__Clientes__IdDire__300424B4");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__Compras__IdProve__37A5467C");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Compras__IdUsuar__36B12243");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__DetalleCo__IdCom__3A81B327");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleCo__IdPro__3B75D760");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleVe__IdPro__4316F928");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__IdVen__4222D4EF");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.ToTable("Direccion");

                entity.Property(e => e.Calle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCasa)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");

                entity.HasIndex(e => e.Rnc, "UQ__Empresa__C2B7F58E9BA981E4")
                    .IsUnique();

                entity.HasIndex(e => e.Ncf, "UQ__Empresa__DF907FBE1B5B264B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreado");

                entity.Property(e => e.Ncf)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ncf");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("razonSocial");

                entity.Property(e => e.Rnc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rnc");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Productos__IdCat__2A4B4B5E");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasIndex(e => e.Rnc, "UQ__Proveedo__CAF07DA8DED2F242")
                    .IsUnique();

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rnc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDireccionNavigation)
                    .WithMany(p => p.Proveedores)
                    .HasForeignKey(d => d.IdDireccion)
                    .HasConstraintName("FK__Proveedor__IdDir__33D4B598");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE0CD5FDE82")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105344D0F09DF")
                    .IsUnique();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pass).IsUnicode(false);

                entity.Property(e => e.Roll)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Ventas__IdClient__3F466844");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Ventas__IdUsuari__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
