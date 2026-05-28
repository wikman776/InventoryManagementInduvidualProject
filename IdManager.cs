using System;
using System.Collections.Generic;
using System.Text;

internal class IdManager
{
    private static int NextId = 1;
    internal static InputResult WriteTheIdOfTheProduct(List<Product> productsList, Product productToEdit)
    {
        while (true)
        {
            Console.Write("Write a new product ID in the format P0001: ");

            string idInput = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(idInput)) { Helpers.ThrowErrorMessage("Cannot enter empty string, try again"); continue; }

            if (string.Equals(idInput, "Q", StringComparison.OrdinalIgnoreCase)) { return InputResult.ReturnToMainMenu; }

            if (string.Equals(idInput, "R", StringComparison.OrdinalIgnoreCase)) { return InputResult.RestartCurrentProcess; }

            if (!idInput.StartsWith("P")) { Helpers.ThrowErrorMessage("ID must start with P, try again"); continue; }

            string numberPart = idInput.Substring(1);

            if (numberPart.Length != 4) { Helpers.ThrowErrorMessage("ID must be in format P0001, try again"); continue;  }

            if (!numberPart.All(char.IsDigit)) { Helpers.ThrowErrorMessage("ID must contain P followed by 4 digits, try again"); continue;  }

            if (productsList.Any(product => product.ID == idInput && product != productToEdit)) { Helpers.ThrowErrorMessage("A product with that ID already exists, try again"); continue; }

            productToEdit.ID = idInput;

            return InputResult.ContinueWithProcess;
        }
    }
    internal static Product? SearchForProductById(List<Product> productsList)
    {
        while (true)
        {
            Console.Write("Product ID: ");
            string input = Console.ReadLine().Trim();
            input = input.ToUpper();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) { Console.Clear(); Helpers.ShowMainMenuText(); return null; }

            if (!input.StartsWith("P", StringComparison.OrdinalIgnoreCase)) { Helpers.ThrowErrorMessage("ID must start with P, try again"); continue; }

            string numberPart = input.Substring(1);

            if (!numberPart.All(char.IsDigit)){ Helpers.ThrowErrorMessage("ID must contain P followed by numbers only, try again"); continue;  }

            if (numberPart.Length != 4)  { Helpers.ThrowErrorMessage("ID must be in format P0001, try again"); continue; }

            Product? foundProduct = productsList.FirstOrDefault(product => product.ID == input);

            if (foundProduct == null) { Helpers.ThrowErrorMessage("No product with that ID was found, try again"); continue; }

            Console.WriteLine("Product found:");
            ProductsManagerHelper.DisplaySingleProduct(foundProduct);

            return foundProduct;
        }
    }
    internal static string GenerateProductId(List<Product> productsList)
    {
        string newId = "P" + NextId.ToString("D4");

        while (productsList.Any(product => product.ID == newId))
        {
            NextId++;
            newId = "P" + NextId.ToString("D4");
        }

        NextId++;

        return newId;
    }
    internal static void SetNextId(ProductListSaveData loadedSaveData)
    {
        NextId = loadedSaveData.NextID;
    }
    internal static void GetNextId(ProductListSaveData loadedSaveData)
    {
        loadedSaveData.NextID = NextId;
    }
}
