using System;
using System.Collections.Generic;
using System.Text;

internal class ProductCodeManager
{
    internal static InputResult WriteTheProductCode(List<Product> productsList, Product productToEdit)
    {
        while (true)
        {
            Console.Write("Write a new product code in the format 'P0001': ");

            string input = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(input)) { Helpers.ThrowErrorMessage("Cannot enter empty string, try again"); continue; }

            if (string.Equals(input, "Q", StringComparison.OrdinalIgnoreCase)) { return InputResult.ReturnToMainMenu; }

            if (string.Equals(input, "R", StringComparison.OrdinalIgnoreCase)) { return InputResult.RestartCurrentProcess; }

            if (!input.StartsWith("P", StringComparison.OrdinalIgnoreCase)) { Helpers.ThrowErrorMessage("product code must start with P, try again"); continue; }

            string numberPart = input.Substring(1);

            if (numberPart.Length != 4) { Helpers.ThrowErrorMessage("product code must be in format P0001, try again"); continue;  }

            if (!numberPart.All(char.IsDigit)) { Helpers.ThrowErrorMessage("product code must contain P followed by 4 digits, try again"); continue;  }

            if (productsList.Any(product => product.ProductCode == input && product != productToEdit)) 
            { Helpers.ThrowErrorMessage("A product with that code already exists, try again"); continue; }

            productToEdit.ProductCode = input;

            return InputResult.ContinueWithProcess;
        }
    }
    internal static Product? SearchForProductByCode(List<Product> productsList)
    {
        while (true)
        {
            Console.Write("Product Code: ");
            string input = Console.ReadLine().Trim();
            input = input.ToUpper();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) { Console.Clear(); Helpers.ShowMainMenuText(); return null; }

            if (!input.StartsWith("P", StringComparison.OrdinalIgnoreCase)) { Helpers.ThrowErrorMessage("Product code must start with P, try again"); continue; }

            string numberPart = input.Substring(1);

            if (!numberPart.All(char.IsDigit)){ Helpers.ThrowErrorMessage("Product code must contain P followed by numbers only, try again"); continue;  }

            if (numberPart.Length != 4)  { Helpers.ThrowErrorMessage("Product code must be in format P0001, try again"); continue; }

            Product? foundProduct = productsList.FirstOrDefault(product => product.ProductCode == input);

            if (foundProduct == null) { Helpers.ThrowErrorMessage("No product with that product code was found, try again"); continue; }

            return foundProduct;
        }
    }
    internal static string GenerateProductCode(List<Product> productsList)
    {
        int highestNumber = productsList.Select(product =>
            {
                string code = product.ProductCode;
                if (code.Length == 5 && code.StartsWith("P") && int.TryParse(code[1..], out int number)) { return number; }
                return 0;
            })
            .DefaultIfEmpty(0)
            .Max();

        int nextNumber = highestNumber + 1;

        if (nextNumber > 9999) { throw new InvalidOperationException("No more product codes are available in the P0001–P9999 format.");}

        return $"P{nextNumber:D4}";
    }
}
