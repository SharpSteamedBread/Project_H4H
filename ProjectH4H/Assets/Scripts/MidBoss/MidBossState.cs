using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class MidBossState : MonoBehaviour
{
  public enum Boss1State { IDLE, SELECTPATTERN, PATTERN1, PATTERN2, PATTERN3, PATTERN4, PATTERN5, DIE, }

  public Boss1State bossState = Boss1State.PATTERN1;

  private Animator animator;

  [Header("플레이어 감지")]
  [SerializeField] private GameObject target;
  private Transform targetPos;
  [SerializeField] private float moveSpeed = 1.0f;
  [SerializeField] private Transform targetRange;
  [SerializeField] private Vector2 attack1Area;


  [Header("공격")]
  [SerializeField] private float attackCooltime = 3.0f;
  [SerializeField] private float valuePatternChangeTime = 0f;
  [SerializeField] private float patternChangeTimeMin = 3f;
  [SerializeField] private float patternChangeTimeMax = 5.0f;
  [SerializeField] private int randomPattern = 1;


    [Header("패턴 1 조건 변수")]
    [SerializeField] private GameObject objPlayerCam;
    [SerializeField] private GameObject objMidBossCam;


    [Header("패턴 2 조건 변수")]
    private float pattern2Dur = 10.0f;
    [SerializeField] private int pattern2Phase = 0;
    [SerializeField] private GameObject objEffectPattern2;


    [Header("패턴 4 조건 변수")]
    [SerializeField] private Vector3 pattern4Pos;
    [SerializeField] private Vector2 pattern4PlayerDetectArea;
    [SerializeField] private Rigidbody2D bossRigidbody2D;
    [SerializeField] private float bossJumpForce = 20;

    [Header("데미지 관리")]
    public UnityEvent onEnemyDamaged;
    public GameObject objDamageInteractor;

    [Header("Scale 조정")]
    private float bossScale = 1.1f;

    [SerializeField] private CommandEnterUI activateCommandSkill;


    private Transform bossTransform;

    private float runTime = 0.0f;


    private int valueBossHP;
    private SpriteRenderer bossSpriteRenderer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossRigidbody2D = GetComponent<Rigidbody2D>();

        StateChange(Boss1State.PATTERN1);
        //StateChange(Boss1State.PATTERN4);

        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = target.GetComponent<Transform>();
        targetRange = target.GetComponent<Transform>();

        valueBossHP = gameObject.GetComponent<BossStatus>().bossHP;
        bossSpriteRenderer = GetComponent<SpriteRenderer>();

        //objStageController = GameObject.FindGameObjectWithTag("StageController");
        //objStageController.GetComponent<StageController>();

        bossTransform = GetComponent<Transform>();
        transform.position = bossTransform.transform.position;

        //데미지 관련
        objDamageInteractor.GetComponent<DamageInteractor>();
        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");
        activateCommandSkill = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CommandEnterUI>();
    }

    private void Update()
    {
        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");
        activateCommandSkill = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CommandEnterUI>();

        ReadyToDie();
        Debug.Log(bossState);

        FaceTarget();
    }

    public void StateChange(Boss1State newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator IDLE()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("isMoving", false);
        animator.SetBool("midBossPattern2", false);

        StopCoroutine(Move());

        valuePatternChangeTime = Random.Range(patternChangeTimeMin, patternChangeTimeMax);
        //Debug.Log($"{valuePatternChangeTime}초 뒤 패턴 변경~");

        yield return new WaitForSeconds(valuePatternChangeTime);

        StateChange(Boss1State.SELECTPATTERN);
    }

    private IEnumerator SELECTPATTERN()
    {
        StartCoroutine(StopMove());

        float distance = Vector2.Distance(targetPos.position, transform.position);
        
        if(distance >= pattern4PlayerDetectArea.x / 2)
        {
            StateChange(Boss1State.PATTERN4);
        }

        else 
        {
            randomPattern = Random.Range(1, 6);
            Debug.Log($"패턴은 {randomPattern}!");

            switch (randomPattern)
            {
                case (1):
                    StateChange(Boss1State.PATTERN2);
                    break;

                case (2):
                    StateChange(Boss1State.PATTERN2);
                    break;

                case (3):
                    StateChange(Boss1State.PATTERN2);
                    break;

                case (4):
                    StateChange(Boss1State.IDLE);
                    break;

                case (5):
                    StateChange(Boss1State.IDLE);
                    break;
            }
        }

        yield return null;
    }

    private IEnumerator PATTERN1()
    {
        animator.SetTrigger("midBossPattern1");

        yield return null;
    }

    public void BossCameraOn()
    {
        objPlayerCam.SetActive(false);
        objMidBossCam.SetActive(true);
    }

    public void BossCameraOff()
    {
        objPlayerCam.SetActive(true);
        objMidBossCam.SetActive(false);

        StateChange(Boss1State.IDLE);
    }

    private IEnumerator PATTERN2()
    {

        runTime = 0f;

        while (runTime < pattern2Dur)
        {
            runTime += Time.deltaTime;

            //Debug.Log($"시간 경과 {runTime}초!");

            animator.SetBool("midBossPattern2", true);
            StartCoroutine(Move());

            objEffectPattern2.SetActive(true);

            yield return null;
        }

        objEffectPattern2.SetActive(false);
        StateChange(Boss1State.IDLE);
    }


    private IEnumerator PATTERN3()
    {
        StartCoroutine(Pattern3MoveTo());

        yield return null;
    }

    private IEnumerator Pattern3MoveTo()
    {
        while (transform.position != pattern4Pos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pattern4Pos, Time.deltaTime * moveSpeed);

            yield return null;
        }

        StartCoroutine(Pattern3SummonTanuki());

        yield return null;
    }

    private IEnumerator Pattern3SummonTanuki()
    {
        //AudioManager.instance.PlaySFX("Boss_Pattern3_On_Voice_1");

        animator.SetBool("bossPattern3", true);
        //Instantiate(pattern4Tanuki, targetPos.position, Quaternion.identity);

        yield return new WaitForSeconds(5f);


        animator.SetBool("bossPattern3", false);

        //AudioManager.instance.PlaySFX("Boss_Pattern3_Off_Voice_2");

        StateChange(Boss1State.IDLE);
        StopCoroutine(Pattern4SummonDalma());
    }

    private IEnumerator PATTERN4()
    {
        animator.SetTrigger("midBossPattern4");

        yield return null;
    }

    public void Pattern4Jump()
    {
        transform.position = new Vector2(targetPos.position.x, transform.position.y);

        bossRigidbody2D.AddForce(Vector2.up * bossJumpForce, ForceMode2D.Impulse);
    }

    public void Pattern4Shot()
    {
        bossRigidbody2D.AddForce(Vector2.down * bossJumpForce * 1.5f, ForceMode2D.Impulse);
        BossCameraOn();
    }

    public void EndPattern4()
    {
        StateChange(Boss1State.IDLE);
    }

    private IEnumerator Pattern4MoveTo()
    {
        while (transform.position != pattern4Pos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pattern4Pos, Time.deltaTime * moveSpeed);

            yield return null;
        }

        StartCoroutine(Pattern4SummonDalma());

        yield return null;
    }

    private IEnumerator Pattern4SummonDalma()
    {
        animator.SetBool("bossPattern4", true);


        yield return new WaitForSeconds(5f);

        animator.SetBool("bossPattern4", false);

        StateChange(Boss1State.IDLE);
        StopCoroutine(Pattern4SummonDalma());
    }

    private IEnumerator PATTERN5()
    {
        animator.SetBool("bossPattern5", true);

        //AudioManager.instance.PlaySFX("Boss_Pattern5_thunder_Voice");

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("bossPattern5", false);
        StateChange(Boss1State.IDLE);

    }

    private IEnumerator DIE()
    {
        StartCoroutine(StopMove());
        animator.SetBool("isDie", true);

        //objUITheEnd.SetActive(true);

        yield return null;
    }

    private IEnumerator StopMove()
    {
        StopCoroutine(IDLE());
        animator.SetBool("isMoving", false);

        animator.SetBool("Idle", true);
        animator.SetBool("midBossPattern2", false);


        yield return null;
    }

    private void ReadyToDie()
    {
        valueBossHP = gameObject.GetComponent<BossStatus>().currHP;

        if (valueBossHP <= 0)
        {
            StateChange(Boss1State.DIE);
        }
    }

    void FaceTarget()
    {
        if (targetRange.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            bossTransform.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        else // 타겟이 오른쪽에 있을 때
        {
            bossTransform.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private IEnumerator Move()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("Idle", false);

        Debug.Log("움직임~");

        float dirX = targetPos.position.x - transform.position.x;
        dirX = (dirX < 0) ? -1 : 1;

        float dirY = targetPos.position.y - transform.position.y + 0.5f;
        dirY = (dirY < 0) ? -1 : 1;

        transform.Translate(new Vector2(dirX, dirY) * moveSpeed * Time.deltaTime);

        yield return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, pattern4PlayerDetectArea);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            objDamageInteractor.GetComponent<DamageInteractor>();

            valueBossHP = gameObject.GetComponent<BossStatus>().currHP;
            gameObject.GetComponent<BossStatus>().currHP -= objDamageInteractor.GetComponent<DamageInteractor>().CalculateDamage();


            //onEnemyDamaged.Invoke();
            CritRate();
        }
    }

    private void CritRate()
    {
        int crit = Random.Range(1, 101);
        //Debug.Log($"크리티컬 수치: {crit}");

        if (crit >= 80)
        {
            activateCommandSkill.TimeToSkillCommand();
        }
    }
}
