using UnityEngine;

namespace Collectables
{
     
     [CreateAssetMenu(menuName = "WordGame/Collectables/Coin", fileName = "Coin")]
     public class Collectable : ScriptableObject
     {
          public float rewardAmount;
          public GameObject prefab;
          public GameObject collectedFX;
          public float lifeTime;

          public virtual void Act()
          {
          }
     }
}