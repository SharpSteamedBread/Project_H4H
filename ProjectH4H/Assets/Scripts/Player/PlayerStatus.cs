using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { Player_Idle, Player_Run, Player_Attack }


public class PlayerStatus : BaseGameEntity
{
    [Header("플레이어 움직임")]
    [SerializeField] private float playerMove;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform playerSprite;

    [Header("파츠")]
    [SerializeField] private int playerLAPartHP;
    [SerializeField] private int playerRAPartHP;
    [SerializeField] private int playerLLPartHP;
    [SerializeField] private int playerRLPartHP;

    [Header("스텟")]
    [SerializeField] private int playerATK;
    [SerializeField] private int playerCRIT;

    //플레이어가 가지고 있는 모든 상태, 현재 상태
    private State<PlayerStatus>[] states;
    private StateMachine<PlayerStatus> stateMachine;

    private PlayerStates currentAction;

    public float PlayerMove
    {
        set => playerMove = 10.0f;
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

    public PlayerStates CurrentState
    {
        set => currentAction = value;
        get => currentAction;
    }

    public override void Setup()
    {
        states = new State<PlayerStatus>[3];
        states[(int)PlayerStates.Player_Idle] = new PlayerOwnedStates.Player_Idle();
        states[(int)PlayerStates.Player_Run] = new PlayerOwnedStates.Player_Run();
        states[(int)PlayerStates.Player_Attack] = new PlayerOwnedStates.Player_Attack();


        //상태를 관리하는 StateMachine에 메모리를 할당하고 첫 상태를 설정
        stateMachine = new StateMachine<PlayerStatus>();
        stateMachine.Setup(this, states[(int)PlayerStates.Player_Run]);

        playerMove = 10.0f;
    }

    public override void Updated()
    {
        stateMachine.Execute();
    }

    public void ChangeState(PlayerStates newState)
    {
        stateMachine.ChangeState(states[(int)newState]);
    }

    private void Update()
    {
        Debug.Log(CurrentState);
    }
}
