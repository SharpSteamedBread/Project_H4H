using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private int enemyCurrHP;
    [SerializeField] private int enemyMaxHP;
    [SerializeField] private int enemyATK;
    [SerializeField] private float attackCooltime = 1.0f;

    [Header("사망")]
    [SerializeField] private GameObject enemyDeadItem;
    [SerializeField] private GameObject enemyBoomHitbox;

    [Header("테스트 겸 피격")]
    [SerializeField] private GameObject criticalShow;
    [SerializeField] private CommandEnterUI activateCommandSkill;
    public UnityEvent onEnemyDamaged;
    public GameObject objDamageInteractor;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip soundHitted;



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
        set => enemyMaxHP = 80;
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
        states = new State<EnemyThirdStatus>[5];
        states[(int)EnemyThirdStates.Enemy_Idle] = new MonsterBoomOwnedStates.Enemy_Idle();
        states[(int)EnemyThirdStates.Enemy_Move] = new MonsterBoomOwnedStates.Enemy_Move();
        states[(int)EnemyThirdStates.Enemy_Attack] = new MonsterBoomOwnedStates.Enemy_Attack();
        states[(int)EnemyThirdStates.Enemy_Damaged] = new MonsterBoomOwnedStates.Enemy_Damaged();
        states[(int)EnemyThirdStates.Enemy_Die] = new MonsterBoomOwnedStates.Enemy_Die();

        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = target.GetComponent<Transform>();

        //상태를 관리하는 StateMachine에 메모리를 할당하고 첫 상태를 설정
        stateMachine = new StateMachine<EnemyThirdStatus>();
        stateMachine.Setup(this, states[(int)EnemyThirdStates.Enemy_Idle]);

        //데미지 관련
        objDamageInteractor.GetComponent<DamageInteractor>();
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
        enemyMove = 20.0f;
        enemyCurrHP = 80;
        enemyMaxHP = 80;

        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");
        activateCommandSkill = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CommandEnterUI>();

        StartCoroutine(RandomWay());

        EnemyThirdStatus entity = gameObject.GetComponent<EnemyThirdStatus>();
        audioSource = gameObject.GetComponent<AudioSource>();

        entity.Setup();
    }

    private void Update()
    {
        EnemyThirdStatus entity = gameObject.GetComponent<EnemyThirdStatus>();
        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");
        activateCommandSkill = GameObject.FindGameObjectWithTag("CombatController").GetComponent<CommandEnterUI>();

        entity.Updated();

        //Debug.Log($"stateMachine: {stateMachine}, enemyStates: {currentState}, move: {enemyMove}");

        if (enemyCurrHP <= 0)
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
        Instantiate(enemyBoomHitbox, EnemyTransform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            int percent = Random.Range(0, 101);

            objDamageInteractor.GetComponent<DamageInteractor>();

            if (percent > 60)
            {
                audioSource.clip = soundHitted;
                audioSource.PlayOneShot(soundHitted);
            }

            enemyAnim.SetTrigger("isDamaged");
            enemyCurrHP -= objDamageInteractor.GetComponent<DamageInteractor>().CalculateDamage();

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
            criticalShow.SetActive(true);

            activateCommandSkill.TimeToSkillCommand();
        }
    }
}
