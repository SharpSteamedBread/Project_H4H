using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterDontMoveOwnedStates
{
    public class Enemy_Idle : State<EnemySecondStatus>
    {
        public override void Enter(EnemySecondStatus entity)
        {
            Debug.Log("���� ��� ����");
        }

        public override void Execute(EnemySecondStatus entity)
        {
            //Debug.Log("���� �����");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            //Debug.Log($"�÷��̾�-���� �Ÿ�: {distance}, ���� ����: {detectArea.x / 2}");

            //���� ���� ����
            if (distance <= entity.AttackArea.x / 2)
            {
                //Debug.Log("������!");

                //Debug.Log($"{attackCooltime}�ʰ� ��ٷ�!");
                //yield return new WaitForSeconds(entity.AttackCooltime);
                //Debug.Log($"����!");
                entity.ChangeState(EnemySecondStates.Enemy_Attack);
            }
        }

        public override void Exit(EnemySecondStatus entity)
        {
            Debug.Log("���� �õ��Ŵ���...");
        }
    }


    public class Enemy_Attack : State<EnemySecondStatus>
    {
        public override void Enter(EnemySecondStatus entity)
        {
            Debug.Log("���� ���� �غ�");
        }

        public override void Execute(EnemySecondStatus entity)
        {
            Debug.Log("���� ����!");

        }

        public override void Exit(EnemySecondStatus entity)
        {
            Debug.Log("���� ���� ����.");
        }
    }

    public class Enemy_Damaged : State<EnemySecondStatus>
    {
        public override void Enter(EnemySecondStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(EnemySecondStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(EnemySecondStatus entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Enemy_Die : State<EnemySecondStatus>
    {
        public override void Enter(EnemySecondStatus entity)
        {
            Debug.Log("�׾���...");
            entity.EnemyAnim.SetBool("isDead", true);
            entity.EnemyDie();
        }

        public override void Execute(EnemySecondStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(EnemySecondStatus entity)
        {
            throw new System.NotImplementedException();
        }

    }
}
