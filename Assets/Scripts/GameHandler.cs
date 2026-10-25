using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public int[] playerScores;
    public Level[] levels;
    public int completedLevels = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerScores = new int[levels.Length];
        
        for(int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = -1;
        }
        SaveSystem.Load();
    }

    public void RecordScore(int levelNum, int score)
    {
        if(playerScores[levelNum] == -1)
        {
            completedLevels++;
        }
        if(playerScores[levelNum] > score || playerScores[levelNum] == -1)
        {
            playerScores[levelNum] = score;
        }
        SaveSystem.Save();
    }

    public int FindTier(int levelNum)
    {
        Level currLevel = levels[levelNum];
        if(playerScores[levelNum] == -1)
        {
            return 0;
        }
        else if(playerScores[levelNum] <= currLevel.threeStarThreshold)
        {
            return 3;
        }
        else if(playerScores[levelNum] <= currLevel.twoStarThreshold)
        {
            return 2;
        }
        else if(playerScores[levelNum] <= currLevel.oneStarThreshold)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int GetThreeStarThreshold(int levelNum)
    {
        return levels[levelNum].threeStarThreshold;
    }

    public void Save(ref GameSaveData data)
    {
        data.playerScores = playerScores;
        data.completedLevels = completedLevels;
    }

    public void Load(GameSaveData data)
    {
        playerScores = data.playerScores;
        completedLevels = data.completedLevels;
    }
}

[System.Serializable]
    public struct GameSaveData
    {
        public int[] playerScores;
        public int completedLevels;
    }