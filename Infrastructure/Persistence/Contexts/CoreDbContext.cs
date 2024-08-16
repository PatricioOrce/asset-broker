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

        public virtual DbSet<FinancialAsset> FinancialAssets { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AssetTyp__3214EC07504F727E");

                entity.ToTable("AssetType");

                entity.Property(e => e.Description).HasMaxLength(100);
            });

            modelBuilder.Entity<FinancialAsset>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Financia__3214EC07B9860FFD");

                entity.ToTable("FinancialAsset");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Ticker).HasMaxLength(10);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.AssetType).WithMany(p => p.FinancialAssets)
                    .HasForeignKey(d => d.AssetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Financial__Asset__4D94879B");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07A85C14F2");

                entity.ToTable("Order");

                entity.Property(e => e.Operation)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Order__StatusId__5070F446");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC075876835D");

                entity.ToTable("OrderStatus");

                entity.Property(e => e.Description).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
