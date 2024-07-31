using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    [SerializeField] private int pattern2Phase = 0;


    [Header("패턴 4 조건 변수")]
    [SerializeField] private Vector3 pattern4Pos;


    private Transform bossTransform;

    private float runTime = 0.0f;
    private float pattern1Dur = 10.0f;


    private int valueBossHP;
    private SpriteRenderer bossSpriteRenderer;


    private void Awake()
    {
        animator = GetComponent<Animator>();

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
    }

    private void Update()
    {
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

        StopCoroutine(Move());
        StopCoroutine(PATTERN2Attack());

        valuePatternChangeTime = Random.Range(patternChangeTimeMin, patternChangeTimeMax);
        //Debug.Log($"{valuePatternChangeTime}초 뒤 패턴 변경~");

        yield return new WaitForSeconds(valuePatternChangeTime);

        StateChange(Boss1State.IDLE);
        //StateChange(Boss1State.SELECTPATTERN);
    }

    private IEnumerator SELECTPATTERN()
    {
        StartCoroutine(StopMove());
        randomPattern = Random.Range(1, 6);
        Debug.Log($"패턴은 {randomPattern}!");

        switch (randomPattern)
        {
            case (1):
                StateChange(Boss1State.PATTERN1);
                break;

            case (2):
                StateChange(Boss1State.PATTERN2);
                break;

            case (3):
                StateChange(Boss1State.PATTERN3);
                break;

            case (4):
                StateChange(Boss1State.PATTERN4);
                break;

            case (5):
                StateChange(Boss1State.PATTERN5);
                break;

        }

        yield return null;

    }

    private IEnumerator PATTERN1()
    {
        animator.SetTrigger("midBossPattern1");

        yield return null;
    }

    public void SwitchCamera()
    {
        objPlayerCam.SetActive(true);
        objMidBossCam.SetActive(false);

        StateChange(Boss1State.IDLE);
    }

    private IEnumerator PATTERN2()
    {
        StartCoroutine(PATTERN2Attack());

        runTime = 0f;

        while (runTime < pattern1Dur)
        {
            runTime += Time.deltaTime;

            //Debug.Log($"시간 경과 {runTime}초!");

            StartCoroutine(Move());

            yield return null;
        }

        StopCoroutine(PATTERN2Attack());
        StateChange(Boss1State.IDLE);
    }

    private IEnumerator PATTERN2Attack()
    {
        //공격 애니메이션 및 데미지 변화 실행
        valuePatternChangeTime = Random.Range(patternChangeTimeMin, patternChangeTimeMax);
        //Debug.Log($"{runTime}초 까지 {valuePatternChangeTime}초 뒤 공격~");

        yield return new WaitForSeconds(valuePatternChangeTime);

        float distance = Vector2.Distance(targetPos.position, transform.position);

        //Debug.Log($"플레이어-몬스터 거리: {distance}, 감지 범위: {detectArea.x / 2}");

        if (distance <= attack1Area.x / 2)
        {
            animator.SetTrigger("bossPattern1");
        }


        if (runTime >= pattern1Dur)
        {
            Debug.Log("공격 그만~");
            StopCoroutine(PATTERN2Attack());
        }

        else
        {
            StartCoroutine(PATTERN2Attack());
        }
    }

    private IEnumerator PATTERN22()
    {
        animator.SetBool("bossPattern2", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("bossPattern2", false);

        //외치는 모션 재생 후 패턴 1로 넘어감
        StartCoroutine(PATTERN2Summon());

        StateChange(Boss1State.PATTERN1);
    }

    private IEnumerator PATTERN2Summon()
    {
        pattern2Phase++;

        switch (pattern2Phase)
        {
            case (1):
                Pattern2Summon1();
                break;

            case (2):
                yield return new WaitForSeconds(30f);

                animator.SetBool("bossPattern2", true);
                Pattern2Summon2();
                yield return new WaitForSeconds(3f);
                animator.SetBool("bossPattern2", false);

                break;

            case (3):
                yield return new WaitForSeconds(30f);

                animator.SetBool("bossPattern2", true);
                Pattern2Summon3();
                yield return new WaitForSeconds(3f);
                animator.SetBool("bossPattern2", false);

                break;
        }

        if (pattern2Phase >= 3)
        {
            StateChange(Boss1State.IDLE);
            StopCoroutine(PATTERN2Summon());
        }

        yield return null;

    }

    private void Pattern2Summon1()
    {
        Debug.Log("1페이즈 몬스터 소환~");

        //AudioManager.instance.PlaySFX("Boss_Pattern2Voice");
    }

    private void Pattern2Summon2()
    {
        Debug.Log("2페이즈 몬스터 소환~");

        //AudioManager.instance.PlaySFX("Boss_Pattern2Voice");
    }

    private void Pattern2Summon3()
    {
        Debug.Log("3페이즈 몬스터 소환~");

        //AudioManager.instance.PlaySFX("Boss_Pattern2Voice");
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
        StartCoroutine(Pattern4MoveTo());

        yield return null;
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
            bossTransform.transform.localScale = new Vector3(1.1f, transform.localScale.y, transform.localScale.z);
        }

        else // 타겟이 오른쪽에 있을 때
        {
            bossTransform.transform.localScale = new Vector3(-1.1f, transform.localScale.y, transform.localScale.z);
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
        Gizmos.DrawWireCube(transform.position, attack1Area);
    }
}
