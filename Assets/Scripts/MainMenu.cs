using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void GameSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    
}
