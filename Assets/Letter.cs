using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(menuName = "WordGame/Letter", fileName = "Letter")]
public class Letter : ScriptableObject
{
    public string _name;
    public float minspawnTime;
    public float maxspawnTime;  
    public GameObject prefab;

    [SerializeField] private float maxforceX;
    [SerializeField] private float minforceX;
    [SerializeField] private float maxforceY;
    [SerializeField] private float minforceY;
    
     public Vector2 GetSpawnForce()
     {
         return  new Vector2(Random.Range(minforceX,maxforceX),Random.Range(minforceY,maxforceY));
     }
} 
