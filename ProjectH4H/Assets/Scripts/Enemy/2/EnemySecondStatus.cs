using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemySecondStates { Enemy_Idle, Enemy_Attack, Enemy_Damaged, Enemy_Die }

public class EnemySecondStatus : BaseGameEntity
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
    public GameObject objDamageInteractor;



    //���Ͱ� ������ �ִ� ��� ����, ���� ����
    private State<EnemySecondStatus>[] states;
    private StateMachine<EnemySecondStatus> stateMachine;

    public EnemySecondStates currentState;
    public EnemySecondStates CurrentState => currentState;

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
        set
        {
            enemyHP = Mathf.Min(value, 210);
        }
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
        states = new State<EnemySecondStatus>[4];
        states[(int)EnemySecondStates.Enemy_Idle] = new MonsterDontMoveOwnedStates.Enemy_Idle();
        states[(int)EnemySecondStates.Enemy_Attack] = new MonsterDontMoveOwnedStates.Enemy_Attack();
        states[(int)EnemySecondStates.Enemy_Damaged] = new MonsterDontMoveOwnedStates.Enemy_Damaged();
        states[(int)EnemySecondStates.Enemy_Die] = new MonsterDontMoveOwnedStates.Enemy_Die();


        //���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ� ù ���¸� ����
        stateMachine = new StateMachine<EnemySecondStatus>();
        stateMachine.Setup(this, states[(int)EnemySecondStates.Enemy_Idle]);

        //������ ����
        objDamageInteractor.GetComponent<DamageInteractor>();
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(EnemySecondStates newState)
    {
        currentState = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    private void Awake()
    {
        enemyMove = 5.0f;
        enemyHP = 100;

        StartCoroutine(RandomWay());

        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = target.GetComponent<Transform>();

        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");

        EnemySecondStatus entity = gameObject.GetComponent<EnemySecondStatus>();
        entity.Setup();
    }

    private void Update()
    {
        EnemySecondStatus entity = gameObject.GetComponent<EnemySecondStatus>();
        entity.Updated();

        //Debug.Log($"stateMachine: {stateMachine}, enemyStates: {currentState}, move: {enemyMove}");

        if(enemyHP <= 0)
        {
            ChangeState(EnemySecondStates.Enemy_Die);
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
        Destroy(transform.parent.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            objDamageInteractor.GetComponent<DamageInteractor>();

            Debug.Log("�ƾ�!");
            enemyAnim.SetTrigger("isDamaged");
            enemyHP -= objDamageInteractor.GetComponent<DamageInteractor>().CalculateDamage();

            //onEnemyDamaged.Invoke();
        }
    }
}
