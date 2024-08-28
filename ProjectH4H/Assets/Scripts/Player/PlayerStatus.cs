using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Cinemachine;
using UnityEngine.SceneManagement;

public enum PlayerStates { Player_Idle, Player_Run, Player_Attack }


public class PlayerStatus : BaseGameEntity
{
    [Header("플레이어 움직임")]
    [SerializeField] private float playerMove = 10.0f;
    [SerializeField] private float playerJumpForce = 10.0f;
    [SerializeField] private float h;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private GameObject objMovingEffect;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Rigidbody2D playerRigidbody;

    [Header("파츠")]
    [SerializeField] private SpriteRenderer playerImage;
    [SerializeField] private int playerCurrHP = 600;
    [SerializeField] private int playerMaxHP = 600;


    [Header("스텟")]
    [SerializeField] private int playerATK;
    [SerializeField] private int playerCRIT;

    [Header("카메라")]
    [SerializeField] private Camera playerCamera;

    [Header("데미지 상호작용")]
    [SerializeField] private GameObject objDamageInteractor;
    [SerializeField] private DamageInteractor damageInteractor;

    [Header("스프링 클램핑")]
    [SerializeField] private float maxVelocity;
    [SerializeField] private float dampingFactor;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxJump;

    //플레이어가 가지고 있는 모든 상태, 현재 상태
    private State<PlayerStatus>[] states;
    private StateMachine<PlayerStatus> stateMachine;

    public PlayerStates currentState;
    public PlayerStates CurrentState => currentState;

    public float PlayerMove
    {
        set => playerMove = value;
        get => playerMove;
    }

    public float PlayerJumpForce
    {
        set => playerJumpForce = 10.0f;
        get => playerJumpForce;
    }

    public Animator PlayerAnim
    {
        set => playerAnim = gameObject.GetComponent<Animator>();
        get => playerAnim;
    }

    public Transform PlayerSprite
    {
        set => playerSprite = gameObject.GetComponent<Transform>();
        get => playerSprite;
    }

    public Rigidbody2D PlayerRigidbody
    {
        set => playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        get => playerRigidbody;
    }

    public int PlayerCurrHP
    {
        set
        {
            playerCurrHP = Mathf.Min(value, playerMaxHP);
        }
        get => playerCurrHP;
    } 

    public int PlayerMaxHP
    {
        set => playerMaxHP = 600;
        get => playerMaxHP;
    }

    public override void Setup()
    {
        states = new State<PlayerStatus>[3];
        states[(int)PlayerStates.Player_Idle] = new PlayerOwnedStates.Player_Idle();
        states[(int)PlayerStates.Player_Run] = new PlayerOwnedStates.Player_Run();
        states[(int)PlayerStates.Player_Attack] = new PlayerOwnedStates.Player_Attack();

        //상태를 관리하는 StateMachine에 메모리를 할당하고 첫 상태를 설정
        stateMachine = new StateMachine<PlayerStatus>();
        stateMachine.Setup(this, states[(int)PlayerStates.Player_Idle]);

        damageInteractor = objDamageInteractor.GetComponent<DamageInteractor>();
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(PlayerStates newState)
    {
        currentState = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    private void Awake()
    {
        PlayerStatus entity = gameObject.GetComponent<PlayerStatus>();
        objMovingEffect.SetActive(false);
        entity.Setup();

        maxVelocity = 70f;
        dampingFactor = 0.9f;
        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");

        audioSource = gameObject.GetComponent<AudioSource>();
        damageInteractor.GetComponent<DamageInteractor>();
    }

    private void FixedUpdate()
    {
        PlayerMoving();
    }

    private void Update()
    {
        PlayerStatus entity = gameObject.GetComponent<PlayerStatus>();
        entity.Updated();

        objDamageInteractor = GameObject.FindGameObjectWithTag("CombatController");

        PlayerJump();

        if(PlayerCurrHP <= 0)
        {
            PlayerCurrHP = 0;
        }
      
        playerAnim.SetBool("onGround", !isJumping);

        if (playerAnim.GetBool("isAttack") == true)
        {
            playerAnim.SetBool("isMoving", false);
            playerMove = 0;
        }

        else
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                playerAnim.SetBool("isMoving", true);
            }
            playerMove = 40;
        }

        if (playerAnim.GetBool("isMoving") == false)
        {
            objMovingEffect.SetActive(false);
        }

        else if(playerAnim.GetBool("isMoving") == true && playerAnim.GetBool("onGround") == true)
        {
            objMovingEffect.SetActive(true);
        }

        //Debug.Log($"stateMachine: {stateMachine}, PlayerStates: {currentState}");
    }

