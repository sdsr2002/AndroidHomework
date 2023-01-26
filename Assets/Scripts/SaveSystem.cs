using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static string SaveFolder => CreateFolderAndReturnPath("/Folder");

    public static string CreateFolderAndReturnPath(string folder)
    {
        if (Directory.Exists(Application.persistentDataPath + folder))
        {
            return Application.persistentDataPath + folder + "/";
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + folder);
            return Application.persistentDataPath + folder + "/";
        }
    }

    public static void SaveArmorEquipment(Equipments equips, string SaveName)
    {
        string FullSavePath = SaveFolder + "_Equipment_" + SaveName + ".Save";
        List<int> IDs = new List<int>();

        AddIDToList(equips.Helm     , ref IDs);
        AddIDToList(equips.Shoulder , ref IDs);
        AddIDToList(equips.Chest    , ref IDs);

        int[] saveList = IDs.ToArray();

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(FullSavePath, FileMode.Create))
        {
            formatter.Serialize(stream, saveList);
            Debug.Log("Saved To: '" + FullSavePath + "'");
        }
    }

    public static void LoadArmorEquipments(ref Equipments equips, string SaveName)
    {
        string FullSavePath = SaveFolder + "_Equipment_" + SaveName + ".Save";

        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(FullSavePath))
            using (FileStream stream = new FileStream(FullSavePath, FileMode.Open))
            {
                int [] IDs = formatter.Deserialize(stream) as int[];
                for(int i = 0; i < IDs.Length; i++)
                {
                    Debug.Log(IDs[i]);
                    equips.EquipArmor(GameManager.Instance.itemDatabase.GetItemFromID(IDs[i]) as Armor); // this can break if Item found is not an Armor Class or no GameManager
                }
                Debug.Log("Load From: '" + FullSavePath + "'");
            }
    }

    private static void AddIDToList(ItemBase itembase,ref List<int> list)
    {
        if (itembase != null)
            list.Add(itembase.ID);
    }
}
