using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { Player_Idle, Player_Run, Player_Attack }


public class PlayerStatus : BaseGameEntity
{
    [Header("�÷��̾� ������")]
    [SerializeField] private float playerMove = 10.0f;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform playerSprite;

    [Header("����")]
    [SerializeField] private int playerLAPartHP;
    [SerializeField] private int playerRAPartHP;
    [SerializeField] private int playerLLPartHP;
    [SerializeField] private int playerRLPartHP;

    [Header("����")]
    [SerializeField] private int playerATK;
    [SerializeField] private int playerCRIT;

    //�÷��̾ ������ �ִ� ��� ����, ���� ����
    private State<PlayerStatus>[] states;
    private StateMachine<PlayerStatus> stateMachine;

    public PlayerStates currentState;
    public PlayerStates CurrentState => currentState;

    public float PlayerMove
    {
        set => playerMove = value;
        get => playerMove;
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

    public override void Setup()
    {
        states = new State<PlayerStatus>[3];
        states[(int)PlayerStates.Player_Idle] = new PlayerOwnedStates.Player_Idle();
        states[(int)PlayerStates.Player_Run] = new PlayerOwnedStates.Player_Run();
        states[(int)PlayerStates.Player_Attack] = new PlayerOwnedStates.Player_Attack();

        //���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ��ϰ� ù ���¸� ����
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

        Debug.Log($"stateMachine: {stateMachine}, PlayerStates: {currentState}");
    }

    public void PlayerMoveInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            playerAnim.SetBool("isRunning", true);
            ChangeState(PlayerStates.Player_Run);
        }
    }
}
