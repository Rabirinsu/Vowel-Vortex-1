using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class LetterDefinition : MonoBehaviour
{
    [Header("EVENTS")] 
    [SerializeField] private GameEvent hitmissinEvent;
    [SerializeField] private GameEvent hitwrongEvent;
    
    [Header("CORE")]
    [SerializeField] private Letter letter;
    [SerializeField] private Rigidbody2D rb;
    public enum Type{ None, wrong, missing}

    public Type currentType;
    [SerializeField] private GameObject wrongScore;
    [SerializeField] private GameObject rightScore;

    [SerializeField]
    private BoxCollider2D col;

    private float lifeTime = 15f;
    [SerializeField]
    private float destroyDelay;
    private void Initialize()
    {
       var force = letter.GetSpawnForce();
       rb.AddForce(force, ForceMode2D.Impulse);
       if(GameManager.Instance.currentWord.isMissing(letter))
       {
           // Got Score
           // Go To Word .. Point
           currentType = Type.missing;
           col.isTrigger = false;
           Destroy(this.gameObject, lifeTime);
       }
       else
       {
           // Go player to explode
           currentType = Type.wrong;    
           col.isTrigger = false;
           Destroy(this.gameObject, lifeTime);
       }
    }
    
    public void GetHit()
      {
          switch(currentType)
          {
              case Type.None:
                  break;
              case Type.wrong:
                  Instantiate(wrongScore,transform.position, quaternion.identity);
                  Destroy();
                  // ReSharper disable once Unity.NoNullPropagation
                  break;
              case Type.missing:
                  Instantiate(rightScore,transform.position, quaternion.identity);
                  Destroy();
                  // ReSharper disable once Unity.NoNullPropagation
                  break;
              default:
                  throw new ArgumentOutOfRangeException();
          }
      }

    private void Destroy()
    {
        rb.isKinematic = true;
        Destroy(col);
        transform.DOShakeScale(1f).OnComplete(() => { transform.DOScale(Vector3.zero, destroyDelay);}).OnComplete(() => {  Destroy(this.gameObject); });
    }
    public void PlaceLetter(Letter letter,Vector2 placementPoint)
    {
        transform.DOMove(placementPoint, 2);
    }

    public void SetDynamic()
    {
        rb.isKinematic = false;
        Initialize();
    }
}
