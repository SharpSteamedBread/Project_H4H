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
            Debug.Log("��!");
            playerCollider.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("�վ���!");
            playerCollider.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }
}
