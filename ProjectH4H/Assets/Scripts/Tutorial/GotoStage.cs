using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoStage : MonoBehaviour
{
    [SerializeField] private GameObject objLoadingBlur;
    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            objLoadingBlur.SetActive(true);
        }
    }
}
