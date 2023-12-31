﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductManagement.API.Data;

#nullable disable

namespace ProductManagement.API.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20230922131558_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductManagement.Library.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Grocery"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Health & Beauty"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sports & Outdoors"
                        });
                });

            modelBuilder.Entity("ProductManagement.Library.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "Product 1",
                            Price = 19.99m,
                            SupplierId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Name = "Product 2",
                            Price = 29.99m,
                            SupplierId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Name = "Product 3",
                            Price = 30.99m,
                            SupplierId = 3
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Name = "Product 4",
                            Price = 40.00m,
                            SupplierId = 4
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            Name = "Product 5",
                            Price = 45.50m,
                            SupplierId = 5
                        });
                });

            modelBuilder.Entity("ProductManagement.Library.Domain.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Supplier A"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Supplier B"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Supplier C"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Supplier D"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Supplier E"
                        });
                });

            modelBuilder.Entity("ProductManagement.Library.Domain.Product", b =>
                {
                    b.HasOne("ProductManagement.Library.Domain.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductManagement.Library.Domain.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ProductManagement.Library.Domain.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductManagement.Library.Domain.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
