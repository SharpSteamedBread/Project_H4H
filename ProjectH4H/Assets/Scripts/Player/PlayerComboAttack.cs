using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public int ZBasicAttack;
    public int XBasicAttack;

    float lastInputTime = 0;
    [SerializeField] private float maxComboDelay = 1.0f;


    private void Awake()
    {
        animator.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ReturntoZero());
    }

    void Update()
    {
        DoBasicAttack();
        ConditionControll();
    }

    private void DoBasicAttack()
    {
        if (Time.deltaTime - lastInputTime > maxComboDelay)
        {
            animator.SetBool("ZAttackCombo1", false);
            animator.SetBool("ZAttackCombo2", false);
            animator.SetBool("ZAttackCombo3", false);

            animator.SetBool("XAttackCombo1", false);
            animator.SetBool("XAttackCombo2", false);
            animator.SetBool("XAttackCombo3", false);

            ZBasicAttack = 0;
            XBasicAttack = 0;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            lastInputTime = Time.deltaTime;
            ZBasicAttack++;
            animator.SetBool("isAttack", true);

            //valueCanMove = false;
            //GetComponent<PlayerMove>().canMove = valueCanMove;

            if (ZBasicAttack == 1)
            {
                animator.SetBool("ZAttackCombo1", true);

                if (animator.GetBool("isJumping") == false)
                {
                    //AudioManager.instance.PlaySFX("PlayerAttack1Effect");
                    //AudioManager.instance.PlaySFX("PlayerAttack1Voice");
                }
            }
            //basicAttack = Mathf.Clamp(basicAttack, 0, 3);
        }

        else if(Input.GetKeyUp(KeyCode.X))
        {
            lastInputTime = Time.deltaTime;
            XBasicAttack++;
            animator.SetBool("isAttack", true);

            //valueCanMove = false;
            //GetComponent<PlayerMove>().canMove = valueCanMove;

            if (XBasicAttack == 1)
            {
                animator.SetBool("XAttackCombo1", true);

                if (animator.GetBool("isJumping") == false)
                {
                    //AudioManager.instance.PlaySFX("PlayerAttack1Effect");
                    //AudioManager.instance.PlaySFX("PlayerAttack1Voice");
                }
            }
            //basicAttack = Mathf.Clamp(basicAttack, 0, 3);
        }
    }

    public void LinkCombo2()
    {
        if (ZBasicAttack >= 2)
        {
            animator.SetBool("ZAttackCombo2", true);
            animator.SetBool("ZAttackCombo1", false);

            if (animator.GetBool("isJumping") == false)
            {
                //AudioManager.instance.PlaySFX("PlayerAttack2Effect");
                //AudioManager.instance.PlaySFX("PlayerAttack2Voice");
            }
        }

        else
        {
            animator.SetBool("ZAttackCombo1", false);
            ZBasicAttack = 0;
            animator.SetBool("isAttack", true);
            //AttackMove();
        }

        if (XBasicAttack >= 2)
        {
            animator.SetBool("XAttackCombo2", true);
            animator.SetBool("XAttackCombo1", false);

            if (animator.GetBool("isJumping") == false)
            {
                //AudioManager.instance.PlaySFX("PlayerAttack2Effect");
                //AudioManager.instance.PlaySFX("PlayerAttack2Voice");
            }
        }

        else
        {
            animator.SetBool("XAttackCombo1", false);
            XBasicAttack = 0;
            animator.SetBool("isAttack", true);
            //AttackMove();
        }
    }

    public void LinkCombo3()
    {
        if (ZBasicAttack >= 3)
        {
            animator.SetBool("ZAttackCombo3", true);
            animator.SetBool("ZAttackCombo2", false);

            if (animator.GetBool("isJumping") == false)
            {
                //AudioManager.instance.PlaySFX("PlayerAttack3Effect");
                //AudioManager.instance.PlaySFX("PlayerAttack3Voice");
            }
        }

        else
        {
            animator.SetBool("ZAttackCombo2", false);
            ZBasicAttack = 0;
            animator.SetBool("isAttack", true);
            //AttackMove();
        }

        if (XBasicAttack >= 3)
        {
            animator.SetBool("XAttackCombo3", true);
            animator.SetBool("XAttackCombo2", false);

            if (animator.GetBool("isJumping") == false)
            {
                //AudioManager.instance.PlaySFX("PlayerAttack3Effect");
                //AudioManager.instance.PlaySFX("PlayerAttack3Voice");
            }
        }

        else
        {
            animator.SetBool("XAttackCombo2", false);
            XBasicAttack = 0;
            //AttackMove();
        }
    }

    public void LinkComboFinish()
    {
        animator.SetBool("ZAttackCombo1", false);
        animator.SetBool("ZAttackCombo2", false);
        animator.SetBool("ZAttackCombo3", false);
        ZBasicAttack = 0;

        animator.SetBool("XAttackCombo1", false);
        animator.SetBool("XAttackCombo2", false);
        animator.SetBool("XAttackCombo3", false);
        XBasicAttack = 0;

        animator.SetBool("isAttack", true);
    }

    private void ConditionControll()
    {
        if (ZBasicAttack > 3)
        {
            ZBasicAttack = 3;
        }


        if (animator.GetBool("ZAttackCombo1") == false && animator.GetBool("ZAttackCombo2") == false
            && animator.GetBool("ZAttackCombo3") == false)
        {
            ZBasicAttack = 0;
        }

        if (XBasicAttack > 3)
        {
            XBasicAttack = 3;
        }


        if (animator.GetBool("XAttackCombo1") == false && animator.GetBool("XAttackCombo2") == false
            && animator.GetBool("XAttackCombo3") == false)
        {
            XBasicAttack = 0;
        }

        if (animator.GetBool("isAttack") == true)
        {
            animator.SetBool("Idle", false);
        }

        else
        {
            //animator.SetBool("Idle", true);

            animator.SetBool("ZAttackCombo1", false);
            animator.SetBool("ZAttackCombo2", false);
            animator.SetBool("ZAttackCombo3", false);
            ZBasicAttack = 0;

            animator.SetBool("XAttackCombo1", false);
            animator.SetBool("XAttackCombo2", false);
            animator.SetBool("XAttackCombo3", false);
            XBasicAttack = 0;
        }

        if (animator.GetBool("Idle") == true)
        {
            animator.SetBool("isAttack", false);
        }

        if (animator.GetBool("isJumping") == true)
        {
            animator.SetBool("isAttack", false);
        }
    }

    private IEnumerator ReturntoZero()
    {
        yield return new WaitForSeconds(4f);

        animator.SetBool("ZAttackCombo1", false);
        animator.SetBool("ZAttackCombo2", false);
        animator.SetBool("ZAttackCombo3", false);
        ZBasicAttack = 0;

        animator.SetBool("XAttackCombo1", false);
        animator.SetBool("XAttackCombo2", false);
        animator.SetBool("XAttackCombo3", false);
        XBasicAttack = 0;

        animator.SetBool("isAttack", false);

        StartCoroutine(ReturntoZero());
    }

}
