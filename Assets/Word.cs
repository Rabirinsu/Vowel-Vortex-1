using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "WordGame/Word", fileName = "Word")]
public class Word : ScriptableObject
{
    public AllLeters allletters;
    public List<Letter> consistLetters;
    public List<Letter> missingLetters;

    public string sentence;
    public int level;
    public int scoreamount;

    [HideInInspector] public List<Letter> wrongLetters;

    [Range(0f, 1f)] public float missingletterspawnChance;

    public Letter questionLetter;

    public void Initialize()
    {
        SetWrongLetters();
    }

    private void SetWrongLetters()
    {
        wrongLetters = allletters.letters;
        foreach (var letter in wrongLetters)
        {
            if (missingLetters.Contains(letter))
            {
                wrongLetters.Remove(letter);
            }
        }
    }

    public List<Letter> GetWordLetters()
    {
        var letterlist = new List<Letter>();
        for (var i = 0; i < consistLetters.Count; i++)
        {
            if (isMissing(consistLetters[i]))
            {
                letterlist.Add(questionLetter);
            }
            else letterlist.Add(consistLetters[i]);
        }

        return letterlist;
    }

    public bool isMissing(Letter letter)
    {
        if (missingLetters.Contains(letter))
        {
            return true;
        }
        return false;
    }

    public GameObject GetLetter()
    {
        var val = Random.value;
        if (val > missingletterspawnChance)
        {
            return wrongLetters[Random.Range(0, wrongLetters.Count)].prefab;
        }
        return missingLetters[Random.Range(0, missingLetters.Count)].prefab;
    }
}