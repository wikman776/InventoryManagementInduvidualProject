ProductsManager pm = new ProductsManager();

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
            pm.AddProduct();
            break;
        case "2":
            pm.UpdateProduct();
            break;
        case "3":
            pm.DeleteProduct();
            break;
        case "4":
            pm.ViewProducts();
            break;
        case "5":
            pm.GenerateReport();
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