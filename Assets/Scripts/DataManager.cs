using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameManager gameManager;
    public void SaveGame()
    {
        playerData = new PlayerData();
        playerData.illuUnlocked = GameManager.Instance.illuUnlocked;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            GameManager.Instance.illuUnlocked = loadedData.illuUnlocked;
        }
        else
        {
            Debug.LogWarning("File not found !");
        }
    }
}
