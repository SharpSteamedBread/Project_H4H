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
        if(objMonster.CompareTag("Enemy1"))
        {
            objMonster.GetComponent<EnemyStatus>();

            currHP = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP;
            maxHP = objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;
        }

        else if(objMonster.CompareTag("Enemy3"))
        {
            objMonster.GetComponent<EnemyThirdStatus>();

            currHP = objMonster.GetComponent<EnemyThirdStatus>().EnemyCurrHP;
            maxHP = objMonster.GetComponent<EnemyThirdStatus>().EnemyMaxHP;

        }

        if (objMonster.CompareTag("MidBoss"))
        {
            objMonster.GetComponent<BossStatus>();

            currHP = objMonster.GetComponent<BossStatus>().currHP;
            maxHP = objMonster.GetComponent<BossStatus>().maxHP;
        }


        hpBar.GetComponent<Slider>();
    }

    private void Update()
    {

        if (objMonster.CompareTag("Enemy1"))
        {
            objMonster.GetComponent<EnemyStatus>();

            currHP = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP;
            maxHP = objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;
        }

        else if (objMonster.CompareTag("Enemy3"))
        {
            objMonster.GetComponent<EnemyThirdStatus>();

            currHP = objMonster.GetComponent<EnemyThirdStatus>().EnemyCurrHP;
            maxHP = objMonster.GetComponent<EnemyThirdStatus>().EnemyMaxHP;

        }

        if (objMonster.CompareTag("MidBoss"))
        {
            objMonster.GetComponent<BossStatus>();

            currHP = objMonster.GetComponent<BossStatus>().currHP;
            maxHP = objMonster.GetComponent<BossStatus>().maxHP;
        }

        hpBar.GetComponent<Slider>();

        hpBar.value = currHP / maxHP;

        //Debug.Log($"몬스터 현재 피: {currHP} , 슬라이더 value: {hpBar.value}");

        if(hpBar.value <= 0 )
        {
            gameObject.SetActive(false);
        }
    }
}
