using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public static bool isGamePause = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private string home = "StartScreen";
    int Saved_scene;

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            if (isGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
       } 
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;
    }
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
        
    }
}
