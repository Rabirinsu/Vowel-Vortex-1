using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Word> words; 
    [SerializeField] private int levelID;
    private int levelpassCount;
    
    public void LevelUp()
    {
        if(levelID < words.Count -1)
          levelID++;
    }
   
    public void LevelDecrease()
    {
        if(levelID >0)
        levelID--;
    }
    public void LevelReset()
    {
        levelID = 0;
    }

    public void LoadLevel()
    {
        GameManager.Instance.currentWord = words[levelID];
    }
        
 }
