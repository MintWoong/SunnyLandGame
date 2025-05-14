using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diChuyenQuai : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của quái vật
    private Vector2 direction = Vector2.right; // Hướng di chuyển ban đầu (qua phải)
    public float leftBoundary = -5f; // Giới hạn bên trái quái vật
    public float rightBoundary = 5f; // Giới hạn bên phải quái vật
    private Animator animator;
    void Start()
    {
        // Các dòng code khác không thay đổi
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Di chuyển quái vật theo hướng và tốc độ đã xác định
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        // Kiểm tra xem quái vật đã đến đầu hoặc cuối khu vực di chuyển chưa
        if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
        {
            // Nếu đạt đến giới hạn, đảo hướng di chuyển
            direction = -direction;
            // Đảo hướng scale x của quái vật để không bị lật ngược mặt
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

    }
    public void Die()
    {
        // Chạy animation chết
        animator.SetTrigger("die");

        // Chờ một khoảng thời gian để animation chết hoàn thành
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Chờ đến khi animation chết hoàn thành
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Sau khi chạy xong animation, hủy đối tượng quái vật
        Destroy(gameObject);
    }


}
