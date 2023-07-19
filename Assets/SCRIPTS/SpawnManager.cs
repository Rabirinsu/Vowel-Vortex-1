using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private GameObject obstacle;
     private float obstaclespawnDelay;
     [SerializeField] private float enableDelay;
    [SerializeField] private float spawnpointX;
    [SerializeField] private float spawnpointY;
    [SerializeField] private float minspawnDelay;     
    [SerializeField] private float maxspawnDelay;     
    [SerializeField] private Animator animator;
    [FormerlySerializedAs("nextLetter")] public GameObject currentLetter;
    [SerializeField] private float multiplewordspawnChance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void OnEnable()
    {
        SetLetter();
        StartCoroutine(EnableAction());
    }

    private IEnumerator EnableAction()
    {
        yield return new WaitForSeconds(enableDelay);
        InvokeRepeating("SpawnObstacles", 1, LevelManager.currentLevel.GenerateObstacleSpawnDelay());
        InvokeRepeating("SpawnCollectables", 1, LevelManager.currentLevel.GenerateCollectableSpawnDelay());
        InvokeRepeating("Spawn", GetSpawnRepeatTime(), GenerateLetterSpawnDelay());
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }
    public void Initialize()
    {
        this.enabled = true;
    }
    
    public void Stop()
    {
        this.enabled = false;
        currentLetter = null;
    }
  
   
    private void SpawnObstacles()
    {
        obstacle = LevelManager.currentLevel.GetObstacle();
        Instantiate(obstacle, new Vector2(Random.Range(-spawnpointX,spawnpointX), spawnpointY), quaternion.identity);
    }  
    private void SpawnCollectables()
    {
        obstacle = LevelManager.currentLevel.GetCollectable();
        Instantiate(obstacle, new Vector2(Random.Range(-spawnpointX,spawnpointX), spawnpointY), quaternion.identity);
    }

    private int GetSpawnRepeatTime()
    {
        if (Random.value < multiplewordspawnChance)
        {
            return 2;
        }

        return 1;
    }
    private float GenerateLetterSpawnDelay()
    {
        return Random.Range(minspawnDelay, maxspawnDelay);
    }
   
    public void SetLetter()
    {
        currentLetter = GameManager.Instance.currentWord.GetLetter();
    }

    public void Spawn()
    {
        if(currentLetter)
        {
            animator.SetTrigger("Attack");
            SetLetter();
        }
    }
}
