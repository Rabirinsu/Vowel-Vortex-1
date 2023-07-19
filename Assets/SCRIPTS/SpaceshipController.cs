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

   [Header("CONTROL MOVEMENT")]
   [SerializeField]  private float maxfallingSpeed;
   [SerializeField] private float minfallSpeed;
   [SerializeField] private float maxRotation;
   [SerializeField] private float rotateSpeed;

  
   [SerializeField] private float boostleveltime_Count;
   [SerializeField]  private float boostlevel1_Time;
   [SerializeField] private float boostlevel2_Time;
   [SerializeField] private float boostlevel3_Time;

   [SerializeField] private Vector2 boostForce;
   [Header("SPRITES")]
   [SerializeField]
   private SpriteRenderer spriteRender;

   [SerializeField] private Sprite defaultSprite;
   [SerializeField] private Sprite level1sprite;
   [SerializeField] private Sprite level2sprite;
   [SerializeField] private Sprite level3sprite;
     public enum rotatePhase{None,Left, Right}

     public rotatePhase currentrotate;
     
    
     private bool boostLevelActive;
   private void FixedUpdate()
    {
        if(Input.anyKey)
        {
            Rotate();
             Move();
           Fire();
        }
        if (!IsFireReady())
            UpdateFireDelay();
        LimitVelocity();
          CheckBoostLevel();
    }
    
   private void Rotate()
   {
   
       if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
       {
           transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
           currentrotate = rotatePhase.Left;
       }
    else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
       {
           transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
           currentrotate = rotatePhase.Right;
       }
   }
   
   private void LimitVelocity()
   {
       if (rb.velocity.y < - maxfallingSpeed)
       {
           rb.velocity = new Vector2( rb.velocity.x, Mathf.Clamp( rb.velocity.y, minfallSpeed,maxfallingSpeed));
       }
   }

   private void Update()
   {
       if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && boostLevelActive)
       {
           boostLevelActive = false;
       }
   }

   private void LateUpdate()
   {
           CheckPlayerBound();
   }

   private Vector2 NormalizePlayerPosition()
   {
      return new Vector2(transform.position.x, transform.position.y);
   }

   private void CheckBoostLevel()
   {
       if(boostLevelActive && boostleveltime_Count < boostlevel3_Time)
         boostleveltime_Count += Time.deltaTime;
       else if (!boostLevelActive && boostleveltime_Count > 0)
           boostleveltime_Count -= Time.deltaTime;
       if ((!boostLevelActive && boostleveltime_Count <= boostlevel1_Time) && spriteRender.sprite != defaultSprite)
       {
           spriteRender.sprite = defaultSprite;
           maxfallingSpeed = 1.5f;
       }
       else if (boostLevelActive &&
                (boostleveltime_Count >= boostlevel1_Time && boostleveltime_Count < boostlevel2_Time) && spriteRender.sprite != level1sprite)
       {
          rb.AddForce(boostForce, ForceMode2D.Impulse);
           spriteRender.sprite = level1sprite;
           maxfallingSpeed = .7f;
       }
       else if (boostleveltime_Count >= boostlevel2_Time && boostleveltime_Count < boostlevel3_Time  && spriteRender.sprite != level2sprite)
       {
          rb.AddForce(boostForce, ForceMode2D.Impulse);
           spriteRender.sprite = level2sprite;
           maxfallingSpeed = .3f;
       }
       else if (boostleveltime_Count >= boostlevel3_Time  && spriteRender.sprite != level3sprite)
       {
           rb.AddForce(boostForce, ForceMode2D.Impulse);
           spriteRender.sprite = level3sprite;
           maxfallingSpeed = .1f;
       }

   }

   private void Move()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            boostLevelActive = true;
          
        }
   

     /*  else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }*/
      /*  float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        var movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement)*/
    }
    
    private void Fire()
    {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                if (IsFireReady())
                {
                    //transform.rotation = SetTargetAngle();
                    BulletPool.Instance.InstantiateBullet(transform.up);
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
