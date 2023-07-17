using System.Collections.Generic;
using Collectables;
using UnityEngine;


[CreateAssetMenu(menuName = "WordGame/Level", fileName = "Level X")]
public class Level : ScriptableObject
{
      public int ID;
      public Word word;
      public List<Obstacle> obstacles;
      public List<Collectable> collectables;
      public float minobstaclepawnDelay;
      public float maxobstaclespawnDelay;
      public float mincollectablespawnDelay;
      public float maxcollectablespawnDelay;

      
      public GameObject GetObstacle()
      {
          return obstacles[Random.Range(0, obstacles.Count)].prefab;
      }

      public float GenerateObstacleSpawnDelay()
      {
          return Random.Range(mincollectablespawnDelay, maxobstaclespawnDelay);
      }  
      public float GenerateCollectableSpawnDelay()
      {
          return Random.Range(mincollectablespawnDelay, maxcollectablespawnDelay);
      }
      public GameObject GetCollectable()
      {
          return collectables[Random.Range(0, collectables.Count)].prefab;
      }
    
}
