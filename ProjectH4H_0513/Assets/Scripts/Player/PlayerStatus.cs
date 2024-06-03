using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Cinemachine;

public enum PlayerStates { Player_Idle, Player_Run, Player_Attack }


public class PlayerStatus : BaseGameEntity
{
    [Header("플레이어 움직임")]
    [SerializeField] private float playerMove = 10.0f;
    [SerializeField] private float playerJumpForce = 10.0f;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Rigidbody2D playerRigidbody;

    [Header("파츠")]
    [SerializeField] private SkeletonMecanim playerSkeletonSprite;
    [SerializeField] private int playerLAPartHP;
    [SerializeField] private int playerRAPartHP;
    [SerializeField] private int playerLLPartHP;
    [SerializeField] private int playerRLPartHP;

    [Header("스텟")]
    [SerializeField] private int playerATK;
    [SerializeField] private int playerCRIT;

    [Header("카메라")]
    [SerializeField] private Camera playerCamera;

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

    public override void Setup()
    {
        states = new State<PlayerStatus>[3];
        states[(int)PlayerStates.Player_Idle] = new PlayerOwnedStates.Player_Idle();
        states[(int)PlayerStates.Player_Run] = new PlayerOwnedStates.Player_Run();
        states[(int)PlayerStates.Player_Attack] = new PlayerOwnedStates.Player_Attack();

        //상태를 관리하는 StateMachine에 메모리를 할당하고 첫 상태를 설정
        stateMachine = new StateMachine<PlayerStatus>();
        stateMachine.Setup(this, states[(int)PlayerStates.Player_Idle]);
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
        entity.Setup();
    }

    private void Update()
    {
        PlayerStatus entity = gameObject.GetComponent<PlayerStatus>();
        entity.Updated();

        //Debug.Log($"stateMachine: {stateMachine}, PlayerStates: {currentState}");
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
        if(collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            playerSkeletonSprite.skeleton.SetColor(Color.red);
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("onGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            playerSkeletonSprite.skeleton.SetColor(Color.white);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("onGround", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy3Hitbox"))
        {
            playerSkeletonSprite.skeleton.SetColor(Color.red);
        }
        else if (collision.gameObject.CompareTag("Enemy4Hitbox"))
        {
            playerSkeletonSprite.skeleton.SetColor(Color.red);
        }
        else if(collision.gameObject.CompareTag("EncounterBoundary"))
        {
            StartCoroutine(ZoomCamera());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy3Hitbox"))
        {
            playerSkeletonSprite.skeleton.SetColor(Color.white);
        }
        else if (collision.gameObject.CompareTag("Enemy4Hitbox"))
        {
            playerSkeletonSprite.skeleton.SetColor(Color.white);
        }
        else if (collision.gameObject.CompareTag("EncounterBoundary"))
        {
            StartCoroutine(UnzoomCamera());
        }
    }

    private IEnumerator ZoomCamera()
    {
        for (int i = 0; i < 15; i++)
        {
            playerCamera.fieldOfView--;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator UnzoomCamera()
    {
        for (int i = 0; i < 15; i++)
        {
            playerCamera.fieldOfView++;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
