using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float obstaclespawnDelay;
    [SerializeField] private float spawnpointX;
    [SerializeField] private float spawnpointY;
    [SerializeField] private float minspawnDelay;     
    [SerializeField] private float maxspawnDelay;     
    [SerializeField] private Animator animator;
    public GameObject nextLetter;
    public void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        InvokeRepeating("SpawnObstacles", 1, obstaclespawnDelay);
    }

    private void SpawnObstacles()
    {
        Instantiate(obstacle, new Vector2(Random.Range(-spawnpointX,spawnpointX), spawnpointY), quaternion.identity);
    }
    
    private float GenerateRandomValue()
    {
        return Random.Range(minspawnDelay, maxspawnDelay);
    }

    public void ThrowLetters()
    {
        Stop();
        nextLetter = GameManager.Instance.currentWord.GetLetter();
        InvokeRepeating("Spawn", 1, GenerateRandomValue());
    }

    public void Stop()
    {
        CancelInvoke("Spawn");
    }
    
    private void Spawn()
    {
        if(nextLetter)
        {
            nextLetter = GameManager.Instance.currentWord.GetLetter();
            nextLetter.SendMessage("SetDynamic", SendMessageOptions.DontRequireReceiver);
            animator.SetTrigger("Attack");
        }
    }
}
