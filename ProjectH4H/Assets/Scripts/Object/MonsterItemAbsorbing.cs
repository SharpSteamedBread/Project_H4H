using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItemAbsorbing : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float objSpeed = 7.0f;

    private void Awake()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position.normalized, playerLocation.position, objSpeed);
    }
}
