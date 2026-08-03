using System;
using System.Collections.Generic;
using System.Text;

internal class ProductsManager
{
    private readonly ProductRepository productRepository = new ProductRepository();
    public List<Product> productsList;
    internal ProductsManager() { productsList = productRepository.GetAllProducts(); }

    internal void AddProduct()
    {
        while (true)
        {
            Console.WriteLine("To add a new product to the list, follow these steps.");
            Console.WriteLine("Enter 'Q' to return to the main menu, or 'r' to restart the process.");

            Product newProduct = new Product();
            
            InputResult nameResult = ProductsManagerHelper.WriteTheNameOfTheProduct(productsList, newProduct);
            if (nameResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (nameResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }
            
            InputResult quantityResult = ProductsManagerHelper.WriteTheQuantityOfTheProduct(productsList, newProduct);
            if (quantityResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (quantityResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }
            
            InputResult priceResult = ProductsManagerHelper.WriteThePriceOfTheProduct(productsList, newProduct);
            if (priceResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
            else if (priceResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue;  }

            newProduct.ProductCode = ProductCodeManager.GenerateProductCode(productsList);
            productRepository.AddProduct(newProduct);
            productsList = productRepository.GetAllProducts();

            Console.Clear();
            ProductsManagerHelper.DisplaySingleProduct(newProduct);
            Helpers.ThrowSuccessMessage("The product has been successfully added to the list!");
            Console.WriteLine();
        }
    }
    internal void UpdateProduct()
    {
        if (productsList.Count == 0) { Helpers.ShowEmptyListErrorMessage(); return; }

        while (true)
        {
            ProductsManagerHelper.DisplayAllProductsInList(productsList, ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("Write the exact product code of a product to update its code, name, quantity, or price");
            Console.WriteLine("Or write 'Q' to return to the main menu");

            Product? productToEdit = ProductCodeManager.SearchForProductByCode(productsList);

            if (productToEdit == null) { return; }

            Console.Clear();
            bool searchForAnotherProduct = false;

            while (searchForAnotherProduct == false)
            {
                ProductsManagerHelper.DisplaySingleProduct(productToEdit);
                Console.WriteLine();

                Console.WriteLine("Which part of the product do you want to update?");
                Console.WriteLine("1 - Product Code");
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
                        InputResult idResult = ProductCodeManager.WriteTheProductCode(productsList, productToEdit);
                        if (idResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (idResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue; }
                        else { SaveUpdatedProduct(productToEdit); }
                        break;
                    case "2":
                        InputResult nameResult = ProductsManagerHelper.WriteTheNameOfTheProduct(productsList, productToEdit);
                        if (nameResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (nameResult == InputResult.RestartCurrentProcess) { Console.Clear(); continue; }
                        else { SaveUpdatedProduct(productToEdit); }
                        break;
                    case "3":
                        InputResult quantityResult = ProductsManagerHelper.WriteTheQuantityOfTheProduct(productsList, productToEdit);
                        if (quantityResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (quantityResult == InputResult.RestartCurrentProcess) { Console.Clear();  continue; }
                        else { SaveUpdatedProduct(productToEdit); }
                        break;
                    case "4":
                        InputResult priceResult = ProductsManagerHelper.WriteThePriceOfTheProduct(productsList, productToEdit);
                        if (priceResult == InputResult.ReturnToMainMenu) { Console.Clear(); Helpers.ShowMainMenuText(); return; }
                        else if (priceResult == InputResult.RestartCurrentProcess) { Console.Clear(); continue; }
                        else { SaveUpdatedProduct(productToEdit); }
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
                        Console.Clear();
                        Helpers.ThrowErrorMessage("Invalid selection.");
                        continue;
                }
            }
        }
    }
    internal void DeleteProduct()
    {
        if (productsList.Count == 0) { Helpers.ShowEmptyListErrorMessage(); return; }

        while (true)
        {
            ProductsManagerHelper.DisplayAllProductsInList(productsList, ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("Write the exact product code of the product you want to delete from the list");
            Console.WriteLine("Or write 'Q' to return to the main menu");

            Product? productToDelete = ProductCodeManager.SearchForProductByCode(productsList);

            if (productToDelete == null) { return; }

            Console.Clear();

            while(true)
            {
                ProductsManagerHelper.DisplaySingleProduct(productToDelete);
                Console.Write("Are you sure you want to delete this product? (y/n): ");
                string confirmInput = Console.ReadLine().Trim();

                if (string.Equals(confirmInput, "y", StringComparison.OrdinalIgnoreCase))
                {
                    productRepository.DeleteProduct(productToDelete.Id);
                    productsList = productRepository.GetAllProducts();
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
        if (productsList.Count == 0) { Helpers.ShowEmptyListErrorMessage(); return; }

        Console.WriteLine("All products in list:");
        ProductsManagerHelper.DisplayAllProductsInList(productsList, ConsoleColor.Green);

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

        foreach (Product product in productsList.OrderBy(product => product.Id))
        {
            decimal productTotalValue = product.Price * product.Quantity;

            report += product.Id + " - " +
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
    private void SaveUpdatedProduct(Product product)
    {
        productRepository.UpdateProduct(product);
        productsList = productRepository.GetAllProducts();
        ProductsManagerHelper.SuccessfullyEditedProducted(product);
    }
}