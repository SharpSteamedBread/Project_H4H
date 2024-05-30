using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorForObject : MonoBehaviour
{
    [SerializeField] private GameObject objInteractObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("때릴 것이다!");
            objInteractObject.SetActive(true);
        }
    }
}
