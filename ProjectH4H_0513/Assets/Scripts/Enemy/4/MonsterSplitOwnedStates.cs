using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterSplitOwnedStates
{
    public class Enemy_Idle : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터 대기 시작");
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터 대기중");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            //Debug.Log($"플레이어-몬스터 거리: {distance}, 감지 범위: {detectArea.x / 2}");

            //공격 상태 입장
            if (distance <= entity.AttackArea.x / 2)
            {
                //Debug.Log("감지함!");

                //Debug.Log($"{attackCooltime}초간 기다려!");
                //yield return new WaitForSeconds(entity.AttackCooltime);
                //Debug.Log($"공격!");
                entity.ChangeState(EnemyFourthStates.Enemy_Attack);
            }

            //추격 상태 입장
            else if(distance <= entity.EncounterArea.x / 2)
            {
                entity.ChangeState(EnemyFourthStates.Enemy_Move);
            }

            //이동
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
            Debug.Log("몬스터 시동거는중...");
        }
    }


    public class Enemy_Move : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터가 포착");
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터 추격중");

            float distance = Vector2.Distance(entity.TargetPos.position, entity.transform.position);

            if (distance >= entity.EncounterArea.x / 2)
            {
                entity.ChangeState(EnemyFourthStates.Enemy_Idle);
            }
        }

        public override void Exit(EnemyFourthStatus entity)
        {
            Debug.Log("움직임 종료");
        }
    }

    public class Enemy_Attack : State<EnemyFourthStatus>
    {
        public override void Enter(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터 공격 준비");
        }

        public override void Execute(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터 공격!");

        }

        public override void Exit(EnemyFourthStatus entity)
        {
            Debug.Log("몬스터 공격 중지.");
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
