using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public void SaveData(Vector3 playerPos,string sceneName)
    {
        var data = new saveData
        {
            playerPosition = playerPos,
            sceneName = sceneName,
            sceneData = CollectSceneData(),
        };
        
        SaveSystem.SaveGame(data);
        
    }
    
    public void loadData()
    {
        saveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            SceneManager.LoadScene(data.sceneName);
            
        }
    }


    //this will save the data from the objects in the scene.
    public SerializableDictionary<string, bool> CollectSceneData()
    {
        return null;
    }
}