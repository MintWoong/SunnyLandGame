using System.Collections;
/*using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;*/
using UnityEngine;
/*using UnityEngine.Pool;*/
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiChuyenNhanVat : MonoBehaviour
{
    public float tocdo;
    private float traiPhai;
    private bool isfacingRight = true;
    public bool duocPhepNhay;
    private Animator animator;
    private Rigidbody2D rb;

    // Các biến liên quan đến nhảy
    public float lucNhayNhe = 5f;
    public float thoiGianNhay = 0.25f;
    public float lucNhayToiDa = 8f;
    private bool dangGiunPhimNhay;
    private float thoiGianGiunPhimNhay;

    // Các biến liên quan đến miễn sát thương
    private bool immuneToDamage = false;
    private float immuneDuration = 2f;
    private float lastHitTime = 0f;

    private SpriteRenderer spriteRenderer; // Component SpriteRenderer để thay đổi trạng thái nhấp nháy

    // Các biến liên quan đến mạng sống và gem
    public int lives = 2;
    public  int collectedGems;
    public int gemsToExtraLife = 10;
    
    //biến text
    [SerializeField] private Text liveText;
    [SerializeField] private Text gemText;
    [SerializeField] private Text winText;

    

    void Start()
    {
        // Khởi tạo các component và biến cơ bản
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectedGems = GameManager.Instance.CollectedGems;
        lives = GameManager.Instance.Lives;
        GameManager.Instance.ResetGameData();
    }


    void Update()
    {
        traiPhai = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(traiPhai * tocdo, rb.velocity.y);
        flip();
        animator.SetFloat("move", Mathf.Abs(traiPhai));
        //hiển thị lên số lượng mạng và gem lên text
        liveText.text = " " + lives;
        gemText.text = " " + collectedGems;
        // Lưu số lượng gems và lives vào GameManager
        GameManager.Instance.CollectedGems = collectedGems;
        GameManager.Instance.Lives = lives;
        

        // Xử lý nhảy
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && duocPhepNhay)
        {
            dangGiunPhimNhay = true;
            thoiGianGiunPhimNhay = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            if (dangGiunPhimNhay)
            {
                float lucNhay = Mathf.Clamp(lucNhayToiDa * (Time.time - thoiGianGiunPhimNhay) / thoiGianNhay, 0f, lucNhayToiDa);
                rb.AddForce(Vector2.up * lucNhay, ForceMode2D.Impulse);
                dangGiunPhimNhay = false;
                AudioManager.Instance.PlaySFX("Jump");
            }
        }

        // Nhấp nháy khi trong trạng thái miễn sát thương
        if (immuneToDamage)
        {
            float blinkSpeed = 0.1f; // Tốc độ nhấp nháy
            bool isVisible = Mathf.FloorToInt(Time.time / blinkSpeed) % 2 == 0;
            spriteRenderer.enabled = isVisible;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }
    
    void flip()
    {
        if (isfacingRight && traiPhai < 0 || !isfacingRight && traiPhai > 0)
        {
            isfacingRight = !isfacingRight;
            Vector3 kichThuoc = transform.localScale;
            kichThuoc.x = kichThuoc.x * -1;
            transform.localScale = kichThuoc;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float contactPointY = collision.contacts[0].point.y;
            float enemyTopY = collision.collider.bounds.max.y;

            // Kiểm tra va chạm từ phía trên để tiêu diệt quái
            if (contactPointY > enemyTopY - 0.1944841f)
            {
                KillEnemy(collision.gameObject);
                AudioManager.Instance.PlaySFX("Kill");
            }
            else if (!CanTakeDamage())
            {
                return; // Đang trong thời gian miễn sát thương
            }
            else if (collectedGems > 0)
            {
                DropGems();
                collectedGems = 0;
                SetImmune(true);
                StartCoroutine(ImmuneDuration());
            }
            else
            {
                Die();
            }
        }
    }

    private void KillEnemy(GameObject enemy)
    {
        enemy.GetComponent<diChuyenQuai>().Die();
    }

    private void Die()
    {
        if (lives >= 1)
        {
            //animator.SetTrigger("death");
            lives--;
            SetImmune(true);
            StartCoroutine(ImmuneDuration());
            Vector3 startingPosition = new Vector3(-12.002f, 0.766f, -0.315f); // Điều chỉnh vị trí ban đầu tùy theo yêu cầu
            transform.position = startingPosition;
            

        }
        else
        {
            ResetGame();
            AudioManager.Instance.PlaySFX("Death");

        }
    }

    private void DropGems()
    {
        collectedGems = 0; // Khi va chạm với quái, gem sẽ biến mất
       
        // TODO: Có thể thực hiện hành động nào đó sau khi gem biến mất (nếu cần)
    }

    private IEnumerator ImmuneDuration()
    {
        yield return new WaitForSeconds(immuneDuration);
        SetImmune(false);
    }

    private bool CanTakeDamage()
    {
        return Time.time >= lastHitTime + immuneDuration;
    }

    private void SetImmune(bool immune)
    {
        immuneToDamage = immune;
        lastHitTime = Time.time;
    }
   
    private void OnTriggerEnter2D(Collider2D hitboxkhac)
    {
        switch (hitboxkhac.gameObject.tag)
        {
            case "matDat":
                duocPhepNhay = true;
                break;
            case "Gem":
                collectedGems++;
                AudioManager.Instance.PlaySFX("Collect");
                if (collectedGems % gemsToExtraLife == 0)
                {
                    lives++;
 
                }
                hitboxkhac.gameObject.SetActive(false);
                break;
            case "Win":
                winText.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D hitboxkhac)
    {
        if (hitboxkhac.gameObject.tag == "matDat")
        {
            duocPhepNhay = false;
        }
    }
    private void ResetGame()
    {
        // TODO: Thực hiện các hành động cần thiết để reset trạng thái trò chơi
        // Ví dụ: Reset vị trí của nhân vật, số mạng, số gem đã nhặt, v.v.
        lives = 1;
        collectedGems = 0;
        // Reset vị trí nhân vật
        Vector3 startingPosition = new Vector3(-12.002f, 0.766f, -0.315f); // Điều chỉnh vị trí ban đầu tùy theo yêu cầu
        transform.position = startingPosition;
        SceneManager.LoadScene("Map1");
    }
}
