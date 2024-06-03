using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject objPlayerHitbox;

    private void Awake()
    {
        objPlayerHitbox.GetComponent<BoxCollider2D>();
    }

    public void EnablePlayerHitbox()
    {
        objPlayerHitbox.SetActive(true);
        objPlayerHitbox.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisablePlayerHitbox()
    {
        objPlayerHitbox.SetActive(false);
    }

    public void DisablePlayerHitboxRigidbody()
    {
        objPlayerHitbox.GetComponent<BoxCollider2D>().enabled = false;

    }
}
