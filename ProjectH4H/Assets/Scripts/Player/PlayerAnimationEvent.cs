using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject objPlayerHitbox;

    public void EnablePlayerHitbox()
    {
        objPlayerHitbox.SetActive(true);
    }

    public void DisablePlayerHitbox()
    {
        objPlayerHitbox.SetActive(false);
    }
}
