using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComboAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private int comboStep;

    [SerializeField] private float lastInputTime = 0f;
    [SerializeField] private float maxComboDelay = 1.0f;

    [Header("UI ¹öÆ°")]
    [SerializeField] private Button objButtonZ;
    [SerializeField] private Button objButtonX;

    private void Awake()
    {
        animator.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ReturnToZero());
    }

    void Update()
    {
        DoComboAttack();
        ConditionControl();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            objButtonZ.OnPointerDown(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            objButtonZ.OnPointerUp(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
            PerformComboAttack(KeyCode.Z);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            objButtonX.OnPointerDown(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            objButtonX.OnPointerUp(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
            PerformComboAttack(KeyCode.X);
        }
    }

    private void PerformComboAttack(KeyCode keyCode)
    {
        lastInputTime = Time.time;
        StartCoroutine(InputDelay(keyCode));
    }

    private IEnumerator InputDelay(KeyCode keyCode)
    {
        yield return new WaitForSeconds(0.1f);
        comboStep++;
        animator.SetBool("isAttack", true);

        SetComboState(keyCode, comboStep);
    }

    private void SetComboState(KeyCode keyCode, int step)
    {
        ResetComboStates();

        if (keyCode == KeyCode.Z)
        {
            if (step == 1) animator.SetBool("ZAttackCombo1", true);
            else if (step == 2) animator.SetBool("ZAttackCombo2", true);
            else if (step == 3) animator.SetBool("ZAttackCombo3", true);
        }
        else if (keyCode == KeyCode.X)
        {
            if (step == 1) animator.SetBool("XAttackCombo1", true);
            else if (step == 2) animator.SetBool("XAttackCombo2", true);
            else if (step == 3) animator.SetBool("XAttackCombo3", true);
        }
    }

    private void ResetComboStates()
    {
        animator.SetBool("ZAttackCombo1", false);
        animator.SetBool("ZAttackCombo2", false);
        animator.SetBool("ZAttackCombo3", false);
        animator.SetBool("XAttackCombo1", false);
        animator.SetBool("XAttackCombo2", false);
        animator.SetBool("XAttackCombo3", false);
    }

    private void ResetAllCombos()
    {
        ResetComboStates();
        comboStep = 0;
        lastInputTime = 0;
    }

    private void DoComboAttack()
    {
        if (Time.time - lastInputTime > maxComboDelay)
        {
            ResetAllCombos();
        }
    }

    private void ConditionControl()
    {
        if (comboStep > 3)
        {
            comboStep = 3;
        }

        if (!animator.GetBool("ZAttackCombo1") && !animator.GetBool("ZAttackCombo2") && !animator.GetBool("ZAttackCombo3") &&
            !animator.GetBool("XAttackCombo1") && !animator.GetBool("XAttackCombo2") && !animator.GetBool("XAttackCombo3"))
        {
            comboStep = 0;
        }

        if (animator.GetBool("isAttack"))
        {
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
            ResetAllCombos();
        }

        if (animator.GetBool("isJumping"))
        {
            animator.SetBool("isAttack", false);
        }
    }

    private IEnumerator ReturnToZero()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            ResetAllCombos();
            animator.SetBool("isAttack", false);
        }
    }
}
