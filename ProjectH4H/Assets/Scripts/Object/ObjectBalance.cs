using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBalance : MonoBehaviour
{
    [SerializeField] private Transform objTransform;
    [SerializeField] private bool isPlayerOn = false;

    private void Awake()
    {
        objTransform = this.gameObject.GetComponent<Transform>();

    }

    void FixedUpdate()
    {
       if(isPlayerOn == false)
        {
            objTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f,
                                         Mathf.Lerp(objTransform.rotation.z, 0, 5f * Time.deltaTime)));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
        }
    }
}
