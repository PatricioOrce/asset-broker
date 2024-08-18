﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Core
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AssetTypeId" }, "IX_Assets_AssetTypeId");

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssetTypeId = 2,
                            Name = "Apple",
                            Ticker = "AAPL",
                            UnitPrice = 177.97m
                        },
                        new
                        {
                            Id = 2,
                            AssetTypeId = 2,
                            Name = "Alphabet Inc",
                            Ticker = "GOOGL",
                            UnitPrice = 138.21m
                        },
                        new
                        {
                            Id = 3,
                            AssetTypeId = 2,
                            Name = "Microsoft",
                            Ticker = "MSFT",
                            UnitPrice = 329.04m
                        },
                        new
                        {
                            Id = 4,
                            AssetTypeId = 2,
                            Name = "Coca Cola",
                            Ticker = "KO",
                            UnitPrice = 58.3m
                        },
                        new
                        {
                            Id = 5,
                            AssetTypeId = 2,
                            Name = "Walmart",
                            Ticker = "WMT",
                            UnitPrice = 163.42m
                        },
                        new
                        {
                            Id = 6,
                            AssetTypeId = 1,
                            Name = "BONOS ARGENTINA USD 2030 L.A",
                            Ticker = "AL30",
                            UnitPrice = 307.4m
                        },
                        new
                        {
                            Id = 7,
                            AssetTypeId = 1,
                            Name = "Bonos Globales Argentina USD Step Up 2030",
                            Ticker = "GD30",
                            UnitPrice = 336.1m
                        },
                        new
                        {
                            Id = 8,
                            AssetTypeId = 3,
                            Name = "Delta Pesos Clase A",
                            Ticker = "Delta.Pesos",
                            UnitPrice = 0.0181m
                        },
                        new
                        {
                            Id = 9,
                            AssetTypeId = 3,
                            Name = "Fima Premium Clase A",
                            Ticker = "Fima.Premium",
                            UnitPrice = 0.0317m
                        });
                });

            modelBuilder.Entity("Domain.Models.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Bono"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Accion"
                        },
                        new
                        {
                            Id = 3,
                            Description = "FCI"
                        });
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AssetId" }, "IX_Orders_AssetId");

                    b.HasIndex(new[] { "StatusId" }, "IX_Orders_StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "En Progreso"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Ejecutada"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Cancelada"
                        });
                });

            modelBuilder.Entity("Domain.Models.Asset", b =>
                {
                    b.HasOne("Domain.Models.AssetType", "AssetType")
                        .WithMany("Assets")
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetType");
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.HasOne("Domain.Models.Asset", "Asset")
                        .WithMany("Orders")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId");

                    b.Navigation("Asset");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Models.Asset", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Models.AssetType", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Domain.Models.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
