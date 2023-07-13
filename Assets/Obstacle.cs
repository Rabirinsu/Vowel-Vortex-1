using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
      [SerializeField] private float lifeTime;
      [SerializeField] private Vector2 forcePower;
      [SerializeField] private Rigidbody2D rb;
      [SerializeField] private GameEvent playerdamageEvent;
      [SerializeField] private GameObject explodeFX;
      private void OnEnable()
      {
             rb.AddForce(forcePower, ForceMode2D.Impulse);
             Destroy(this.gameObject,lifeTime);
      }
   
      private void OnTriggerEnter(Collider other)
      {
        
      }

      private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.gameObject.CompareTag("Player"))
          {
              playerdamageEvent?.Raise();
              Instantiate(explodeFX, transform.position, quaternion.identity);
              Destroy(this.gameObject);
          }
           else if (other.gameObject.CompareTag("Bullet"))
          {
              Instantiate(explodeFX, transform.position, quaternion.identity);
              Destroy(this.gameObject);
          }
      }
}
