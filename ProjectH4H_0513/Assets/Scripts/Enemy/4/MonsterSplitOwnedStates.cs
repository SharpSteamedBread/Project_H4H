using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterSplitOwnedStates
{
    public class Enemy_Idle : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            Debug.Log("���� ��� ����");
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            Debug.Log("���� �����");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            //Debug.Log($"�÷��̾�-���� �Ÿ�: {distance}, ���� ����: {detectArea.x / 2}");

            //���� ���� ����
            if (distance <= entity.AttackArea.x / 2)
            {
                //Debug.Log("������!");

                //Debug.Log($"{attackCooltime}�ʰ� ��ٷ�!");
                //yield return new WaitForSeconds(entity.AttackCooltime);
                //Debug.Log($"����!");
                entity.ChangeState(EnemyFourthStates.Enemy_Attack);
            }

            //�߰� ���� ����
            else if(distance <= entity.EncounterArea.x / 2)
            {
                entity.ChangeState(EnemyFourthStates.Enemy_Move);
            }

            //�̵�
            entity.Rigid.velocity = new Vector2(entity.NextMove * entity.EnemyMove, entity.Rigid.velocity.y);

            switch (entity.NextMove)
            {
                case -1:
                    entity.EnemyTransform.localScale = new Vector3(-1f, 1f, 1f);
                    entity.EnemyAnim.SetBool("isMoving", true);
                    break;

                case 0:
                    entity.EnemyTransform.localScale = new Vector3(1f, 1f, 1f);
                    entity.EnemyAnim.SetBool("isMoving", false);
                    break;

                case 1:
                    entity.EnemyTransform.localScale = new Vector3(1f, 1f, 1f);
                    entity.EnemyAnim.SetBool("isMoving", true);
                    break;
            }

        }

        public override void Exit(EnemyFourthStatus entity)
        {
            Debug.Log("���� �õ��Ŵ���...");
        }
    }


    public class Enemy_Move : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            Debug.Log("���Ͱ� ����");
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            Debug.Log("���� �߰���");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            if (distance >= entity.EncounterArea.x / 2)
            {
                entity.ChangeState(EnemyFourthStates.Enemy_Idle);
            }
        }

        public override void Exit(EnemyFourthStatus entity)
        {
            Debug.Log("������ ����");
        }
    }

    public class Enemy_Attack : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            Debug.Log("���� ���� �غ�");
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            Debug.Log("���� ����!");

        }

        public override void Exit(EnemyFourthStatus entity)
        {
            Debug.Log("���� ���� ����.");
        }
    }

    public class Enemy_Damaged : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(EnemyFourthStatus entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Enemy_Die : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            entity.EnemyAnim.SetBool("isDead", true);
            entity.EnemyDie();
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(EnemyFourthStatus entity)
        {
            throw new System.NotImplementedException();
        }

    }
}
