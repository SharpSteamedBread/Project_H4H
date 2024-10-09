using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private Transform playerFlip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody2D playerRigidbody;

    [Header("평타")]
    [SerializeField] private Transform objVFXLocationZ1;
    [SerializeField] private GameObject objVFXZ1;
    [SerializeField] private Transform objVFXLocationZ2;
    [SerializeField] private GameObject objVFXZ2;
    [SerializeField] private Transform objVFXLocationZ3;
    [SerializeField] private GameObject objVFXZ3;

    [SerializeField] private Transform objVFXLocationX1;
    [SerializeField] private GameObject objVFXX1;
    [SerializeField] private Transform objVFXLocationX2;
    [SerializeField] private GameObject objVFXX2;
    [SerializeField] private Transform objVFXLocationX3;
    [SerializeField] private GameObject objVFXX3;

    [Header("스킬 5")]
    [SerializeField] private Transform objVFXLocationSkill5;
    [SerializeField] private GameObject objVFXSkill5Left;
    [SerializeField] private GameObject objVFXSkill5Right;

    [Header("스킬 6")]
    [SerializeField] private Transform objVFXLocationSkill6;
    [SerializeField] private GameObject objVFXSkill6;
    [SerializeField] private float skill6JumpForce = 10f;

    [Header("히트박스")]
    [SerializeField] private DamageInteractor damageInteractor;


    private void Awake()
    {
        playerFlip = gameObject.GetComponent<Transform>();
        audioSource = gameObject.GetComponent<AudioSource>();
        damageInteractor.GetComponent<DamageInteractor>();
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        damageInteractor.GetComponent<DamageInteractor>();
    }

    public void ZAttackCombo1()
    {
        GameObject cloneVFXLocationZ1 = Instantiate(objVFXZ1, objVFXLocationZ1.transform.position, objVFXZ1.transform.rotation);

        if(playerFlip.transform.localScale.x < 0)
        {
            cloneVFXLocationZ1.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }

        else
        {
            cloneVFXLocationZ1.transform.rotation = objVFXZ1.transform.rotation;
        }

        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackZ;
    }

    public void ZAttackCombo2()
    {
        GameObject cloneVFXLocationZ2 = Instantiate(objVFXZ2, objVFXLocationZ2.transform.position, objVFXZ2.transform.rotation);

        if (playerFlip.transform.localScale.x < 0)
        {
            cloneVFXLocationZ2.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }

        else
        {
            cloneVFXLocationZ2.transform.rotation = objVFXZ2.transform.rotation;
        }

        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackZ;
    }
    public void ZAttackCombo3()
    {
        GameObject cloneVFXLocationZ3 = Instantiate(objVFXZ3, objVFXLocationZ3.transform.position, objVFXZ3.transform.rotation);

        if (playerFlip.transform.localScale.x < 0)
        {
            cloneVFXLocationZ3.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }

        else
        {
            cloneVFXLocationZ3.transform.rotation = objVFXZ3.transform.rotation;
        }

        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackZ;
    }

    public void XAttackCombo1()
    {
        GameObject cloneVFXLocationX1 = Instantiate(objVFXX1, objVFXLocationX1.transform.position, objVFXX1.transform.rotation);

        if (playerFlip.transform.localScale.x < 0)
        {
            cloneVFXLocationX1.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }

        else
        {
            cloneVFXLocationX1.transform.rotation = objVFXX1.transform.rotation;
        }

        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackX;
    }

    public void XAttackCombo2()
    {
        GameObject cloneVFXLocationX2 = Instantiate(objVFXX2, objVFXLocationX2.transform.position, objVFXX2.transform.rotation);

        if (playerFlip.transform.localScale.x < 0)
        {
            cloneVFXLocationX2.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }

        else
        {
            cloneVFXLocationX2.transform.rotation = objVFXX2.transform.rotation;
        }

        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackX;
    }

    public void XAttackCombo3()
    {
        GameObject cloneVFXLocationX3 = Instantiate(objVFXX3, objVFXLocationX3.transform.position, objVFXX3.transform.rotation);
        
        if (playerFlip.transform.localScale.x < 0)
        {
            cloneVFXLocationX3.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }

        else
        {
            cloneVFXLocationX3.transform.rotation = objVFXX3.transform.rotation;
        }

        damageInteractor.playerDamageType = PlayerDamageType.ComboAttackX;
    }


    public void PlayerSkill5()
    {

        if (playerFlip.transform.localScale.x < 0)
        {
            GameObject cloneVFXLocationSkill5 = Instantiate(objVFXSkill5Right, objVFXLocationSkill5.transform.position, objVFXSkill5Right.transform.rotation);
        }

        else
        {
            GameObject cloneVFXLocationSkill5 = Instantiate(objVFXSkill5Left, objVFXLocationSkill5.transform.position, objVFXLocationSkill5.transform.rotation);
        }

        damageInteractor.playerDamageType = PlayerDamageType.PlayerSkill5;
    }

    public void PlayerSkill6()
    {
        GameObject cloneVFXLocationSkill6 = Instantiate(objVFXSkill6, objVFXLocationSkill6.transform.position, objVFXLocationSkill6.transform.rotation);
        //cloneVFXLocationSkill6.transform.localScale = playerFlip.transform.localScale;
        damageInteractor.playerDamageType = PlayerDamageType.PlayerSkill6;
    }

    public void PlayerSkill6Jump()
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, skill6JumpForce * Time.unscaledDeltaTime);
        playerRigidbody.AddForce(Vector2.up * skill6JumpForce, ForceMode2D.Impulse);

    }
}
