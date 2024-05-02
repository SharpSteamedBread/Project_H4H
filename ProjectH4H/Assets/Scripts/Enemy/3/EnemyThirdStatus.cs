using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyThirdStates { Enemy_Idle, Enemy_Move, Enemy_Attack, Enemy_Damaged, Enemy_Die }

public class EnemyThirdStatus : BaseGameEntity
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Transform enemyTransform;

    [Header("감지 범위")]
    [SerializeField] private Vector2 attackArea;
    [SerializeField] private Vector2 encounterArea;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform targetPos;

    [Header("몬스터 움직임")]
    [SerializeField] private float enemyMove = 5.0f;
    [SerializeField] private int nextMove;              // 이동 제어 변수. -1: 왼쪽 0: 대기 1: 오른쪽
    [SerializeField] private float randomSecond;


    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Transform enemySprite;

    [Header("스텟")]
    [SerializeField] private int enemyHP;
    [SerializeField] private int enemyATK;
    [SerializeField] private float attackCooltime = 1.0f;

    [Header("사망")]
    [SerializeField] private GameObject enemyDeadItem;


    //몬스터가 가지고 있는 모든 상태, 현재 상태
    private State<EnemyThirdStatus>[] states;
    private StateMachine<EnemyThirdStatus> stateMachine;

    public EnemyThirdStates currentState;
    public EnemyThirdStates CurrentState => currentState;

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
        states = new State<EnemyThirdStatus>[5];
        states[(int)EnemyThirdStates.Enemy_Idle] = new MonsterBoomOwnedStates.Enemy_Idle();
        states[(int)EnemyThirdStates.Enemy_Move] = new MonsterBoomOwnedStates.Enemy_Move();
        states[(int)EnemyThirdStates.Enemy_Attack] = new MonsterBoomOwnedStates.Enemy_Attack();
        states[(int)EnemyThirdStates.Enemy_Damaged] = new MonsterBoomOwnedStates.Enemy_Damaged();
        states[(int)EnemyThirdStates.Enemy_Die] = new MonsterBoomOwnedStates.Enemy_Die();


        //상태를 관리하는 StateMachine에 메모리를 할당하고 첫 상태를 설정
        stateMachine = new StateMachine<EnemyThirdStatus>();
        stateMachine.Setup(this, states[(int)EnemyThirdStates.Enemy_Idle]);
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(EnemyThirdStates newState)
    {
        currentState = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    private void Awake()
    {
        enemyMove = 5.0f;
        enemyHP = 100;

        StartCoroutine(RandomWay());

        EnemyThirdStatus entity = gameObject.GetComponent<EnemyThirdStatus>();
        entity.Setup();
    }

    private void Update()
    {
        EnemyThirdStatus entity = gameObject.GetComponent<EnemyThirdStatus>();
        entity.Updated();

        Debug.Log($"stateMachine: {stateMachine}, enemyStates: {currentState}, move: {enemyMove}");

        if(enemyHP <= 0)
        {
            ChangeState(EnemyThirdStates.Enemy_Die);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            enemyHP = 0;
        }
    }
}
