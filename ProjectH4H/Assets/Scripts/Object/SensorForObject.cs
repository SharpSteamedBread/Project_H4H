using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorForObject : MonoBehaviour
{
    [SerializeField] private GameObject objInteractObject;
    [SerializeField] private Animator animInteractObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(objInteractObject.CompareTag("Object_falling"))
            {
                objInteractObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            else if (objInteractObject.CompareTag("Object_hide"))
            {
                objInteractObject.SetActive(true);
                animInteractObject.SetTrigger("isEnabled");
            }
        }
    }
}
