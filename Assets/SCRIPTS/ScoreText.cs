
using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private int scoreAmount;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private GameEvent hitwrongEvent;
    [SerializeField] private GameEvent hitmissinEvent;
    [SerializeField] private Transform scoreAnchor;
    public enum Type{wrong, right, Coin}

    public Type currentType;
    private void OnEnable()
    {
        if (currentType == Type.Coin)
        scoreTMP.text = scoreAmount.ToString();
        
        scoreAnchor = GameObject.FindWithTag("ScoreAnchor").transform;
        transform.DOMove(scoreAnchor.position, 2).OnComplete(() =>
        {
            if (currentType == Type.wrong)
                hitwrongEvent?.Raise(); 
           else if (currentType == Type.right)
                hitmissinEvent?.Raise();
            else if (currentType == Type.Coin)
            {
                  GameManager.Instance.UpdateScore(scoreAmount);
                  UIManager.Instance.UpdateScore(scoreAmount);
            }
             Destroy(this.gameObject);
        });
    }

    private void LateUpdate()
    {
        if(GameManager.Instance.currentPhase == GameManager.Phase.Over)
            Destroy(this.gameObject);
    }
}
