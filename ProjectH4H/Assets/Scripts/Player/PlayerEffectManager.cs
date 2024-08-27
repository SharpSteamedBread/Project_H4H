using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private Transform playerFlip;
    [SerializeField] private AudioSource audioSource;

    [Header("이펙트")]
    [SerializeField] private Transform objVFXLocationZ1;
    [SerializeField] private GameObject objVFXZ1;
    [SerializeField] private Transform objVFXLocationZ2;
    [SerializeField] private GameObject objVFXZ2;
    [SerializeField] private Transform objVFXLocationZ3;
    [SerializeField] private GameObject objVFXZ3;

    [SerializeField] private AudioClip objSFX_X1;
    [SerializeField] private AudioClip objSFX_X2;
    [SerializeField] private AudioClip objSFX_X3;


    [SerializeField] private Transform objVFXLocationSkill5;
    [SerializeField] private GameObject objVFXSkill5;
    [SerializeField] private Transform objVFXLocationSkill6;
    [SerializeField] private GameObject objVFXSkill6;

    [Header("히트박스")]
    [SerializeField] private DamageInteractor damageInteractor;


    private void Awake()
    {
        playerFlip = gameObject.GetComponent<Transform>();
        audioSource = gameObject.GetComponent<AudioSource>();
        damageInteractor.GetComponent<DamageInteractor>();
    }

    private void Update()
    {
        damageInteractor.GetComponent<DamageInteractor>();
    }

    public void ZAttackCombo1()
    {
        GameObject cloneVFXLocationZ1 = Instantiate(objVFXZ1, objVFXLocationZ1.transform.position, objVFXZ1.transform.rotation);
        cloneVFXLocationZ1.transform.localScale = playerFlip.transform.localScale;
        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackZ;
    }

    public void ZAttackCombo2()
    {
        GameObject cloneVFXLocationZ2 = Instantiate(objVFXZ2, objVFXLocationZ2.transform.position, objVFXZ2.transform.rotation);
        cloneVFXLocationZ2.transform.localScale = playerFlip.transform.localScale;
        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackZ;
    }
    public void ZAttackCombo3()
    {
        GameObject cloneVFXLocationZ3 = Instantiate(objVFXZ3, objVFXLocationZ3.transform.position, objVFXZ3.transform.rotation);
        cloneVFXLocationZ3.transform.localScale = playerFlip.transform.localScale;
        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackZ;
    }

    public void XAttackCombo1()
    {
        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackX;
        audioSource.clip = objSFX_X1;
        audioSource.Play();
    }

    public void XAttackCombo2()
    {
        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackX;
        audioSource.clip = objSFX_X2;
        audioSource.Play();
    }

    public void XAttackCombo3()
    {
        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackX;
        audioSource.clip = objSFX_X3;
        audioSource.Play();
    }


    public void PlayerSkill5()
    {
        GameObject cloneVFXLocationSkill5 = Instantiate(objVFXSkill5, objVFXLocationSkill5.transform.position, objVFXLocationSkill5.transform.rotation);
        cloneVFXLocationSkill5.transform.localScale = playerFlip.transform.localScale;
        damageInteractor.playerDamageType = PlayerDamageType.PlayerSkill5;
    }

    public void PlayerSkill6()
    {
        GameObject cloneVFXLocationSkill6 = Instantiate(objVFXSkill6, objVFXLocationSkill6.transform.position, objVFXLocationSkill6.transform.rotation);
        cloneVFXLocationSkill6.transform.localScale = playerFlip.transform.localScale;
        damageInteractor.playerDamageType = PlayerDamageType.PlayerSkill6;
    }
}
