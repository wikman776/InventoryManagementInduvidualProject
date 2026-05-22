using System;
using System.Collections.Generic;
using System.Text;

internal class Helpers
{
    internal static void ShowMainMenuText()
    {
        Console.WriteLine("Enter a Number");
        Console.WriteLine("1-Add Product");
        Console.WriteLine("2-Update Product");
        Console.WriteLine("3-Delete Product");
        Console.WriteLine("4-View Products");
        Console.WriteLine("5-Generate Report");
        Console.WriteLine("0-Quit");
    }
    internal static void ThrowSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    internal static void ThrowErrorMessage(string errorString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorString);
        Console.ResetColor();
    }
    internal static void WhaitForPressAnyKeyInput()
    {
        Console.WriteLine();
        Console.Write("Press any key to return to the main menu: ");
        Console.ReadKey(true);
    }
}