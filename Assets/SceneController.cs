using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public void GoToScene(string scenenName)
   {
      SceneManager.LoadScene(scenenName);
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
