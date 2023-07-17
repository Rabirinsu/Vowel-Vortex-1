using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [Header("LEVEL")] 
    private int currentlevelID;
    public static Level currentLevel;
    private Dictionary<int, Level> Levels = new();
    [SerializeField] private Word word;
    private List<Obstacle> obstacles;
    private float minobstaclepawnDelay;
    private float maxobstaclespawnDelay;
    [Header("CORE")] 
    [SerializeField] private GameData gameData;
    private int levelpassCount;

    private void InitializeLevel(Level level)
    {
        word = level.word;
        obstacles = level.obstacles;
        minobstaclepawnDelay = level.minobstaclepawnDelay;
        maxobstaclespawnDelay = level.maxobstaclespawnDelay;
        currentLevel = level;
        GameManager.Instance.currentWord = word;
    }
    
    private void Awake()
    {
        LoadAllLevels();
        GameManager.Instance.LoadGame();
    }
       
    public void LevelUp()
    {
          currentlevelID++;
          gameData.currentlevelID = currentlevelID;
    }

    private void LoadAllLevels()
    {
        var levels = Resources.LoadAll<Level>("SO/Levels");
        foreach (var level in levels)
        {
            Levels.Add(level.ID, level);
        }
        currentlevelID = gameData.currentlevelID;
        LoadLevel();
    }
   
    public void LevelDecrease()
    {
         currentlevelID--;
         gameData.currentlevelID = currentlevelID;
    }
    
    public void LevelReset()
    {
        currentlevelID = 1;
        gameData.currentlevelID = currentlevelID;
    }

    public void LoadLevel()
    {
        if (Levels.TryGetValue(currentlevelID, out var level))
        {
            InitializeLevel(level);
            Debug.Log("LEVEL INITIALIZED" + level.ID);
        }
        else
        {
            // levels Can not found 
            currentlevelID = 1;
            InitializeLevel(level);
        }
    }
        
 }
