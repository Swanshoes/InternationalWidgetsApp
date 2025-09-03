using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace InternationalWidgetsApp.Models;

public partial class InternationalWidgetsDbContext : DbContext
{
    public InternationalWidgetsDbContext()
    {
    }

    public InternationalWidgetsDbContext(DbContextOptions<InternationalWidgetsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=international_widgets_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customerID");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(150)
                .HasColumnName("city");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customerName");
            entity.Property(e => e.State)
                .HasMaxLength(150)
                .HasColumnName("state");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PRIMARY");

            entity.ToTable("invoices");

            entity.HasIndex(e => e.CustomerId, "customerID");

            entity.Property(e => e.InvoiceId)
                .HasColumnType("int(11)")
                .HasColumnName("invoiceID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customerID");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoiceDate");
            entity.Property(e => e.InvoiceTotal)
                .HasPrecision(10, 2)
                .HasColumnName("invoiceTotal");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_ibfk_1");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId).HasName("PRIMARY");

            entity.ToTable("invoice_items");

            entity.HasIndex(e => e.InvoiceId, "invoiceID");

            entity.HasIndex(e => e.ItemId, "itemID");

            entity.Property(e => e.InvoiceItemId)
                .HasColumnType("int(11)")
                .HasColumnName("invoiceItemID");
            entity.Property(e => e.InvoiceId)
                .HasColumnType("int(11)")
                .HasColumnName("invoiceID");
            entity.Property(e => e.ItemId)
                .HasColumnType("int(11)")
                .HasColumnName("itemID");
            entity.Property(e => e.LineAmount)
                .HasPrecision(10, 2)
                .HasColumnName("lineAmount");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_items_ibfk_1");

            entity.HasOne(d => d.Item).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_items_ibfk_2");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("items");

            entity.Property(e => e.ItemId)
                .HasColumnType("int(11)")
                .HasColumnName("itemID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unitPrice");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
