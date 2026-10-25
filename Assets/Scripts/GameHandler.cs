using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public int[] playerScores;
    public Level[] levels;
    [HideInInspector] public int completedLevels = 0;

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
        foreach(int i in playerScores)
        {
            playerScores[i] = -1;
        }
    }

    void Start()
    {

    }

    public void RecordScore(int levelNum, int score)
    {
        if(playerScores[levelNum] == -1)
        {
            completedLevels++;
        }
        playerScores[levelNum] = score;
    }

    public int FindTier(int levelNum)
    {
        Level currLevel = levels[levelNum];
        if(playerScores[levelNum] <= currLevel.threeStarThreshold)
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
}
