using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("CORE")]
    public static GameManager Instance;

    public int currentScore;
    private float initializegameDelay;
    [SerializeField] private GameData data;
    [FormerlySerializedAs("initializegame_event")]
    [Header("EVENTS")] 
    [SerializeField] private GameEvent initializegameEvent;
    [SerializeField] private GameEvent startgameEvent;
    [SerializeField] private GameEvent restartgameEvent;
    [SerializeField] private GameEvent gameoverEvent;
    [Header("WORD & LETTER")]
    public Word currentWord;
    public  Transform letterspawnPoint;
    [SerializeField] Transform wordplacementstartPoint;
      private Vector2 wordplacePoint;
    [SerializeField] float wordplaceoffsetX;
    private List<Letter> wordLetters;
    private string sentence;
    [SerializeField] private TextMeshProUGUI sentenceTMP;
    
    public enum Phase { None, Initialize, Start,  Restart, Over, Pause}

    private Phase _phase;

    public Phase currentPhase
    {
        get { return _phase;}
        set
        {
            _phase = value;
            UpdatePhase();
        }
    }
    private void Awake()
    {
        Define();
      
      
    }
    // ReSharper disable once Unity.NoNullPropagation
    private void UpdatePhase()
    {
        switch (_phase)
        {
            case Phase.None:
                currentPhase = Phase.Initialize;
                break;
            case Phase.Initialize:
                initializegameEvent?.Raise();
                break;
            case Phase.Start:
                // ReSharper disable once Unity.NoNullPropagation
                startgameEvent?.Raise();
                break;
            case Phase.Restart:
                break;
            case Phase.Over:
                break;
            case Phase.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
      
        
    }
    private void Define()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Application.targetFrameRate = 120;
        SetWord();
        initializegameDelay = data.initializeDelay;
        UpdatePhase();
    }

    public void InitializeGame()
    {
        StartCoroutine(GameInitializer());
    }
    
    private IEnumerator GameInitializer()
    {
        sentence = currentWord.sentence;
        wordplacePoint = wordplacementstartPoint.position;
        PlaceSentence();
        yield return new WaitForSeconds(initializegameDelay);
        currentPhase = Phase.Start;
    }

    private void SetWord()
    {
         currentWord.Initialize();
         wordLetters = currentWord.GetWordLetters();
    } 

    IEnumerator PlaceWord()
    {
        for(var i =0; i < wordLetters.Count ; i++)
        {
            var letterprefab = Instantiate(wordLetters[i].prefab, letterspawnPoint.position, Quaternion.identity);
            letterprefab.GetComponent<LetterDefinition>().PlaceLetter(wordLetters[i], wordplacePoint);
            letterprefab.transform.DOMove(wordplacePoint, 2);
            wordplacePoint += new Vector2( wordplaceoffsetX, 0);
            yield return new WaitForSeconds(2);
        }
    }
    private void PlaceSentence()
    {
        sentenceTMP.DOText(sentence, 5).OnComplete(() => { StartCoroutine(PlaceWord());
        });
    }

    public void UpdateScore(int scoreAmount)
    {
        currentScore += scoreAmount;
    }
    
}
