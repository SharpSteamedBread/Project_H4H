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
    [SerializeField] private float h;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Rigidbody2D playerRigidbody;

    [Header("파츠")]
    [SerializeField] private SpriteRenderer playerImage;
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

    private void FixedUpdate()
    {
        PlayerMoving();
    }

    private void Update()
    {
        PlayerStatus entity = gameObject.GetComponent<PlayerStatus>();
        entity.Updated();

        
        PlayerJump();

        if (playerAnim.GetBool("onGround") == true)
        {
            Debug.Log("땅 착지!");
        }


        //Debug.Log($"stateMachine: {stateMachine}, PlayerStates: {currentState}");
    }

    private void PlayerMoving()
    {
        //커맨드 창이 열려있지 않을 때에만 이동
        if (CommandCheckDict.isCommandSystemOpened == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                playerAnim.SetBool("isMoving", true);
            }

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
            playerAnim.GetBool("onGround") == true)
            {
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
        if(collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            playerImage.color = Color.red;
        }

        if(collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            playerAnim.SetBool("onGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_dontmove"))
        {
            playerImage.color = Color.white;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("onGround", false);
            playerAnim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy3Hitbox") ||
            collision.gameObject.CompareTag("Enemy4Hitbox") ||
            collision.gameObject.CompareTag("Object_hide") ||
            collision.gameObject.CompareTag("Object_falling") ||
            collision.gameObject.CompareTag("Object_spinning"))
        {
            playerImage.color = Color.red;
        }

        else if(collision.gameObject.CompareTag("EncounterBoundary"))
        {
            //StartCoroutine(ZoomCamera());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy3Hitbox") ||
            collision.gameObject.CompareTag("Enemy4Hitbox") ||
            collision.gameObject.CompareTag("Object_hide") ||
            collision.gameObject.CompareTag("Object_falling") ||
            collision.gameObject.CompareTag("Object_spinning"))
        {
            playerImage.color = Color.white;
        }

        else if (collision.gameObject.CompareTag("EncounterBoundary"))
        {
            //StartCoroutine(UnzoomCamera());
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
