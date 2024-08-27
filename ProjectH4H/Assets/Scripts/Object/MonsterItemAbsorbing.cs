using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItemAbsorbing : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float objSpeed = 7.0f;

    [Header("회복량")]
    [SerializeField] private int healAmount = 40;

    private void Awake()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, objSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log($"회복 전: {collision.GetComponent<PlayerStatus>().PlayerCurrHP}");

            collision.GetComponent<PlayerStatus>().PlayerCurrHP += healAmount;

            //Debug.Log($"회복된다~ / 회복 후: {collision.GetComponent<PlayerStatus>().PlayerCurrHP}");
            Destroy(gameObject);
        }
    }
}
