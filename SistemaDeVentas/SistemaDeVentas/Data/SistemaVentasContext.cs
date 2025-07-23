using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Models;

namespace SistemaDeVentas.Data
{
    public class SistemaVentasContext : DbContext
    {
        public SistemaVentasContext(DbContextOptions<SistemaVentasContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
                
                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Venta
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroVenta).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Estado).HasMaxLength(20);
                entity.Property(e => e.Observaciones).HasMaxLength(500);
                entity.HasIndex(e => e.NumeroVenta).IsUnique();

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de DetalleVenta
            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Datos semilla (seed data)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Categorías
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Electrónicos", Descripcion = "Dispositivos electrónicos y gadgets" },
                new Categoria { Id = 2, Nombre = "Ropa", Descripcion = "Prendas de vestir para toda la familia" },
                new Categoria { Id = 3, Nombre = "Hogar", Descripcion = "Artículos para el hogar y decoración" }
            );

            // Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Juan Pérez", Email = "juan@example.com", Telefono = "123456789", Direccion = "Calle 123 #45-67" },
                new Usuario { Id = 2, Nombre = "María García", Email = "maria@example.com", Telefono = "987654321", Direccion = "Carrera 45 #23-89" }
            );

            // Productos
            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Smartphone", Descripcion = "Teléfono inteligente última generación", Precio = 699.99m, Stock = 25, CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "Laptop", Descripcion = "Computadora portátil para trabajo", Precio = 1299.99m, Stock = 15, CategoriaId = 1 },
                new Producto { Id = 3, Nombre = "Camiseta", Descripcion = "Camiseta de algodón 100%", Precio = 29.99m, Stock = 50, CategoriaId = 2 }
            );
        }
    }
} 