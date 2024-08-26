using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyStates { EnemyNormal_Idle, EnemyNormal_Move, EnemyNormal_Attack, EnemyNormal_Damaged, EnemyNormal_Die }

public class EnemyStatus : BaseGameEntity
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Transform enemyTransform;

    [Header("���� ����")]
    [SerializeField] private Vector2 attackArea;
    [SerializeField] private Vector2 chaseArea;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform targetPos;

    [Header("���� ������")]
    [SerializeField] private float enemyMove = 5.0f;
    [SerializeField] private int nextMove;              // �̵� ���� ����. -1: ���� 0: ��� 1: ������
    [SerializeField] private float randomSecond;


    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Transform enemySprite;

    [Header("����")]
    [SerializeField] private int enemyCurrHP;
    [SerializeField] private int enemyMaxHP;
    [SerializeField] private int enemyATK;
    [SerializeField] private float attackCooltime = 1.0f;

    [Header("���")]
    [SerializeField] private GameObject enemyDeadItem;

    [Header("�׽�Ʈ �� �ǰ�")]
    [SerializeField] private GameObject criticalShow;
    [SerializeField] private CommandEnterUI activateCommandSkill;
    public UnityEvent onEnemyDamaged;
    public GameObject objDamageInteractor;


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

    public int EnemyCurrHP
    {
        set
        {
            enemyCurrHP = Mathf.Min(value, enemyMaxHP);
        }
        get => enemyCurrHP;
    }

    public int EnemyMaxHP
    {
        set => enemyMaxHP = value;
        get => enemyMaxHP; 
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

        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = target.GetComponent<Transform>();

        //���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ� ù ���¸� ����
        stateMachine = new StateMachine<EnemyStatus>();
        stateMachine.Setup(this, states[(int)EnemyStates.EnemyNormal_Idle]);

        //������ ����
        objDamageInteractor.GetComponent<DamageInteractor>();
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
        enemyCurrHP = 150;
        enemyMaxHP = 150;

        StartCoroutine(RandomWay());

        EnemyStatus entity = gameObject.GetComponent<EnemyStatus>();

        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");
        activateCommandSkill = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CommandEnterUI>();

        entity.Setup();
    }

    private void Update()
    {
        EnemyStatus entity = gameObject.GetComponent<EnemyStatus>();
        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");
        activateCommandSkill = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CommandEnterUI>();

        entity.Updated();

        //Debug.Log($"stateMachine: {stateMachine}, enemyStates: {currentState}, move: {enemyMove}");

        if(enemyCurrHP <= 0)
        {
            ChangeState(EnemyStates.EnemyNormal_Die);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackArea);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, chaseArea);

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
        if(collision.CompareTag("PlayerHitbox"))
        {
            objDamageInteractor.GetComponent<DamageInteractor>();

            Debug.Log("�ƾ�!");
            enemyAnim.SetTrigger("isDamaged");
            enemyCurrHP -= objDamageInteractor.GetComponent<DamageInteractor>().CalculateDamage();

            //onEnemyDamaged.Invoke();
            CritRate();
        }
    }

    private void CritRate()
    {
        int crit = Random.Range(1, 101);
        //Debug.Log($"ũ��Ƽ�� ��ġ: {crit}");

        if(crit >= 80)
        {
            criticalShow.SetActive(true);

            activateCommandSkill.TimeToSkillCommand();
        }
    }
}
