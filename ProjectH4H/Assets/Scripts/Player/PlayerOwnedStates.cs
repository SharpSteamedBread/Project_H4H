using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerOwnedStates
{
    public class Player_Idle : State<PlayerStatus>
    {
        public override void Enter(PlayerStatus entity)
        {
            entity.PlayerAnim.SetBool("isMoving", false);
        }
        public override void Execute(PlayerStatus entity)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                //커맨드 창이 안 켜졌을 때에만 이동
                if (CommandCheckDict.isCommandSystemOpened == false)
                {
                    entity.PlayerAnim.SetBool("isMoving", true);
                    entity.ChangeState(PlayerStates.Player_Run);
                }
            }

            //점프
            if (Input.GetKeyDown(KeyCode.Space))
            {
                entity.PlayerRigidbody.AddForce(Vector2.up * entity.PlayerJumpForce, ForceMode2D.Impulse);
                entity.PlayerAnim.SetTrigger("isJumping");
            }
        }

        public override void Exit(PlayerStatus entity)
        {
            Debug.Log("출동!");

        }
    }

    public class Player_Run : State<PlayerStatus>
    {
        private float h;

        public override void Enter(PlayerStatus entity)
        {
            Debug.Log("걷는다!");
        }

        public override void Execute(PlayerStatus entity)
        {
            //조작 끝 & 커맨드 창이 열려있을 때에는 행동을 초기화
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)
                || CommandCheckDict.isCommandSystemOpened == true)
            {
                entity.ChangeState(PlayerStates.Player_Idle);
            }
        }

        public override void Exit(PlayerStatus entity)
        {
            Debug.Log("멈춰!");
        }
    }

    public class Player_Attack : State<PlayerStatus>
    {
        public override void Enter(PlayerStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(PlayerStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(PlayerStatus entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
