using System;
using System.Collections.Generic;
using System.Text;

internal class Inventory
{
    public List<Product> productsList = new List<Product>();

    internal void AddProduct()
    {
        while (true)
        {
            Console.WriteLine("To add a new product to the list, follow these steps.");
            Console.WriteLine("Enter 'Q' to return to the main menu, or 'r' to restart the process.");

            Product newProduct = new Product();

            InputResult idResult = ProductsManager.WriteTheIdOfTheProduct(productsList, newProduct);
            if (idResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (idResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }
            
            InputResult nameResult = ProductsManager.WriteTheNameOfTheProduct(productsList, newProduct);
            if (nameResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (nameResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }
            
            InputResult quantityResult = ProductsManager.WriteTheQuantityOfTheProduct(productsList, newProduct);
            if (quantityResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (quantityResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }
            
            InputResult priceResult = ProductsManager.WriteThePriceOfTheProduct(productsList, newProduct);
            if (priceResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (priceResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }

            productsList.Add(newProduct);
            Console.Clear();
            Helpers.ThrowSuccessMessage("The product has been successfully added to the list!");
        }
    }
    internal void UpdateProduct()
    {
        while(true)
        {
            Console.WriteLine("Write the exact ID of a product to update its ID, name, quantity, or price");
            Console.WriteLine("Or write 'Q' to return to the main menu");

            Product? productToEdit = SearchForProductById();

            if (productToEdit == null) { return; }

            bool searchForAnotherProduct = false;

            while (searchForAnotherProduct == false)
            {
                Console.WriteLine();
                Console.WriteLine("Which part of the product do you want to update?");
                Console.WriteLine("1 - ID");
                Console.WriteLine("2 - Name");
                Console.WriteLine("3 - Quantity");
                Console.WriteLine("4 - Price");
                Console.WriteLine("Q - Return to main menu");
                Console.WriteLine("R - Return to previous section");
                Console.Write("Input: ");

                string newInput = Console.ReadLine().Trim();

                switch (newInput)
                {
                    case "1":
                        InputResult idResult = ProductsManager.WriteTheIdOfTheProduct(productsList, productToEdit);
                        if (idResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (idResult == InputResult.RestartCurrentProcess) { Console.Clear(); ProductsManager.DisplaySingleProduct(productToEdit); continue; }
                        ProductsManager.SuccessfullyEditedProducted(productToEdit);
                        break;
                    case "2":
                        InputResult nameResult = ProductsManager.WriteTheNameOfTheProduct(productsList, productToEdit);
                        if (nameResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (nameResult == InputResult.RestartCurrentProcess) { Console.Clear(); ProductsManager.DisplaySingleProduct(productToEdit); continue; }
                        ProductsManager.SuccessfullyEditedProducted(productToEdit);

                        break;
                    case "3":
                        InputResult quantityResult = ProductsManager.WriteTheQuantityOfTheProduct(productsList, productToEdit);
                        if (quantityResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (quantityResult == InputResult.RestartCurrentProcess) { Console.Clear(); ProductsManager.DisplaySingleProduct(productToEdit); continue; }
                        ProductsManager.SuccessfullyEditedProducted(productToEdit);

                        break;
                    case "4":
                        InputResult priceResult = ProductsManager.WriteThePriceOfTheProduct(productsList, productToEdit);
                        if (priceResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (priceResult == InputResult.RestartCurrentProcess) { Console.Clear(); ProductsManager.DisplaySingleProduct(productToEdit); continue; }
                        ProductsManager.SuccessfullyEditedProducted(productToEdit);
                        break;
                    case "q":
                    case "Q":
                        Console.Clear();
                        Helpers.ShowMainMenuText();
                        return;
                    case "r":
                    case "R":
                        Console.Clear();
                        searchForAnotherProduct = true;
                        break;
                    default:
                        Helpers.ThrowErrorMessage("Invalid selection.");
                        continue;
                }
            }
        }
    }
    internal void DeleteProduct()
    {
        while (true)
        {
            Console.WriteLine("Write the exact ID of the product you want to delete from the list");
            Console.WriteLine("Or write 'Q' to return to the main menu");

            Product? productToDelete = SearchForProductById();

            if (productToDelete == null) { return; }

            while(true)
            {
                Console.Write("Are you sure you want to delete this product? (y/n): ");
                string confirmInput = Console.ReadLine().Trim();

                if (string.Equals(confirmInput, "y", StringComparison.OrdinalIgnoreCase))
                {
                    productsList.Remove(productToDelete);
                    Console.Clear();
                    Helpers.ThrowSuccessMessage("Product was successfully deleted");
                    break;
                }
                else if (string.Equals(confirmInput, "n", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Delete process cancelled.");
                    break;
                }
                else
                {
                    Console.Clear();
                    Helpers.ThrowErrorMessage("Invalid input, input must be 'y' or 'n'");
                    continue;
                }
            }
        }
    }
    internal void ViewProducts()
    {
        if (productsList.Count == 0)
        {
            Helpers.ThrowErrorMessage("There are no products in the list");
            Helpers.WhaitForPressAnyKeyInput();
            Console.Clear();
            Helpers.ShowMainMenuText();
            return;
        }

        Console.WriteLine("All products in list:");
        ProductsManager.DisplayAllProductsInList(productsList);

        Helpers.WhaitForPressAnyKeyInput();
        Console.Clear();
        Helpers.ShowMainMenuText();
        return;
    }
    internal void GenerateReport()
    {
        if (productsList.Count == 0)
        {
            Helpers.ThrowErrorMessage("There are no products in the list");
            Helpers.WhaitForPressAnyKeyInput();
            Console.Clear();
            Helpers.ShowMainMenuText();
            return;
        }

        decimal totalInventoryValue = productsList.Sum(product => product.Price * product.Quantity);
        int totalProductTypes = productsList.Count;
        int totalQuantity = productsList.Sum(product => product.Quantity);

        Product highestStockProduct = productsList.OrderByDescending(product => product.Quantity).First();
        Product lowestStockProduct = productsList.OrderBy(product => product.Quantity).First();

        string report = "";

        report += "Inventory Report\n";
        report += "----------------\n";
        report += "Total inventory value = " + totalInventoryValue.ToString("0.00") + "\n";
        report += "Total product types = " + totalProductTypes + "\n";
        report += "Total quantity of all products = " + totalQuantity + "\n";
        report += "Highest stock product: " + highestStockProduct.Name + " = " + highestStockProduct.Quantity + "\n";
        report += "Lowest stock product: " + lowestStockProduct.Name + " = " + lowestStockProduct.Quantity + "\n\n";

        report += "Inventory value per product:\n";

        foreach (Product product in productsList.OrderBy(product => product.ID))
        {
            decimal productTotalValue = product.Price * product.Quantity;

            report += product.ID + " - " +
                      product.Name + ": " +
                      product.Quantity + " x " +
                      product.Price.ToString("0.00") + " = " +
                      productTotalValue.ToString("0.00") + "\n";
        }

        Console.WriteLine(report);

        Console.WriteLine();
        Console.WriteLine("Enter 'S' to save the report to a text file and return to the main menu.");
        Console.WriteLine("Or enter any other key to return to directly return to the main menu.");

        string input = Console.ReadLine().Trim();

        if (string.Equals(input, "s", StringComparison.OrdinalIgnoreCase))
        {
            File.WriteAllText("inventory_report.txt", report);

            Console.Clear();
            Helpers.ThrowSuccessMessage("Report saved to inventory_report.txt");
            Helpers.ShowMainMenuText();
        }
        else
        {
            Console.Clear();
            Helpers.ShowMainMenuText();
        }

        
    }

    #region List Helpers
    internal Product? SearchForProductById()
    {
        while (true)
        {
            Console.Write("Product ID: ");
            string input = Console.ReadLine().Trim();

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) { Console.Clear(); Helpers.ShowMainMenuText(); return null; }

            if (!input.All(char.IsDigit)) { Helpers.ThrowErrorMessage("ID must contain numbers only, try again"); continue; }

            if (input.Length != 4) { Helpers.ThrowErrorMessage("ID must be exactly 4 digits, try again"); continue; }

            Product? foundProduct = productsList.FirstOrDefault(product => product.ID == input);

            if (foundProduct == null) { Helpers.ThrowErrorMessage("No product with that ID was found, try again"); continue; }

            Console.WriteLine("Product found:");
            ProductsManager.DisplaySingleProduct(foundProduct);

            return foundProduct;
        }
    }
    #endregion
}