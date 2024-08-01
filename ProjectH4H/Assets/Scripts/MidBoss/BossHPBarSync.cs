using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarSync : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject objMonster;

    private float currHP;
    private float maxHP;

    private void Awake()
    {

            objMonster.GetComponent<BossStatus>();

            currHP = objMonster.GetComponent<BossStatus>().currHP;
            maxHP = objMonster.GetComponent<BossStatus>().maxHP;

        hpBar.GetComponent<Slider>();
    }

    private void Update()
    {
            objMonster.GetComponent<BossStatus>();

            currHP = objMonster.GetComponent<BossStatus>().currHP;
            maxHP = objMonster.GetComponent<BossStatus>().maxHP;

        hpBar.GetComponent<Slider>();

        hpBar.value = currHP / maxHP;

        Debug.Log($"몬스터 현재 피: {currHP} , 슬라이더 value: {hpBar.value}");

        if (hpBar.value <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
