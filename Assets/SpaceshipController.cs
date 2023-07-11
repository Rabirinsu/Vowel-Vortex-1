using System;   
using UnityEngine;


public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float fireDelay;
    [SerializeField]  private float currentfireDelay;
   [SerializeField] private Rigidbody2D rb;
    
   private Vector2 targetAngle;
   [SerializeField] private Camera mainCam;
   private void FixedUpdate()
    {
        if(Input.anyKey)
        {
            Move();
            Fire();
        }

        if (!IsFireReady())
            UpdateFireDelay();
     
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
            if (Input.GetButton("Fire1") && IsFireReady())
            {
                 transform.rotation = SetTargetAngle();
                BulletPool.Instance.InstantiateBullet(targetAngle);
                currentfireDelay = fireDelay;
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
