
using UnityEngine;


[CreateAssetMenu(menuName = "WordGame/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
      public float initializeDelay;
      public int bestScore;

      public void SaveScore(int currentScore)
      {
            if (currentScore > bestScore)
                  bestScore = currentScore;
      }

}
