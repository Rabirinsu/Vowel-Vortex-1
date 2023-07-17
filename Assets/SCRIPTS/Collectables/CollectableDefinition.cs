using System;
using UnityEngine;

namespace Collectables
{
    public class CollectableDefinition : MonoBehaviour
    {
        [SerializeField] private Collectable collectable;
        private float rewardAmount;
        private GameObject prefab;
        private GameObject collectedFX;
        private float lifeTime;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Vector2 forcePower;
        [Header("EVENTS")]
        [SerializeField] private GameEvent collectedEvent;
        private void Initialize()
        {
            rewardAmount = collectable.rewardAmount;
            prefab = collectable.prefab;
            collectedFX = collectable.collectedFX;
            lifeTime = collectable.lifeTime;
            Destroy(this.gameObject,lifeTime);
        }

        private void OnEnable()
        {
            Initialize();
            rb.AddForce(forcePower, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
          /*  if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(collectedFX, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }*/
             if (other.gameObject.CompareTag("Bullet"))
            {
                Instantiate(collectedFX, transform.position, Quaternion.identity);
                collectedEvent?.Raise();
                Destroy(this.gameObject);
            }
        }
    }
    
    
}
