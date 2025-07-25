﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaDeVentas.Data;

#nullable disable

namespace SistemaDeVentas.Migrations
{
    [DbContext(typeof(SistemaVentasContext))]
    partial class SistemaVentasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SistemaDeVentas.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Dispositivos electrónicos y gadgets",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6279),
                            Nombre = "Electrónicos"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Prendas de vestir para toda la familia",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6331),
                            Nombre = "Ropa"
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Artículos para el hogar y decoración",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6376),
                            Nombre = "Hogar"
                        });
                });

            modelBuilder.Entity("SistemaDeVentas.Models.DetalleVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("DetallesVenta");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoriaId = 1,
                            Descripcion = "Teléfono inteligente última generación",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6665),
                            Nombre = "Smartphone",
                            Precio = 699.99m,
                            Stock = 25
                        },
                        new
                        {
                            Id = 2,
                            CategoriaId = 1,
                            Descripcion = "Computadora portátil para trabajo",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6710),
                            Nombre = "Laptop",
                            Precio = 1299.99m,
                            Stock = 15
                        },
                        new
                        {
                            Id = 3,
                            CategoriaId = 2,
                            Descripcion = "Camiseta de algodón 100%",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6755),
                            Nombre = "Camiseta",
                            Precio = 29.99m,
                            Stock = 50
                        });
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Direccion = "Calle 123 #45-67",
                            Email = "juan@example.com",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6564),
                            Nombre = "Juan Pérez",
                            Telefono = "123456789"
                        },
                        new
                        {
                            Id = 2,
                            Direccion = "Carrera 45 #23-89",
                            Email = "maria@example.com",
                            Estado = true,
                            FechaCreacion = new DateTime(2025, 7, 20, 23, 54, 45, 278, DateTimeKind.Local).AddTicks(6609),
                            Nombre = "María García",
                            Telefono = "987654321"
                        });
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Venta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroVenta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NumeroVenta")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.DetalleVenta", b =>
                {
                    b.HasOne("SistemaDeVentas.Models.Producto", "Producto")
                        .WithMany("DetallesVenta")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SistemaDeVentas.Models.Venta", "Venta")
                        .WithMany("DetallesVenta")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Producto", b =>
                {
                    b.HasOne("SistemaDeVentas.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Venta", b =>
                {
                    b.HasOne("SistemaDeVentas.Models.Usuario", "Usuario")
                        .WithMany("Ventas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Producto", b =>
                {
                    b.Navigation("DetallesVenta");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Usuario", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("SistemaDeVentas.Models.Venta", b =>
                {
                    b.Navigation("DetallesVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
