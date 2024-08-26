using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterDontMoveOwnedStates
{
    public class Enemy_Idle : State<EnemySecondStatus>
    {
        public override void Enter(EnemySecondStatus entity)
        {
            Debug.Log("몬스터 대기 시작");
        }

        public override void Execute(EnemySecondStatus entity)
        {
            //Debug.Log("몬스터 대기중");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            //Debug.Log($"플레이어-몬스터 거리: {distance}, 감지 범위: {detectArea.x / 2}");

            //공격 상태 입장
            if (distance <= entity.AttackArea.x / 2)
            {
                //Debug.Log("감지함!");

                //Debug.Log($"{attackCooltime}초간 기다려!");
                //yield return new WaitForSeconds(entity.AttackCooltime);
                //Debug.Log($"공격!");
                entity.ChangeState(EnemySecondStates.Enemy_Attack);
            }
        }

        public override void Exit(EnemySecondStatus entity)
        {
            Debug.Log("몬스터 시동거는중...");
        }
    }


    public class Enemy_Attack : State<EnemySecondStatus>
    {
        public override void Enter(EnemySecondStatus entity)
        {
            Debug.Log("몬스터 공격 준비");
        }

        public override void Execute(EnemySecondStatus entity)
        {
            Debug.Log("몬스터 공격!");

        }

        public override void Exit(EnemySecondStatus entity)
        {
            Debug.Log("몬스터 공격 중지.");
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
            Debug.Log("죽었다...");
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
