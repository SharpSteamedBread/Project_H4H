using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { EnemyNormal_Idle, EnemyNormal_Move, EnemyNormal_Attack, EnemyNormal_Damaged, EnemyNormal_Die }

public class EnemyStatus : BaseGameEntity
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Transform enemyTransform;

    [Header("���� ����")]
    [SerializeField] private Vector2 attackArea;
    [SerializeField] private Vector2 encounterArea;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform targetPos;

    [Header("���� ������")]
    [SerializeField] private float enemyMove = 5.0f;
    [SerializeField] private int nextMove;              // �̵� ���� ����. -1: ���� 0: ��� 1: ������
    [SerializeField] private float randomSecond;


    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Transform enemySprite;

    [Header("����")]
    [SerializeField] private int enemyHP;
    [SerializeField] private int enemyATK;
    [SerializeField] private float attackCooltime = 1.0f;

    [Header("���")]
    [SerializeField] private GameObject enemyDeadItem;


    //���Ͱ� ������ �ִ� ��� ����, ���� ����
    private State<EnemyStatus>[] states;
    private StateMachine<EnemyStatus> stateMachine;

    public EnemyStates currentState;
    public EnemyStates CurrentState => currentState;

    public float EnemyMove
    {
        set => enemyMove = value;
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

    public Vector2 AttackArea
    {
        set => attackArea = value;
        get => attackArea;
    }

    public Vector2 EncounterArea
    {
        set => EncounterArea = value;
        get => encounterArea;
    }

    public Transform TargetPos
    {
        set => targetPos = target.GetComponent<Transform>();
        get => targetPos;
    }

    public int EnemyHP
    {
        set => enemyHP = value;
        get => enemyHP;
    }

    public float AttackCooltime
    {
        set => attackCooltime = value;
        get => attackCooltime;
    }

    public Rigidbody2D Rigid
    {
        set => rigid = gameObject.GetComponent<Rigidbody2D>();
        get => rigid;
    }

    public Transform EnemyTransform
    {
        set => enemyTransform = gameObject.GetComponent<Transform>();
        get => enemyTransform;
    }

    public int NextMove
    {
        set => nextMove = value;
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
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(EnemyStates newState)
    {
        currentState = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    private void Awake()
    {
        enemyMove = 5.0f;
        enemyHP = 100;

        StartCoroutine(RandomWay());

        EnemyStatus entity = gameObject.GetComponent<EnemyStatus>();
        entity.Setup();
    }

    private void Update()
    {
        EnemyStatus entity = gameObject.GetComponent<EnemyStatus>();
        entity.Updated();

        Debug.Log($"stateMachine: {stateMachine}, enemyStates: {currentState}, move: {enemyMove}");

        if(enemyHP <= 0)
        {
            ChangeState(EnemyStates.EnemyNormal_Die);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackArea);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, encounterArea);

    }

    private IEnumerator RandomWay()
    {
        nextMove = Random.Range(-1, 2);
        randomSecond = Random.Range(1.0f, 3.0f);

        yield return new WaitForSeconds(randomSecond);

        StartCoroutine(RandomWay());
    }

    public void EnemyDie()
    {
        Instantiate(enemyDeadItem, EnemyTransform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
