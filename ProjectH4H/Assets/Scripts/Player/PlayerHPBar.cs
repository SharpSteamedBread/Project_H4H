using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject objPlayer;

    private float currHP;
    private float maxHP;

    private void Awake()
    {
        objPlayer.GetComponent<PlayerStatus>();

        currHP = objPlayer.GetComponent<PlayerStatus>().PlayerCurrHP;
        maxHP = objPlayer.GetComponent<PlayerStatus>().PlayerMaxHP;

        hpBar.GetComponent<Slider>();
    }


    void Update()
    {
        objPlayer.GetComponent<PlayerStatus>();

        currHP = objPlayer.GetComponent<PlayerStatus>().PlayerCurrHP;
        maxHP = objPlayer.GetComponent<PlayerStatus>().PlayerMaxHP;
       
        hpBar.GetComponent<Slider>();
        
        hpBar.value = currHP / maxHP;
    }
}
