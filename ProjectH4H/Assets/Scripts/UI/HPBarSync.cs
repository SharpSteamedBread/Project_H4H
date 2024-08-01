using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarSync : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject objMonster;

    private float currHP;
    private float maxHP;

    private void Awake()
    {

            objMonster.GetComponent<EnemyStatus>();

            currHP = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP;
            maxHP = objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;


         if (gameObject.CompareTag("MidBoss"))
        {
            objMonster.GetComponent<BossStatus>();

            currHP = objMonster.GetComponent<BossStatus>().currHP;
            maxHP = objMonster.GetComponent<BossStatus>().maxHP;
        }


        hpBar.GetComponent<Slider>();
    }

    private void Update()
    {

            objMonster.GetComponent<EnemyStatus>();

            currHP = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP;
            maxHP = objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;
       
         if (gameObject.CompareTag("MidBoss"))
        {
            objMonster.GetComponent<BossStatus>();

            currHP = objMonster.GetComponent<BossStatus>().currHP;
            maxHP = objMonster.GetComponent<BossStatus>().maxHP;
        }

        hpBar.GetComponent<Slider>();

        hpBar.value = currHP / maxHP;

        Debug.Log($"���� ���� ��: {currHP} , �����̴� value: {hpBar.value}");

        if(hpBar.value <= 0 )
        {
            gameObject.SetActive(false);
        }
    }
}
