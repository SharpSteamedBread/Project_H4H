using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnableManager : MonoBehaviour
{
    public void EnableObject()
    {
        gameObject.SetActive(true);
    }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
