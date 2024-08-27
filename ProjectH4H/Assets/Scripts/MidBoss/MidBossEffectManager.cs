using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossEffectManager : MonoBehaviour
{
    [SerializeField] private Transform midbossFlip;

    [Header("이펙트")]
    [SerializeField] private Transform objVFXLocationPattern2;
    [SerializeField] private GameObject objVFXPattern2;

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

    public void Pattern2EFF()
    {
        GameObject cloneVFXLocationPTN2 = Instantiate(objVFXPattern2, objVFXLocationPattern2.transform.position, objVFXLocationPattern2.transform.rotation);
        cloneVFXLocationPTN2.transform.localScale = midbossFlip.transform.localScale;
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern2;
    }

    public void Pattern4EFF()
    {
        GameObject cloneVFXLocationPTN4 = Instantiate(objVFXPattern4, objVFXLocationPattern4.transform.position, objVFXLocationPattern4.transform.rotation);
        cloneVFXLocationPTN4.transform.localScale = midbossFlip.transform.localScale;
        damageInteractor.midbossDamageType = MidbossDamageType.Pattern4;
    }
}
