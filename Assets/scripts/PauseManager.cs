using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // or your custom pause key
        {
           
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
       pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioListener.pause = false; // when resumed
    }

    void Pause()
    {
        
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        AudioListener.pause = true; // when paused
        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioListener.pause = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu"); // Replace with your actual menu scene name
    }

    public void QuitGame()
    {
        AudioListener.pause = false;
        Application.Quit();
    }
}
