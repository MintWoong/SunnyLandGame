using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private string detailPlay = "DetailPlay";
    public void PlayOption()
    {
        SceneManager.LoadScene(detailPlay);
    }

}
