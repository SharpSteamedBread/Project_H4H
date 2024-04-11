using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerOwnedStates
{
    public class Player_Idle : State<PlayerStatus>
    {
        public override void Enter(PlayerStatus entity)
        {
            entity.PrintText("����մϴ�!");

        }
        public override void Execute(PlayerStatus entity)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                entity.PlayerAnim.SetBool("isRunning", true);
                entity.ChangeState(PlayerStates.Player_Run);
            }

            entity.PrintText("�����~");
        }

        public override void Exit(PlayerStatus entity)
        {
            entity.PrintText("�⵿!");

        }
    }

    public class Player_Run : State<PlayerStatus>
    {
        private float h, v;

        public override void Enter(PlayerStatus entity)
        {
            entity.PrintText("�ȴ´�!");

        }

        public override void Execute(PlayerStatus entity)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                entity.PlayerAnim.SetBool("isRunning", true);
            }

            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                entity.PlayerAnim.SetBool("isRunning", false);
                entity.ChangeState(PlayerStates.Player_Idle);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                entity.PlayerSprite.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                entity.PlayerSprite.transform.localScale = new Vector3(1, 1, 1);
            }

            h = Input.GetAxis("Horizontal");        // ������
            v = Input.GetAxis("Vertical");          // ������

            entity.transform.position += new Vector3(h, 0, v) * entity.PlayerMove * Time.deltaTime;
        }

        public override void Exit(PlayerStatus entity)
        {
            
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
