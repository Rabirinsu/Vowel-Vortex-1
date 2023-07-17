
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = "WordGame/Obstacle", fileName = "New Obstacle")]
public class Obstacle : ScriptableObject
{
       public GameObject prefab;
       public Sprite sprite;
       public Vector2 forcePower;
       public int hardnesLevel;
       public float lifeTime;
       public GameObject explodeFX;
       
      
}
