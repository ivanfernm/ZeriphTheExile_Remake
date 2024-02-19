using System.IO;
using DefaultNamespace;
using UnityEngine;

public static class SaveSystem
{
    //the path were the save file will be stored
    private static string savePath = Path.Combine(Application.persistentDataPath, "savedata.json");

    //Save method
    public static void SaveGame(saveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }
    
    //Load method
    public static saveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData data = JsonUtility.FromJson<saveData>(json);
            return data;
        }
        
        return null;
        
    }
    //CreateData method
    public static saveData CreateData()
    {
        saveData data = new saveData();
        data.sceneData = new SerializableDictionary<string, bool>();
        return data;
    }
    
    //Delete method
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }

}