using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterNormalOwnedStates
{
    public class EnemyNormal_Idle : State<EnemyStatus>
    {
        public override void Enter(EnemyStatus entity)
        {
            Debug.Log("���� ��� ����");
        }

        public override void Execute(EnemyStatus entity)
        {
            Debug.Log("���� �����");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            //Debug.Log($"�÷��̾�-���� �Ÿ�: {distance}, ���� ����: {detectArea.x / 2}");

            if (distance <= entity.AttackArea.x / 2)
            {
                //Debug.Log("������!");

                //Debug.Log($"{attackCooltime}�ʰ� ��ٷ�!");
                //yield return new WaitForSeconds(entity.AttackCooltime);
                //Debug.Log($"����!");
                entity.ChangeState(EnemyStates.EnemyNormal_Attack);
            }

            else if(distance <= entity.ChaseArea.x / 2)
            {
                entity.ChangeState(EnemyStates.EnemyNormal_Move);
            }
        }

        public override void Exit(EnemyStatus entity)
        {
            Debug.Log("���� �õ��Ŵ���...");
        }
    }


    public class EnemyNormal_Move : State<EnemyStatus>
    {
        public override void Enter(EnemyStatus entity)
        {
            Debug.Log("���Ͱ� ����");
        }

        public override void Execute(EnemyStatus entity)
        {
            Debug.Log("���� �߰���");

            entity.Rigid.velocity = new Vector2(entity.NextMove * entity.EnemyMove, entity.Rigid.velocity.y);

            Debug.Log(entity.NextMove);

            switch (entity.NextMove)
            {
                case -1:
                    entity.EnemyTransform.transform.localScale = new Vector3(entity.transform.localScale.x * 1f, entity.transform.localScale.y, entity.transform.localScale.z);
                    entity.EnemyAnim.SetBool("isMoving", true);
                    break;

                case 0:
                    entity.EnemyTransform.transform.localScale = new Vector3(entity.transform.localScale.x * 1f, entity.transform.localScale.y, entity.transform.localScale.z);
                    entity.EnemyAnim.SetBool("isMoving", false);
                    break;

                case 1:
                    entity.EnemyTransform.transform.localScale = new Vector3(entity.transform.localScale.x * -1f, entity.transform.localScale.y, entity.transform.localScale.z);
                    entity.EnemyAnim.SetBool("isMoving", true);
                    break;
            }

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            if (distance >= entity.ChaseArea.x / 2)
            {
                entity.ChangeState(EnemyStates.EnemyNormal_Idle);
            }
        }

        public override void Exit(EnemyStatus entity)
        {
            Debug.Log("������ ����");
        }
    }

    public class EnemyNormal_Attack : State<EnemyStatus>
    {
        public override void Enter(EnemyStatus entity)
        {
            Debug.Log("���� ���� �غ�");
        }

        public override void Execute(EnemyStatus entity)
        {
            Debug.Log("���� ����!");

        }

        public override void Exit(EnemyStatus entity)
        {
            Debug.Log("���� ���� ����.");
        }
    }

    public class EnemyNormal_Damaged : State<EnemyStatus>
    {
        public override void Enter(EnemyStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(EnemyStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(EnemyStatus entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class EnemyNormal_Die : State<EnemyStatus>
    {
        public override void Enter(EnemyStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(EnemyStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(EnemyStatus entity)
        {
            throw new System.NotImplementedException();
        }

    }
}
