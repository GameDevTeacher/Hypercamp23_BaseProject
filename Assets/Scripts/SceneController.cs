using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public void GoToScene(string sceneName)
   {
      SceneManager.LoadScene(sceneName);
   }

   public void GoToNextScene()
   {
      if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
         
      }
      else
      {
         SceneManager.LoadScene(0); 
      }
      
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
