using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public string playerName;
    public string highestScorePlayerName;
    public int highestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highestScore;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.playerName = highestScorePlayerName;
        data.highestScore = highestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestScorePlayerName = data.playerName;
            highestScore = data.highestScore;
        }
    }
}
