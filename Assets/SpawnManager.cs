using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private float minspawnDelay;     
    [SerializeField] private float maxspawnDelay;     
    [SerializeField] private Animator animator;
    public GameObject nextLetter;
    public void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
     
    }
    private float GenerateRandomValue()
    {
        return Random.Range(minspawnDelay, maxspawnDelay);
    }

    public void ThrowLetters()
    {
        Stop();
        nextLetter = GameManager.Instance.currentWord.GetLetter();
        InvokeRepeating("Spawn", 1, GenerateRandomValue());
    }

    public void Stop()
    {
        CancelInvoke();
    }
    
    private void Spawn()
    {
        if(nextLetter)
        {
            nextLetter = GameManager.Instance.currentWord.GetLetter();
            nextLetter.SendMessage("SetDynamic", SendMessageOptions.DontRequireReceiver);
            animator.SetTrigger("Attack");
        }
    }
}
