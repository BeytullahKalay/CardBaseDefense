using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
   public void PlayAgainButtonActions()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
