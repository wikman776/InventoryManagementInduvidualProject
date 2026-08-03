using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

internal class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }// Berättar för Entity Framework att Product ska lagras som en tabell i databasen.
    //En DbSet<T> är Entity Frameworks motsvarighet till en List<T>, men där innehållet ligger i en databas istället för i datorns RAM-minne.

    //On configuring gör = "När du ska prata med databasen, använd denna SQL Server."
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //berättar var databasen ligger
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InventoryManagementDb;Trusted_Connection=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(product => product.ProductCode).IsUnique();

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, ProductCode = "P0001", Name = "Laptop", Quantity = 8, Price = 899.99m },
            new Product { Id = 2, ProductCode = "P0002", Name = "Wireless Mouse", Quantity = 25, Price = 29.99m },
            new Product { Id = 3, ProductCode = "P0003", Name = "Mechanical Keyboard", Quantity = 15, Price = 79.99m },
            new Product { Id = 4, ProductCode = "P0004", Name = "Monitor", Quantity = 12, Price = 249.99m },
            new Product { Id = 5, ProductCode = "P0005", Name = "USB-C Cable", Quantity = 50, Price = 14.99m },
            new Product { Id = 6, ProductCode = "P0006", Name = "Office Chair", Quantity = 6, Price = 199.99m },
            new Product { Id = 7, ProductCode = "P0007", Name = "Webcam", Quantity = 18, Price = 59.99m },
            new Product { Id = 8, ProductCode = "P0008", Name = "External SSD", Quantity = 10, Price = 119.99m },
            new Product { Id = 9, ProductCode = "P0009", Name = "Headset", Quantity = 20, Price = 69.99m },
            new Product { Id = 10, ProductCode = "P0010", Name = "Desk Lamp", Quantity = 14, Price = 39.99m }
        );
    }
    //ModelBuilder är "Verktyget jag använder för att beskriva hur databasen ska byggas."
}