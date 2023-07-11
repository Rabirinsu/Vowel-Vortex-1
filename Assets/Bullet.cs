using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
      [SerializeField] private float lifeTime;
      [SerializeField] private Rigidbody2D rb;
      [FormerlySerializedAs("force")] [SerializeField] private float forcePower;
      
      private void OnEnable()
      {
          StartCoroutine(Define());
       }
      
      IEnumerator Define()
      {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
      }

      public void Fire(Vector2 targetAngle)
      {
            Debug.Log("Bullet fired " + targetAngle);
            rb.AddForce(targetAngle * forcePower , ForceMode2D.Impulse);
      }

      private void OnCollisionEnter2D(Collision2D other)
      {
            if (other.gameObject.CompareTag("Letter"))
            {
                    other.gameObject.SendMessage("GetHit", SendMessageOptions.DontRequireReceiver);
            }
      }
}
