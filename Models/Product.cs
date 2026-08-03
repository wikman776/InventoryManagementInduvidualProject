using System;
using System.Collections.Generic;
using System.Text;

internal class Product
{
    public int Id { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    internal string ConvertProductToString(Product product, int quantityPadding, int namePadding, int codePadding)
    {
        return
            product.ProductCode.PadRight(codePadding) +
            product.Name.PadRight(namePadding) +
            product.Quantity.ToString().PadRight(quantityPadding) +
            product.Price.ToString("0.00");
    }
}