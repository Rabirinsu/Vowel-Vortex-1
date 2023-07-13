
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
      [SerializeField] private TextMeshProUGUI scoreTmp;
      [SerializeField] private Image succesBar;
      private float barcurrentFillamount;
      [SerializeField] private GameObject gameoverCanvas;
      [SerializeField] private float uitweenDelay;
       [SerializeField] private float rightbarfillAmount;
       [SerializeField] private float wrongbarfillAmount;
      [SerializeField] private Color rightColor;
      [SerializeField] private Color wrongColor;
      [SerializeField] private Color defaultColor;
      [SerializeField] private List<Button> lifes;
     [SerializeField] private GameEvent gameoverEvent;
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
          if (barcurrentFillamount > 1)
              barcurrentFillamount = 1;
          else if (barcurrentFillamount < 0)
              barcurrentFillamount = 0;
      }
      public void Reset()
      {
          barcurrentFillamount = 0f;
          UpdateSuccesBar(0f,defaultColor);
          foreach (var life in lifes)
          {
              life.interactable = true;
          }
      }

      public void GameOver()
      {
          gameoverCanvas.SetActive(true);
      }
      public void UpdateScore(int scoreAmount)
      {
          var beforescore = GameManager.Instance.currentScore - scoreAmount;
          var currentScore = GameManager.Instance.currentScore;
          scoreTmp.DOCounter(beforescore, currentScore, uitweenDelay);

          if (scoreAmount > 0)
          {
              scoreTmp.DOColor(rightColor, uitweenDelay).OnComplete(() => { scoreTmp.color = defaultColor; });
              UpdateSuccesBar(rightbarfillAmount, rightColor);
          }
          else
          {
              scoreTmp.DOColor(wrongColor, uitweenDelay).OnComplete(() => { scoreTmp.color = defaultColor; });
              UpdateSuccesBar(wrongbarfillAmount, wrongColor);
          }
              
      }

      private void UpdateSuccesBar(float fillAmount, Color color)
      {
          barcurrentFillamount += fillAmount;
          NormalizeBar();
          succesBar.DOFillAmount(barcurrentFillamount, uitweenDelay);
          succesBar.DOColor(color, uitweenDelay).OnComplete(() => { succesBar.color = defaultColor; });
      }
  }
