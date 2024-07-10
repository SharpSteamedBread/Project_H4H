using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThroughGround : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCollider;

    private void Awake()
    {
        playerCollider.GetComponent<BoxCollider2D>();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerCollider.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerCollider.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
