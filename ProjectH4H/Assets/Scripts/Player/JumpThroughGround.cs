using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThroughGround : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D playerCollider;

    private void Awake()
    {
        playerCollider.GetComponent<CapsuleCollider2D>();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Äá!");
            playerCollider.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("¶Õ¾ú´Ù!");
            playerCollider.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }
}
