using System;
using System.Collections.Generic;
using System.Text;

internal class Product
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    internal string ConvertProductToString(Product product, int quantityPadding, int namePadding, int idPadding)
    {
        return
            product.ID.ToString().PadRight(idPadding) +
            product.Name.PadRight(namePadding) +
            product.Quantity.ToString().PadRight(quantityPadding) +
            product.Price.ToString("0.00");
    }
}