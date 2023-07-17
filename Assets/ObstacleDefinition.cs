using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleDefinition : MonoBehaviour
{
    [SerializeField] private float followDistance;
    private Transform player;
    [Header("CORE")]
    [SerializeField] private Obstacle obstacle;

    private GameObject prefab;
   [SerializeField] private  Sprite sprite;
    private Vector2 forcePower;
    public int hardnesLevel;
   private float lifeTime;
    public GameObject explodeFX;
    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("EVENTS")]
    [SerializeField] private GameEvent playerdamageEvent;

    private bool OnFollow;
      private void OnEnable()
      {
          Initialize();
      }

    private void Initialize()
      {
          prefab = obstacle.prefab;
          sprite = obstacle.sprite;
          forcePower = obstacle.forcePower;
          hardnesLevel = obstacle.hardnesLevel;
          lifeTime = obstacle.lifeTime;
          explodeFX = obstacle.explodeFX;
          Define();
      }
      
      private void Define()
      {
          spriteRenderer.sprite = sprite;
       
          Destroy(this.gameObject,lifeTime);
          player = GameObject.FindWithTag("Player").transform;
          if (hardnesLevel <= 1) 
           rb.AddForce(forcePower, ForceMode2D.Impulse);
      }

      private void LateUpdate()
      {
          Act();
      }

      private void Act()
      {
          if (!OnFollow)
          {
              if (hardnesLevel > 1 )
              {
                  var dir = player.position - transform.position;
                  var angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                  transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                  if (Vector2.Distance(transform.position, player.position) > followDistance)
                  {
                      Vector2.Distance(transform.position, player.position);
                      var step = 2 * Time.deltaTime;
                      transform.position = Vector2.MoveTowards(transform.position, player.position, step);
                  }
                  else
                  {
                      OnFollow = true;
                      rb.AddForce(forcePower, ForceMode2D.Impulse);
                  }
              }
          }
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
