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
  [HideInInspector] public float lettersuccesfillAmount;
  [HideInInspector]  public float levelsuccesFill;
  [HideInInspector]  public int currentScore;
  [HideInInspector]   public int bestScore;
    private float initializegameDelay;
   [SerializeField] private List<Letter> missingLetters;
    [SerializeField] private GameData data;
    [FormerlySerializedAs("initializegame_event")]
    [Header("EVENTS")] 
    [SerializeField] private GameEvent initializegameEvent;
    [SerializeField] private GameEvent startgameEvent;
    [SerializeField] private GameEvent restartgameEvent;
    [SerializeField] private GameEvent gameoverEvent;
    [SerializeField] private GameEvent gamesuccesEvent;
    [Header("WORD & LETTER")] 
    public Word currentWord;
    public  Transform letterspawnPoint;
    [SerializeField] Transform wordplacementstartPoint;
      private Vector2 wordplacePoint;
    [SerializeField] float wordplaceoffsetX;
    [SerializeField] private List<Letter> wordLetters;
    private string sentence;
    [SerializeField] private TextMeshProUGUI sentenceTMP;
    [SerializeField] private List<GameObject> placedWords;
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
    public void UpdateBestScore()
    {
        if (currentScore > data.bestScore)
        {
            data.bestScore = currentScore;
            SetBesTScore();
        }
    }

    private void Awake()
    {
        Define();
    }

    public void SetBesTScore()
    {
        bestScore = data.bestScore;
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

    public void LoadGame()
    {
        currentPhase = Phase.Initialize;
    }

    public void StopGame()
    {
        
        Time.timeScale = 0;
        currentPhase = Phase.Over;
        UnplaceWord();
        wordLetters.Clear();
    }
    
    public void  RestartGame()
    {
        currentScore = 0;
        Time.timeScale = 1;
        sentenceTMP.text = "";
        placedWords.Clear();
        currentPhase = Phase.Initialize;
    }   
    public void LevelUp()
    {
        Time.timeScale = 1;
        sentenceTMP.text = "";
        placedWords.Clear();
        currentPhase = Phase.Initialize;
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
        initializegameDelay = data.initializeDelay;
    }

    public void InitializeGame()
    {
        SetWord();
        StartCoroutine(GameInitializer());
        levelsuccesFill = 0;
    }

    public bool IsWordCorrected(Letter letter)
    {
        if (missingLetters.Contains(letter))
        {
          missingLetters.Remove(letter);
          if (missingLetters.Count == 0)
              return true;
        }
        if (missingLetters.Count == 0)
            return true;
        return false;
    }
    
    private IEnumerator GameInitializer()
    {
        UnplaceWord();
        sentence = currentWord.sentence;
        wordplacePoint = wordplacementstartPoint.position;
        PlaceSentence();
        StartCoroutine(PlaceWord());
        yield return new WaitForSeconds(initializegameDelay);
        currentPhase = Phase.Start;
    }
    public void SetWord()
    {
          missingLetters.Clear();
         wordLetters = currentWord.GetWordLetters();
         currentWord.Initialize();
         foreach (var letter in currentWord.missingLetters)
         {
             missingLetters.Add(letter);
         }
    } 

    IEnumerator PlaceWord()
    {
        for(var i =0; i < wordLetters.Count ; i++)
        {     
            var letterprefab = Instantiate(wordLetters[i].prefab, letterspawnPoint.position, Quaternion.identity);
            placedWords.Add(letterprefab);
            letterprefab.GetComponent<LetterDefinition>().PlaceLetter(wordLetters[i], wordplacePoint);
            letterprefab.transform.DOMove(wordplacePoint, 2f);
            wordplacePoint += new Vector2( wordplaceoffsetX, 0);
            yield return new WaitForSeconds(1.2f);
        }
    }

    public void UnplaceWord()
    {
        for (var i = 0; i < placedWords.Count; i++)
        {
           Destroy( placedWords[i]);
        }
        placedWords.Clear();
    }
    private void PlaceSentence()
    {
        sentenceTMP.DOText(sentence, 2);
    }

    public void UpdateScore(int scoreAmount)
    {
        currentScore += scoreAmount;
    }
    
}
