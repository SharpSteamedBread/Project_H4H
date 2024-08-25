using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSquish : MonoBehaviour
{
    [SerializeField] private Sprite objSpriteNormal;
    [SerializeField] private Sprite objSpriteSquished;

    [SerializeField] private SpriteRenderer objSquish;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            objSquish.sprite = objSpriteSquished;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objSquish.sprite = objSpriteNormal;
        }
    }
}
