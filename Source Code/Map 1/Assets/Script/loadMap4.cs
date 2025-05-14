using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMap3 : MonoBehaviour
{
    public string targetSceneName = "Map4"; // Tên của scene mà bạn muốn chuyển tới

    private bool isNearDoor = false;
    private void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ChangeSceneAfterDelay());
            AudioManager.Instance.PlaySFX("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra khi nhân vật đi vào gần cánh cửa
        if (collision.CompareTag("Fox"))
        {
            isNearDoor = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Kiểm tra khi nhân vật đi ra xa cánh cửa
        if (collision.CompareTag("Fox"))
        {
            isNearDoor = false;
        }
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Đợi 1 giây
        SceneManager.LoadScene(targetSceneName); // Chuyển đến scene mới


    }
}
