using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    int Saved_scene;
    public void StarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Load_Saved_Scene()
    {
        Saved_scene = PlayerPrefs.GetInt("Saved");

        if (Saved_scene != 0)
            SceneManager.LoadSceneAsync(Saved_scene);
        else
            return;
    }

    public void BackGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
