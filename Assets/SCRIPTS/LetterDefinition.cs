using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LetterDefinition : MonoBehaviour
{
    [Header("EVENTS")] 
    [SerializeField] private GameEvent hitmissinEvent;
    [SerializeField] private GameEvent hitwrongEvent;
    [SerializeField] private GameEvent gamesuccesEvent;
    
    [Header("CORE")]
    [SerializeField] private Letter letter;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public enum Type{ None, wrong, missing}

    public Type currentType;
    [SerializeField] private GameObject wrongScore;
    [SerializeField] private GameObject rightScore;
    [SerializeField]  private BoxCollider2D col;
    private float lifeTime = 15f;
    [SerializeField]   private float destroyDelay;

    [SerializeField] private GameObject wrongFX;
    [SerializeField] private GameObject rightFX;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
           spriteRenderer.color = Color.green;
           transform.GetChild(0).gameObject.SetActive(true);
           Destroy(this.gameObject, lifeTime);
       }
       else
       {
           // Go player to explode
           currentType = Type.wrong;    
           spriteRenderer.color = Color.green;
           col.isTrigger = false;
           transform.GetChild(0).gameObject.SetActive(true);
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
                if (GameManager.Instance.IsWordCorrected(letter))
                    gamesuccesEvent?.Raise();
                Destroy();
                // ReSharper disable once Unity.NoNullPropagation
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
        
    }

    private IEnumerator CallEventRoutine()
    {
        yield return new WaitForSeconds(.7f);
        
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
