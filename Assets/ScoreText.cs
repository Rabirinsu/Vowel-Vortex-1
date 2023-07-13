
using DG.Tweening;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private GameEvent hitwrongEvent;
    [SerializeField] private GameEvent hitmissinEvent;
    [SerializeField] private Transform scoreAnchor;
    public enum Type{wrong, right}

    public Type currentType;
    private void OnEnable()
    {
         scoreAnchor = GameObject.FindWithTag("ScoreAnchor").transform;
        transform.DOMove(scoreAnchor.position, 2).OnComplete(() =>
        {
            if (currentType == Type.wrong)
                hitwrongEvent?.Raise(); 
            if (currentType == Type.right)
                hitmissinEvent?.Raise();
             Destroy(this.gameObject);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
