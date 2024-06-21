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
                if (CommandCheckDict.isCommandSystemOpened == false)
                {
                    entity.PlayerAnim.SetBool("isMoving", true);
                    entity.ChangeState(PlayerStates.Player_Run);
                }
            }

            //����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                entity.PlayerRigidbody.AddForce(Vector2.up * entity.PlayerJumpForce, ForceMode2D.Impulse);
                entity.PlayerAnim.SetTrigger("isJumping");
            }
        }

        public override void Exit(PlayerStatus entity)
        {
            Debug.Log("�⵿!");

        }
    }

    public class Player_Run : State<PlayerStatus>
    {
        private float h;

        public override void Enter(PlayerStatus entity)
        {
            Debug.Log("�ȴ´�!");
        }

        public override void Execute(PlayerStatus entity)
        {
            //Ŀ�ǵ� â�� �������� ���� ������ �̵�
            if(CommandCheckDict.isCommandSystemOpened == false)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
                    entity.PlayerAnim.SetBool("isMoving", true);
                }

                //����
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    entity.PlayerRigidbody.AddForce(Vector2.up * entity.PlayerJumpForce, ForceMode2D.Impulse);
                    entity.PlayerAnim.SetTrigger("isJumping");
                }

                h = Input.GetAxis("Horizontal");        // ������

                entity.transform.position += new Vector3(h, 0, 0) * entity.PlayerMove * Time.smoothDeltaTime;

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
                || CommandCheckDict.isCommandSystemOpened == true)
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
