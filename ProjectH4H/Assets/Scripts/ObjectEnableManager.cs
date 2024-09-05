using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnableManager : MonoBehaviour
{
    public void EnableObject()
    {
        gameObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
