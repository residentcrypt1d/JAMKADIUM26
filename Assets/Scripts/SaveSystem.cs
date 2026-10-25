using UnityEngine;
using System.IO;

public class SaveSystem
{
    private static SaveData _saveData = new SaveData();
    [System.Serializable]
    public struct SaveData
    {
        public GameSaveData GameData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        GameHandler.instance.Save(ref _saveData.GameData);
    }

    public static void Load()
    {
        string saveContent;
        try
        {
            saveContent = File.ReadAllText(SaveFileName());
        }
        catch
        {
            return;
        }
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        GameHandler.instance.Load(_saveData.GameData);
    }
}
