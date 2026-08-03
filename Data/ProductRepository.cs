using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

internal class ProductRepository
{
    internal List<Product> GetAllProducts()
    {
        using ProductDbContext db = new ProductDbContext();
        return db.Products.AsNoTracking().ToList();
        //db.Products, är products listan
        // "AsNoTracking()" säger att EF bara ska läsa produkterna, inte hålla reda på eventuella ändringar i dem
    }
    internal void AddProduct(Product product)
    {
        using ProductDbContext db = new ProductDbContext();
        db.Products.Add(product);
        db.SaveChanges();
    }
    internal void UpdateProduct(Product product)
    {
        using ProductDbContext db = new ProductDbContext();
        db.Products.Update(product);
        db.SaveChanges();
    }
    internal void DeleteProduct(int productId)
    {
        using ProductDbContext db = new ProductDbContext();
        Product? product = db.Products.Find(productId);
        if (product == null) { throw new InvalidOperationException("Product could not be found."); }
        db.Products.Remove(product);
        db.SaveChanges();
    }
}