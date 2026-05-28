using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

internal class SaveLoadSystem
{
    internal static void LoadProductsList(ProductsManager inventory)
    {
        string filePath = GetJsonFilePath();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("JSON file path: " + Path.GetFullPath(filePath));
            Helpers.ThrowErrorMessage("Could not find save file");
            return;
        }

        string json = File.ReadAllText(filePath);

        ProductListSaveData? loadedSaveData = JsonSerializer.Deserialize<ProductListSaveData>(json);

        if (loadedSaveData == null) { Helpers.ThrowErrorMessage("Failed to load product list"); return; }

        IdManager.SetNextId(loadedSaveData);

        Helpers.ThrowSuccessMessage("Product list loaded successfully"); 
        inventory.productsList = loadedSaveData.ProductsList;
    }
    internal static void SaveProductsList(ProductsManager inventory)
    {
        ProductListSaveData saveData = new ProductListSaveData();
        saveData.ProductsList = inventory.productsList;
        IdManager.GetNextId(saveData);

        string jsonToSave = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(GetJsonFilePath(), jsonToSave);

        Console.WriteLine("Products list saved to location:");
        Console.WriteLine(Path.GetFullPath(GetJsonFilePath()));
    }
    internal static string GetJsonFilePath()
    {
        string filePath = "products.json";
        return filePath;
    }
    
}
internal class ProductListSaveData
{
    public List<Product> ProductsList { get; set; } = new();
    public int NextID { get; set; }
}