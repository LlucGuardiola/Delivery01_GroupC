using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //  public void Play() 
    // {
    // SceneManager.LoadScene("Gameplay");
    //}

    public void OnStart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
