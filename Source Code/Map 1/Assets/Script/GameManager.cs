using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CollectedGems { get; set; }
    public int Lives { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetGameData()
    {
        // Đặt lại dữ liệu game về trạng thái ban đầu
        Lives = 2;
        CollectedGems = 0;
        // TODO: Đặt lại các dữ liệu khác (vị trí, trạng thái nhân vật, v.v.)
    }

}
