using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { EnemyNormal_Idle, EnemyNormal_Move, EnemyNormal_Attack, EnemyNormal_Damaged, EnemyNormal_Die }

public class EnemyStatus : BaseGameEntity
{
    private Rigidbody2D rigid;
    private Transform enemyTransform;

    [Header("���� ����")]
    [SerializeField] private Vector2 attackArea;
    [SerializeField] private Vector2 chaseArea;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform targetPos;

    [Header("���� ������")]
    [SerializeField] private float enemyMove;
    [SerializeField] private int nextMove;              // �̵� ���� ����. -1: ���� 0: ��� 1: ������
    [SerializeField] private float randomSecond;


    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Transform enemySprite;

    [Header("����")]
    [SerializeField] private int enemyATK;
    [SerializeField] private float attackCooltime = 1.0f;


    //���Ͱ� ������ �ִ� ��� ����, ���� ����
    private State<EnemyStatus>[] states;
    private StateMachine<EnemyStatus> stateMachine;

    private EnemyStates currentAction;

    public float EnemyMove
    {
        set => enemyMove = 5.0f;
        get => enemyMove;
    }

    public Animator EnemyAnim
    {
        set => enemyAnim = gameObject.GetComponent<Animator>();
        get => enemyAnim;
    }

    public Transform EnemySprite
    {
        set => enemySprite = gameObject.GetComponent<Transform>();
        get => enemySprite;
    }

    public EnemyStates CurrentState
    {
        set => currentAction = value;
        get => currentAction;
    }

    public Vector2 AttackArea
    {
        set => attackArea = value;
        get => attackArea;
    }

    public Vector2 ChaseArea
    {
        set => chaseArea = value;
        get => chaseArea;
    }

    public Transform TargetPos
    {
        set => targetPos = target.GetComponent<Transform>();
        get => targetPos;
    }

    public float AttackCooltime
    {
        set => attackCooltime = 3.0f;
        get => attackCooltime;
    }

    public Rigidbody2D Rigid
    {
        set => rigid = GetComponent<Rigidbody2D>();
        get => rigid;
    }

    public Transform EnemyTransform
    {
        set => enemyTransform = GetComponent<Transform>();
        get => enemyTransform;
    }

    public int NextMove
    {
        set => nextMove = Random.Range(-1, 2);
        get => nextMove;
    }

    public float RandomSecond
    {
        set => randomSecond = Random.Range(0f, 3f);
        get => randomSecond;
    }

    public override void Setup()
    {
        states = new State<EnemyStatus>[5];
        states[(int)EnemyStates.EnemyNormal_Idle] = new MonsterNormalOwnedStates.EnemyNormal_Idle();
        states[(int)EnemyStates.EnemyNormal_Move] = new MonsterNormalOwnedStates.EnemyNormal_Move();
        states[(int)EnemyStates.EnemyNormal_Attack] = new MonsterNormalOwnedStates.EnemyNormal_Attack();
        states[(int)EnemyStates.EnemyNormal_Damaged] = new MonsterNormalOwnedStates.EnemyNormal_Damaged();
        states[(int)EnemyStates.EnemyNormal_Die] = new MonsterNormalOwnedStates.EnemyNormal_Die();


        //���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ� ù ���¸� ����
        stateMachine = new StateMachine<EnemyStatus>();
        stateMachine.Setup(this, states[(int)EnemyStates.EnemyNormal_Idle]);

        enemyMove = 10.0f;
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(EnemyStates newState)
    {
        stateMachine.ChangeState(states[(int)newState]);
    }

    private void Awake()
    {
        EnemyStatus entity = gameObject.GetComponent<EnemyStatus>();
        entity.Setup();
    }

    private void Update()
    {
        EnemyStatus entity = gameObject.GetComponent<EnemyStatus>();
        entity.Updated();

        Debug.Log($"stateMachine: {stateMachine}, enemyStates: {currentAction}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackArea);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, chaseArea);

    }

    public IEnumerator DetectPlayer()
    {
        float distance = Vector2.Distance(targetPos.position, transform.position);

        //Debug.Log($"�÷��̾�-���� �Ÿ�: {distance}, ���� ����: {detectArea.x / 2}");

        if (distance <= attackArea.x / 2)
        {
            //Debug.Log("������!");

            //Debug.Log($"{attackCooltime}�ʰ� ��ٷ�!");
            yield return new WaitForSeconds(attackCooltime);
            //Debug.Log($"����!");
            ChangeState(EnemyStates.EnemyNormal_Attack);
        }

        else if (distance <= chaseArea.x / 2)
        {
            ChangeState(EnemyStates.EnemyNormal_Move);
        }

        else
        {
            ChangeState(EnemyStates.EnemyNormal_Idle);
        }

        yield return null;

        StartCoroutine(DetectPlayer());
    }

    public void Move()
    {
        rigid.velocity = new Vector2(nextMove * enemyMove, rigid.velocity.y);

        switch (nextMove)
        {
            case -1:
                enemyTransform.transform.localScale = new Vector3(transform.localScale.x * 1f, transform.localScale.y, transform.localScale.z);
                enemyAnim.SetBool("isMoving", true);
                break;

            case 0:
                enemyTransform.transform.localScale = new Vector3(transform.localScale.x * 1f, transform.localScale.y, transform.localScale.z);
                enemyAnim.SetBool("isMoving", false);
                break;

            case 1:
                enemyTransform.transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                enemyAnim.SetBool("isMoving", true);
                break;
        }
    }
}
