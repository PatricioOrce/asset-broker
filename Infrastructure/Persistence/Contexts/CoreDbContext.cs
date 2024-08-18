using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssetType> AssetTypes { get; set; }

        public virtual DbSet<Asset> Assets { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasIndex(e => e.AssetTypeId, "IX_Assets_AssetTypeId");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.HasOne(d => d.AssetType).WithMany(p => p.Assets).HasForeignKey(d => d.AssetTypeId);
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.AssetId, "IX_Orders_AssetId");

                entity.HasIndex(e => e.StatusId, "IX_Orders_StatusId");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Asset).WithMany(p => p.Orders).HasForeignKey(d => d.AssetId);

                entity.HasOne(d => d.Status).WithMany(p => p.Orders).HasForeignKey(d => d.StatusId);
            });

            //Seeds
            modelBuilder.Entity<AssetType>().HasData(
            new AssetType { Id = (int)AssetTypeEnum.Bono, Description = "Bono" },
            new AssetType { Id = (int)AssetTypeEnum.Accion, Description = "Accion" },
            new AssetType { Id = (int)AssetTypeEnum.FCI, Description = "FCI" }
            );
            modelBuilder.Entity<OrderStatus>().HasData(
            new OrderStatus { Id = (int)OrderStatusesEnum.InProgress, Description = "En Progreso" },
            new OrderStatus { Id = (int)OrderStatusesEnum.Excecuted, Description = "Ejecutada" },
            new OrderStatus { Id = (int)OrderStatusesEnum.Cancelled, Description = "Cancelada" }
            );
            modelBuilder.Entity<Asset>().HasData(
            new Asset
            {
                Id = 1,
                Ticker = "AAPL",
                Name = "Apple",
                AssetTypeId = (int)AssetTypeEnum.Accion,
                UnitPrice = 177.97m
            },
            new Asset
            {
                Id = 2,
                Ticker = "GOOGL",
                Name = "Alphabet Inc",
                AssetTypeId = (int)AssetTypeEnum.Accion,
                UnitPrice = 138.21m
            },
            new Asset
            {
                Id = 3,
                Ticker = "MSFT",
                Name = "Microsoft",
                AssetTypeId = (int)AssetTypeEnum.Accion,
                UnitPrice = 329.04m
            },
            new Asset
            {
                Id = 4,
                Ticker = "KO",
                Name = "Coca Cola",
                AssetTypeId = (int)AssetTypeEnum.Accion,
                UnitPrice = 58.3m
            },
            new Asset
            {
                Id = 5,
                Ticker = "WMT",
                Name = "Walmart",
                AssetTypeId = (int)AssetTypeEnum.Accion,
                UnitPrice = 163.42m
            },
            new Asset
            {
                Id = 6,
                Ticker = "AL30",
                Name = "BONOS ARGENTINA USD 2030 L.A",
                AssetTypeId = (int)AssetTypeEnum.Bono,
                UnitPrice = 307.4m
            },
            new Asset
            {
                Id = 7,
                Ticker = "GD30",
                Name = "Bonos Globales Argentina USD Step Up 2030",
                AssetTypeId = (int)AssetTypeEnum.Bono,
                UnitPrice = 336.1m
            },
            new Asset
            {
                Id = 8,
                Ticker = "Delta.Pesos",
                Name = "Delta Pesos Clase A",
                AssetTypeId = (int)AssetTypeEnum.FCI,
                UnitPrice = 0.0181m
            },
            new Asset
            {
                Id = 9,
                Ticker = "Fima.Premium",
                Name = "Fima Premium Clase A",
                AssetTypeId = (int)AssetTypeEnum.FCI,
                UnitPrice = 0.0317m
            }
        );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
