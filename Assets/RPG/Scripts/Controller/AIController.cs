using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    //Enemy Controller
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5.0f; //敵人開始追擊player的距離
        [SerializeField] float suspicionTime = 8.0f; //敵人懷疑時間
        [SerializeField] PatrolPath patrolPath; //敵人巡邏路徑
        [SerializeField] float wayPointTolerance = 1f;
        int currentWayPointIndex = 0;

        GameObject player;
        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 guardPosition; //敵人原本位置
        float timeSinceLastSawPlayer = Mathf.Infinity; //敵人上次最後追擊(看見)player的經過時間(再次看見會歸0)

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            guardPosition = transform.position; //一開始賦予敵人原本的位置(敵人要返回的地方)
        }
        private void Update()
        {
            if(health.IsDead()) return;
            
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                //Player進入敵人警戒範圍而攻擊Player
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                //Player脫離敵人敵人警戒範圍，敵人進入懷疑狀態
                SuspicionBehaviour();
            }
            else
            {
                //fighter.Cancel();
                //敵人返回至巡邏行為
                PatrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }
        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPostion = guardPosition;
            if(patrolPath != null) //如果存在巡邏路徑
            {
                if (AtWayPoint())
                {
                    CycleWayPoint();
                }
                nextPostion = GetCurrentWayPoint();
            }
            mover.StartMoveAction(nextPostion);
        }
        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            if (distanceToWayPoint < wayPointTolerance) //是否已到達wayPoint(敵人距離wayPoint小於1即為已到達)
            {
                return true; //已到達wayPoint
            }
            else
            {
                return false;
            }
        }
        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint() //取得要到的wayPoint位置
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }



        private bool InAttackRangeOfPlayer()  //Player進入敵人警戒範圍
        {
            
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;  //如果player 與 Enemy的距離小於chaseDistance則回傳true
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
}
