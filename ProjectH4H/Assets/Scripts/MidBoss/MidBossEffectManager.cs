using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossEffectManager : MonoBehaviour
{
    [SerializeField] private Transform midbossFlip;

    [Header("이펙트")]
    [SerializeField] private Transform objVFXLocationPattern1;
    [SerializeField] private GameObject objVFXPattern1;

    [Space(10)]
    [SerializeField] private Transform objVFXLocationPattern2;
    [SerializeField] private GameObject objVFXPattern2;

    [Space(10)]
    [SerializeField] private Transform objVFXLocationPattern4;
    [SerializeField] private GameObject objVFXPattern4;

    [Header("히트박스")]
    [SerializeField] private DamageInteractor damageInteractor;


    private void Awake()
    {
        midbossFlip = gameObject.GetComponent<Transform>();
        damageInteractor.GetComponent<DamageInteractor>();
    }

    void Update()
    {
        damageInteractor.GetComponent<DamageInteractor>();
    }

    public void Pattern1EFF()
    {
        GameObject cloneVFXLocationPTN1 = Instantiate(objVFXPattern1, objVFXLocationPattern1.transform.position, objVFXLocationPattern1.transform.rotation);
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern1;
    }

    public void Pattern2EFF()
    {
        objVFXPattern2.SetActive(true);
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern2;
    }

    public void Pattern4EFF()
    {
        GameObject cloneVFXLocationPTN4 = Instantiate(objVFXPattern4, objVFXLocationPattern4.transform.position, objVFXLocationPattern4.transform.rotation);
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern4;
    }
}
