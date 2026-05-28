ProductsManager inventory = new ProductsManager();
SaveLoadSystem.LoadProductsList(inventory);

Helpers.ShowMainMenuText();

bool isRunning = true;

while (isRunning)
{
    Console.Write("Enter a Number: ");
    string userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput)) { Helpers.ThrowErrorMessage("Input cannot be empty, try again"); continue; }

    Console.Clear();

    switch (userInput)
    {
        case "1":
            inventory.AddProduct();
            break;
        case "2":
            inventory.UpdateProduct();
            break;
        case "3":
            inventory.DeleteProduct();
            break;
        case "4":
            inventory.ViewProducts();
            break;
        case "5":
            inventory.GenerateReport();
            break;
        case "0":
            isRunning = false;
            break;
        default:
            Console.Clear();
            Helpers.ThrowErrorMessage("Invalid input, try again");
            Helpers.ShowMainMenuText();
            break;
    }
}

SaveLoadSystem.SaveProductsList(inventory);