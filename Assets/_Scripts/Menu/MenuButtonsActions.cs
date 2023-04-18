using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsActions : MonoBehaviour
{
    [SerializeField] private string github;
    [SerializeField] private string linkedin;
    
    
    public void Play()
    {
        Debug.Log("Load Next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OpenGithub()
    {
        Application.OpenURL(github);
    }

    public void OpenLinkedIn()
    {
        Application.OpenURL(linkedin);
    }
}
