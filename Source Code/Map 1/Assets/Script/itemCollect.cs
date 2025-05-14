using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class itemCollect : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fox")
        {
            Destroy(gameObject); // Hủy đối tượng gem sau khi lụm
        }
    }
    
}
