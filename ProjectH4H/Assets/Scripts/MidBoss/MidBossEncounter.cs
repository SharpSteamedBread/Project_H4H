using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MidBossEncounter : MonoBehaviour
{
    [SerializeField] private GameObject objPlayerCam;

    [SerializeField] private GameObject objMidBoss;
    [SerializeField] private GameObject objMidBossCam;
    [SerializeField] private GameObject objMidBossUI;

    [SerializeField] private GameObject objParticleUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            WelcomePlayer();
            this.gameObject.SetActive(false);
        }
        
    }

    private void WelcomePlayer()
    {
        objPlayerCam.SetActive(false);
        objMidBossCam.SetActive(true);
        objMidBoss.SetActive(true);
        objMidBossUI.SetActive(true);
        objParticleUI.SetActive(true);

    }
}
