using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public Vector3Data playerPosition;
    public int health;
    public int exp;
}

[System.Serializable]
public class Vector3Data
{
    public float x, y, z;

    public Vector3Data(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

public class SaveLoadManager : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/savegame.json";
    }

    public void SaveGame(string sceneName, Vector3 position, int health, int exp)
    {
        SaveData saveData = new SaveData
        {
            sceneName = sceneName,
            playerPosition = new Vector3Data(position),
            health = health,
            exp = exp
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("게임 저장 완료! 저장 경로: " + savePath);
    }

    public bool LoadGame(out string sceneName, out Vector3 position, out int health, out int exp)
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            sceneName = saveData.sceneName;
            position = saveData.playerPosition.ToVector3();
            health = saveData.health;
            exp = saveData.exp;

            return true;
        }

        sceneName = "";
        position = Vector3.zero;
        health = 0;
        exp = 0;
        return false;
    }
}
