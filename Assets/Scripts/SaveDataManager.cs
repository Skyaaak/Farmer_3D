using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveInventoryManager
{
    public static void SaveJsonData(IEnumerable<ISaveable> a_Saveables)
    {
        Inventory sd = new Inventory();
        foreach (var saveable in a_Saveables)
        {
            saveable.PopulateInventory(sd);
        }

        if (FileManager.WriteToFile("Inventory.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }

    public static void LoadJsonData(IEnumerable<ISaveable> a_Saveables)
    {
        if (FileManager.LoadFromFile("Inventory.dat", out var json))
        {
            Inventory sd = new Inventory();
            sd.LoadFromJson(json);

            foreach (var saveable in a_Saveables)
            {
                saveable.LoadFromInventory(sd);
            }

            Debug.Log("Load complete");
        }
    }
}
