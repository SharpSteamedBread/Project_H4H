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
        hpBar.GetComponent<Slider>();

        currHP = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP;
        maxHP = objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;
    }

    private void Update()
    {
        currHP = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP;
        maxHP = objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;

        hpBar.value = currHP / maxHP;

        Debug.Log($"몬스터 현재 피: {currHP} , 슬라이더 value: {hpBar.value}");

        if(hpBar.value <= 0 )
        {
            gameObject.SetActive(false);
        }
    }
}
