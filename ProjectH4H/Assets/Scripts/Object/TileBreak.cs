using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBreak : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //this.gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<Animator>().enabled = true;
        }
        
    }
}
