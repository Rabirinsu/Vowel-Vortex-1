
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [Header("TEXT MESH")]
      [SerializeField] private TextMeshProUGUI scoreTmp;
      [SerializeField] private TextMeshProUGUI currentscoreTmp;
      [SerializeField] private TextMeshProUGUI bestscoreTmp;
      [SerializeField] private TextMeshProUGUI currentlevelTmp;
      private float barcurrentFillamount;
      [Header("CORE")]
      [SerializeField] private Image succesBar;
      [SerializeField] private GameObject gameoverCanvas;
      [SerializeField] private GameObject gamesuccessCanvas;
      [SerializeField] private GameObject scoreTable;
      [SerializeField] private float uitweenDelay;
       [SerializeField] private float rightbarfillAmount;
       [SerializeField] private float wrongbarfillAmount;
      [SerializeField] private Color rightColor;
      [SerializeField] private Color wrongColor;
      [SerializeField] private Color defaultColor;
      [SerializeField] private List<Button> lifes;
      [Header("EVENTS")]
     [SerializeField] private GameEvent gameoverEvent;
     [SerializeField] private GameEvent gamesuccessEvent;
      private void OnEnable()
      {
          Reset();
      }

      public void UpdateLifes()
      {
          for (var i = 0; i < lifes.Count; i++)
          {
              if (lifes[i].interactable)
              {
                  lifes[i].interactable = false;
                  if (i == lifes.Count-1)
                  {
                      gameoverEvent.Raise(); 
                  }
                  return;
              }
          }
      }
      private void NormalizeBar()
      {
          if (barcurrentFillamount >= 1)
          {
              barcurrentFillamount = 1;
              gamesuccessEvent?.Raise();
          }
          else if (barcurrentFillamount < 0)
              barcurrentFillamount = 0;
      }
      public void Reset()
      {
          GameManager.Instance.currentScore = 0;
          gameoverCanvas.SetActive(false);
          gamesuccessCanvas.SetActive(false);
          scoreTable.SetActive(false);
          barcurrentFillamount = 0f;
         // UpdateSuccesBar(0f,defaultColor);
          UpdateScore(0);
          foreach (var life in lifes)
          {
              life.interactable = true;
          }
          currentlevelTmp.text = "1";
      }
     public void LevelUp()
      {
          gameoverCanvas.SetActive(false);
          gamesuccessCanvas.SetActive(false);
          scoreTable.SetActive(false);
          barcurrentFillamount = 0f;
        //  UpdateSuccesBar(0f,defaultColor);
          
          foreach (var life in lifes)
          {
              life.interactable = true;
          }

          currentlevelTmp.text = LevelManager.currentLevel.ID.ToString();
      }

      public void GameOver()
      {
          currentscoreTmp.text =  GameManager.Instance.currentScore.ToString();
          bestscoreTmp.text =  GameManager.Instance.bestScore.ToString();
      }
      
      public void UpdateScore(int scoreAmount)
      {
          var beforescore = GameManager.Instance.currentScore - scoreAmount;
          var currentScore = GameManager.Instance.currentScore;
          scoreTmp.DOCounter(beforescore, currentScore, uitweenDelay);

          if (scoreAmount > 0)
          {
              scoreTmp.DOColor(rightColor, uitweenDelay).OnComplete(() => { scoreTmp.color = defaultColor; });
            //  UpdateSuccesBar(rightbarfillAmount, rightColor);
          }
          else
          {
              scoreTmp.DOColor(wrongColor, uitweenDelay).OnComplete(() => { scoreTmp.color = defaultColor; });
             // UpdateSuccesBar(wrongbarfillAmount, wrongColor);
          }
              
      }

      private void UpdateSuccesBar(float fillAmount, Color color)
      {
          barcurrentFillamount = fillAmount;
          NormalizeBar();
          succesBar.DOFillAmount(barcurrentFillamount, uitweenDelay);
          succesBar.DOColor(color, uitweenDelay).OnComplete(() => { succesBar.color = defaultColor; });
      }
  }
