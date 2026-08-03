using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

internal class ProductsManagerHelper
{
    private static int textPadding = 5;

    #region Input Product Info
    internal static InputResult WriteTheNameOfTheProduct(List<Product> productsList, Product productToEdit)
    {
        while (true)
        {
            Console.Write("write the name of the product: ");
            string nameInput = Console.ReadLine().Trim();

            //tar bort för många mellanrum
            nameInput = string.Join(" ", nameInput.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (string.IsNullOrWhiteSpace(nameInput)) { Helpers.ThrowErrorMessage("Cannot enter empty string, try again"); continue; }

            if (string.Equals(nameInput, "q", StringComparison.OrdinalIgnoreCase)) { return InputResult.ReturnToMainMenu; }
            if (string.Equals(nameInput, "r", StringComparison.OrdinalIgnoreCase)) { return InputResult.RestartCurrentProcess; }

            if (productsList.Any(product => product.Name.Equals(nameInput, StringComparison.OrdinalIgnoreCase) && product != productToEdit))
            { Helpers.ThrowErrorMessage("A product with that name already exists, try again"); continue; }

            if (nameInput.Length < 2) { Helpers.ThrowErrorMessage("Name is too short, try again"); continue; }

            if (nameInput.Length > 30) { Helpers.ThrowErrorMessage("Name is too long, try again"); continue; }

            //tillåtna tecken är bokstav, siffra, mellanrum, och bindestreck
            bool validCharacters = nameInput.All(character => char.IsLetterOrDigit(character) || character == ' ' || character == '-');
            if (!validCharacters) { Helpers.ThrowErrorMessage("Invalid characters in product name"); continue; }

            productToEdit.Name = nameInput;
            return InputResult.ContinueWithProcess; 
        }
    }
    internal static InputResult WriteTheQuantityOfTheProduct(List<Product> productsList, Product productToEdit)
    {
        while (true)
        {
            Console.Write("Write the quantity of the product: ");
            string quantityInput = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(quantityInput)) { Helpers.ThrowErrorMessage("Cannot enter empty string, try again"); continue; }

            if (string.Equals(quantityInput, "q", StringComparison.OrdinalIgnoreCase)) { return InputResult.ReturnToMainMenu; }
            if (string.Equals(quantityInput, "r", StringComparison.OrdinalIgnoreCase)) { return InputResult.RestartCurrentProcess; }

            bool isIntBool = int.TryParse(quantityInput, out int quantityInt);
            if (!isIntBool) { Helpers.ThrowErrorMessage("The products quantity must be a number, try again"); continue; }

            if (quantityInt > 100000) { Helpers.ThrowErrorMessage("Quantity is too large, try again"); continue; }

            if (quantityInt < 0) { Helpers.ThrowErrorMessage("The quantity can't be less than 0, try again"); continue; }

            productToEdit.Quantity = quantityInt;

            return InputResult.ContinueWithProcess;
        }
    }
    internal static InputResult WriteThePriceOfTheProduct(List<Product> productsList, Product productToEdit)
    {
        while (true)
        {
            Console.Write("Write the price of the product: ");
            string priceInput = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(priceInput)) { Helpers.ThrowErrorMessage("Cannot enter empty string, try again"); continue; }

            if (string.Equals(priceInput, "q", StringComparison.OrdinalIgnoreCase)) { return InputResult.ReturnToMainMenu; }
            if (string.Equals(priceInput, "r", StringComparison.OrdinalIgnoreCase)) { return InputResult.RestartCurrentProcess; }

            bool isDecimalBool = decimal.TryParse(priceInput, out decimal price);
            if (!isDecimalBool) { Helpers.ThrowErrorMessage("The products price must be a number, try again"); continue; }

            if (price > 100000) { Helpers.ThrowErrorMessage("The price is too large, try again"); continue; }

            if (price <= 0) { Helpers.ThrowErrorMessage("Price must be greater than 0, try again"); continue; }

            //kollar att det inte är mera än 2 decimaler i siffran
            if (decimal.Round(price, 2) != price) { Helpers.ThrowErrorMessage("Price can only have 2 decimal places, try again"); continue; }

            productToEdit.Price = price;

            return InputResult.ContinueWithProcess; 
        }
    }
    
    #endregion

    #region Display Product
    internal static void DisplayAllProductsInList(List<Product> productsList, ConsoleColor choosenColor)
    {
        int codePadding = Math.Max( "Product Code".Length, productsList.Max(product => product.ProductCode.Length)) + textPadding;
        int namePadding = Math.Max("Name".Length, productsList.Max(product => product.Name.Length)) + textPadding;
        int quantityPadding = Math.Max("Quantity".Length, productsList.Max(product => product.Quantity.ToString().Length)) + textPadding;

        Console.WriteLine(DisplayProductHeader(codePadding, quantityPadding, namePadding));

        Console.ForegroundColor = choosenColor;
        foreach (Product product in productsList.OrderBy(product => product.ProductCode).ThenBy(product => product.Price))
        {
            Console.WriteLine(product.ConvertProductToString(product, quantityPadding, namePadding, codePadding));
        }
        Console.ResetColor();
    }
    internal static void DisplaySingleProduct(Product productToDisplay)
    {
        int codePadding = Math.Max("Product Code".Length, productToDisplay.ProductCode.Length) + textPadding;
        int namePadding = Math.Max("Name".Length, productToDisplay.Name.Length) + textPadding;
        int quantityPadding = Math.Max("Quantity".Length, productToDisplay.Quantity.ToString().Length) + textPadding;

        Console.WriteLine(DisplayProductHeader(codePadding, quantityPadding, namePadding));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(productToDisplay.ConvertProductToString(productToDisplay, quantityPadding, namePadding, codePadding));
        Console.ResetColor();
    }
    private static string DisplayProductHeader(int codePadding, int quantityPadding, int namePadding)
    {
        return "Product Code".PadRight(codePadding) +
               "Name".PadRight(namePadding) +
               "Quantity".PadRight(quantityPadding) +
               "Price";
    }
    internal static void SuccessfullyEditedProducted(Product product)
    {
        Console.Clear();
        Helpers.ThrowSuccessMessage("The product was updated successfully.");
        Console.WriteLine();
    }
    #endregion
}

public enum InputResult
{
    ContinueWithProcess,
    ReturnToMainMenu,
    RestartCurrentProcess
}