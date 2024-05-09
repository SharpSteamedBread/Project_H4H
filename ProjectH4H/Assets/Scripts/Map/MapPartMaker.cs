using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPartMaker : MonoBehaviour
{
    [SerializeField] private GameObject objMapPart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(objMapPart);
            gameObject.SetActive(false);
        }
    }
}
