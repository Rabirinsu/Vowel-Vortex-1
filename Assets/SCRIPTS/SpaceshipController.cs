using System;   
using UnityEngine;
using UnityEngine.Serialization;


public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float  fireDelay;
    [SerializeField]  private float currentfireDelay;
   [SerializeField] private Rigidbody2D rb;
    
   private Vector2 targetAngle;
   [SerializeField] private Camera mainCam;
    [Header("Player Screen Bounds")]
   [SerializeField] private float rightBound;
   [SerializeField] private float leftBound;
   [SerializeField] private float ceilBound;
   [SerializeField] private float floorBound;
   private void FixedUpdate()
    {
            Fire();
        if(Input.anyKey)
        {
           Move();
        }
        if (!IsFireReady())
            UpdateFireDelay();
     
    }

   private void LateUpdate()
   {
       if(Input.anyKey)
       {
           CheckPlayerBound();
       }
   }

   private Vector2 NormalizePlayerPosition()
   {
      return new Vector2(transform.position.x, transform.position.y);
   }
   
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        var movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
    
    private void Fire()
    {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                if (IsFireReady())
                {
                    transform.rotation = SetTargetAngle();
                    BulletPool.Instance.InstantiateBullet(targetAngle);
                    currentfireDelay = fireDelay;
                }
            }
    }

    private void CheckPlayerBound()
    {
        if (transform.position.x >= rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, 0);
        }
        else if (transform.position.x <= leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, 0);
        }   
        if (transform.position.y >= ceilBound)
        {
            transform.position = new Vector3(transform.position.x, ceilBound, 0);
        }
        else if (transform.position.y <= floorBound)
        {
            transform.position = new Vector3(transform.position.x, floorBound, 0);
        }
        
    }
    private void UpdateFireDelay()
    {
        currentfireDelay -= Time.deltaTime;
    }
    
    private bool IsFireReady()
    {
        if (currentfireDelay <= 0)
            return true;
        else 
            return false;
    }
    
    private Quaternion SetTargetAngle()
    {
       var  mousePosition = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - mainCam.transform.position.z));
       var rotateAngle = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position); 
       targetAngle = (mousePosition - transform.position);
       targetAngle.Normalize();
       return rotateAngle;
    }
}
