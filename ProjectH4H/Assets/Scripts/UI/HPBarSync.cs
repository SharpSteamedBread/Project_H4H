using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarSync : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject objMonster;

    private void Awake()
    {
        objMonster.GetComponent<EnemyStatus>();
        hpBar.GetComponent<Slider>();
    }

    private void Update()
    {
        hpBar.value = objMonster.GetComponent<EnemyStatus>().EnemyCurrHP / objMonster.GetComponent<EnemyStatus>().EnemyMaxHP;

        if(hpBar.value <= 0 )
        {
            gameObject.SetActive(false);
        }
    }
}
