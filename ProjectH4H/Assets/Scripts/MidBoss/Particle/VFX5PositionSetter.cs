using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX5PositionSetter : MonoBehaviour
{
    [SerializeField] private string targetName;
    [SerializeField] private Transform transformPlayer;
    //[SerializeField] private Transform transformMidboss;

    private void Awake()
    {
        transformPlayer = GameObject.FindWithTag("Player").transform;
        //transformMidboss = GameObject.FindWithTag("MidBoss").transform;
    }

    void Start()
    {
        gameObject.transform.position = new Vector3(transformPlayer.position.x, transformPlayer.position.y, transformPlayer.position.z);

        /*
          else if(targetName == "MidBoss")
        {
            gameObject.transform.position = new Vector3(transformMidboss.position.x, transformMidboss.position.y, transformMidboss.position.z);
        }
         */

    }
}
