using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
