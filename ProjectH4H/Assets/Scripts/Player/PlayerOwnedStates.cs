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
                //Ŀ�ǵ� â�� �� ������ ������ �̵�
                if (CommandCheck.isCommandSystemOpened == false)
                {
                    entity.PlayerAnim.SetBool("isMoving", true);
                    entity.ChangeState(PlayerStates.Player_Run);
                }
            }
        }

        public override void Exit(PlayerStatus entity)
        {
            Debug.Log("�⵿!");

        }
    }

    public class Player_Run : State<PlayerStatus>
    {
        private float h, v;

        public override void Enter(PlayerStatus entity)
        {
            Debug.Log("�ȴ´�!");
        }

        public override void Execute(PlayerStatus entity)
        {
            //Ŀ�ǵ� â�� �������� ���� ������ �̵�
            if(CommandCheck.isCommandSystemOpened == false)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
                    entity.PlayerAnim.SetBool("isMoving", true);
                }

                h = Input.GetAxis("Horizontal");        // ������
                v = Input.GetAxis("Vertical");          // ������

                entity.transform.position += new Vector3(h, 0, v) * entity.PlayerMove * Time.deltaTime;

                //���� ��ȯ
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    entity.PlayerSprite.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    entity.PlayerSprite.transform.localScale = new Vector3(1, 1, 1);
                }
            }

            //���� �� & Ŀ�ǵ� â�� �������� ������ �ൿ�� �ʱ�ȭ
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)
                || CommandCheck.isCommandSystemOpened == true)
            {
                entity.ChangeState(PlayerStates.Player_Idle);
            }
        }

        public override void Exit(PlayerStatus entity)
        {
            Debug.Log("����!");
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