    private void PlayerMoving()
    {
        //커맨드 창이 열려있지 않을 때 && 공격하지 않을 때에만 이동
        if (CommandCheckDict.isCommandSystemOpened == false)
        {
            

            h = Input.GetAxis("Horizontal");        // 가로축

            transform.position += new Vector3(h, 0, 0) * playerMove * Time.deltaTime;

            //방향 전환
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerSprite.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                playerSprite.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void PlayerJump()
    {
        //커맨드 창이 열려있지 않을 때에만 이동
        if (CommandCheckDict.isCommandSystemOpened == false)
        {
            //점프
            if (Input.GetKeyDown(KeyCode.Space) && 
                isJumping == false && playerAnim.GetBool("onGround") == true)
            {
                audioSource.clip = sfxJump;
                audioSource.Play();

                isJumping = true;
                playerRigidbody.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                playerAnim.SetTrigger("isJumping");
            }
        }
    }

    public void PlayerMoveInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            playerAnim.SetBool("isRunning", true);
            ChangeState(PlayerStates.Player_Run);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            isJumping = false;
            playerAnim.SetBool("onGround", true);
        }

        else if (collision.gameObject.CompareTag("Object_spinning"))
        {
            damageInteractor.objectDamageType = ObjectDamageType.Object_spinning;
            PlayerisDamaged();
        }

        if(collision.gameObject.CompareTag("Object_squish"))
        {
            /*
            Debug.Log($"팅!@ 현재 속도는 {PlayerRigidbody.velocity}");
            float maxVelocity = 1000f;

            if (PlayerRigidbody.velocity.magnitude >= maxVelocity)
            {
                Debug.Log($"속도가 {maxVelocity}를 초과하여 클램핑합니다.");
                PlayerRigidbody.velocity = PlayerRigidbody.velocity.normalized * maxVelocity;
            }
            else
            {
                Debug.Log($"속도가 {maxVelocity} 미만입니다.");
            }
            */

            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, maxVelocity);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            damageInteractor.enemyDamageType = EnemyDamageType.Enemy2;
            PlayerisDamaged();
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_dontmove") ||
            collision.gameObject.CompareTag("Object_spinning"))
        {
            playerImage.color = Color.white;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            playerAnim.SetBool("onGround", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy3Hitbox"))
        {
            damageInteractor.enemyDamageType = EnemyDamageType.Enemy3;
            PlayerisDamaged();
        }

        else if (collision.gameObject.CompareTag("Object_spinning"))
        {
            damageInteractor.objectDamageType = ObjectDamageType.Object_spinning;
            PlayerisDamaged();
        }

        else if (collision.gameObject.CompareTag("Enemy4Hitbox"))
        {
            damageInteractor.enemyDamageType = EnemyDamageType.Enemy4;
            PlayerisDamaged();
        }

        if (collision.gameObject.CompareTag("Object_hide"))
        {
            damageInteractor.objectDamageType = ObjectDamageType.Object_hide;
            PlayerisDamaged();
        }

        else if (collision.gameObject.CompareTag("Object_falling"))
        {
            damageInteractor.objectDamageType = ObjectDamageType.Object_falling;
            PlayerisDamaged();
        }

        else if (collision.gameObject.CompareTag("Object_beam"))
        {
            damageInteractor.objectDamageType = ObjectDamageType.Object_Beam;
            PlayerisDamaged();
        }

        else if (collision.gameObject.CompareTag("Object_thorn"))
        {
            damageInteractor.objectDamageType = ObjectDamageType.Object_thorn;
            PlayerisDamaged();
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy3Hitbox") ||
            collision.gameObject.CompareTag("Enemy4Hitbox") ||
            collision.gameObject.CompareTag("Object_hide") ||
            collision.gameObject.CompareTag("Object_falling") ||
            collision.gameObject.CompareTag("Object_beam") ||
            collision.gameObject.CompareTag("Object_thorn") ||
            collision.gameObject.CompareTag("Object_spinning"))
        {
            playerImage.color = Color.white;
        }

        else if (collision.gameObject.CompareTag("EncounterBoundary"))
        {
            //StartCoroutine(UnzoomCamera());
        }
    }

    private void PlayerisDamaged()
    {
        playerAnim.SetTrigger("isDamaged");
        playerImage.color = Color.red;

        objDamageInteractor.GetComponent<DamageInteractor>();
        PlayerCurrHP -= objDamageInteractor.GetComponent<DamageInteractor>().GetDamageFromObj();

        //Debug.Log($"플레이어는 {objDamageInteractor.GetComponent<DamageInteractor>().GetDamageFromObj()}만큼의 데미지를 입었다!");
    }
}
