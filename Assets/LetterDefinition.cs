using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    
    private void Initialize()
    {
       var force = letter.GetSpawnForce();
       rb.AddForce(force, ForceMode2D.Impulse);
       if(GameManager.Instance.currentWord.isMissing(letter))
       {
           // Got Score
           // Go To Word .. Point
           currentType = Type.missing;
       }
       else
       {
           // Go player to explode
           currentType = Type.wrong;
       }
    }
    
    public void GetHit()
      {
          switch(currentType)
          {
              case Type.None:
                  break;
              case Type.wrong:
                  rb.isKinematic = true;
                  // ReSharper disable once Unity.NoNullPropagation
                  hitwrongEvent?.Raise();
                  break;
              case Type.missing:
                  rb.isKinematic = true;
                  // ReSharper disable once Unity.NoNullPropagation
                  hitmissinEvent?.Raise();
                  break;
              default:
                  throw new ArgumentOutOfRangeException();
          }
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
