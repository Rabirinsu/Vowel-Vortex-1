
using UnityEngine;


[CreateAssetMenu(menuName = "WordGame/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
      public float initializeDelay;
      public int bestScore;
      public int currentlevelID;
      public void SaveScore(int currentScore)
      {
            if (currentScore > bestScore)
                  bestScore = currentScore;
      }

}
