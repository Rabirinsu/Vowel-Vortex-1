
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
      [SerializeField] private TextMeshProUGUI scoreTmp;
      [SerializeField] private float scoreupdateDelay;
      [SerializeField] private Color rightColor;
      [SerializeField] private Color wrongColor;
      [SerializeField] private Color defaultColor;

      public void UpdateScore(int scoreAmount)
      {
          var beforescore = GameManager.Instance.currentScore - scoreAmount;
          var currentScore = GameManager.Instance.currentScore;
          scoreTmp.DOCounter(beforescore, currentScore, scoreupdateDelay);

          if (scoreAmount > 0)
          {
              scoreTmp.DOColor(rightColor, 2).OnComplete(() => { scoreTmp.color = defaultColor; });
          } else scoreTmp.DOColor(wrongColor, 2).OnComplete(() => { scoreTmp.color = defaultColor; });
      }

  }
