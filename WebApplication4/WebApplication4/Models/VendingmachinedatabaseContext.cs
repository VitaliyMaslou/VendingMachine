using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication4.Models
{
    public partial class VendingmachinedatabaseContext : DbContext
    {
        
        public VendingmachinedatabaseContext()
        {
        }

        public VendingmachinedatabaseContext(DbContextOptions<VendingmachinedatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<VendingMachine> VendingMachines { get; set; }
        public virtual DbSet<VendingMachineCoin> VendingMachineCoins { get; set; }
        public virtual DbSet<VendingMachineDrink> VendingMachineDrinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Vending machine data base;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<VendingMachine>(entity =>
            {
                entity.ToTable("Vending Machines");

                entity.Property(e => e.SecretCode)
                    .HasMaxLength(60)
                    .HasColumnName("Secret Code");
            });

            modelBuilder.Entity<VendingMachineCoin>(entity =>
            {
                entity.ToTable("Vending Machine Coins");

                entity.Property(e => e.CoinsId).HasColumnName("Coins Id");

                entity.Property(e => e.VendingMachineId).HasColumnName("Vending Machine Id");

                entity.HasOne(d => d.Coins)
                    .WithMany(p => p.VendingMachineCoins)
                    .HasForeignKey(d => d.CoinsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vending M__Coins__4316F928");

                entity.HasOne(d => d.VendingMachine)
                    .WithMany(p => p.VendingMachineCoins)
                    .HasForeignKey(d => d.VendingMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vending M__Vendi__4222D4EF");
            });

            modelBuilder.Entity<VendingMachineDrink>(entity =>
            {
                entity.ToTable("Vending Machine Drinks");

                entity.Property(e => e.DrinksId).HasColumnName("Drinks Id");

                entity.Property(e => e.VendingMachineId).HasColumnName("Vending Machine Id");

                entity.HasOne(d => d.Drinks)
                    .WithMany(p => p.VendingMachineDrinks)
                    .HasForeignKey(d => d.DrinksId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vending M__Drink__3D5E1FD2");

                entity.HasOne(d => d.VendingMachine)
                    .WithMany(p => p.VendingMachineDrinks)
                    .HasForeignKey(d => d.VendingMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vending M__Vendi__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
