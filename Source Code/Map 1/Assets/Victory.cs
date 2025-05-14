using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public void Menu()
    {
        // Reset dữ liệu trò chơi trước khi quay lại menu
        ResetGameData();
        SceneManager.LoadScene(0);
    }

    private void ResetGameData()
    {
        // Đặt lại dữ liệu game (số mạng, gem, vị trí, v.v.) vào trạng thái ban đầu
        GameManager.Instance.Lives = 2; // Ví dụ: Đặt số mạng ban đầu là 2
        GameManager.Instance.CollectedGems = 0; // Đặt số gem đã nhặt về 0
        // TODO: Đặt lại các dữ liệu khác (vị trí, trạng thái nhân vật, v.v.)
    }
}
