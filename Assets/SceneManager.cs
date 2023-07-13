
using UnityEngine;

public class SceneManager : MonoBehaviour
{
     public void LoadScene(int ID)
     {
         UnityEngine.SceneManagement.SceneManager.LoadScene(ID);
     }
     
}
