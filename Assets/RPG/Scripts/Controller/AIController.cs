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
        [SerializeField] float chaseDistance = 5.0f; //�ĤH�}�l�l��player���Z��
        [SerializeField] float suspicionTime = 8.0f; //�ĤH�h�îɶ�
        [SerializeField] PatrolPath patrolPath; //�ĤH���޸��|
        [SerializeField] float wayPointTolerance = 1f;
        int currentWayPointIndex = 0;

        GameObject player;
        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 guardPosition; //�ĤH�쥻��m
        float timeSinceLastSawPlayer = Mathf.Infinity; //�ĤH�W���̫�l��(�ݨ�)player���g�L�ɶ�(�A���ݨ��|�k0)

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            guardPosition = transform.position; //�@�}�l�ᤩ�ĤH�쥻����m(�ĤH�n��^���a��)
        }
        private void Update()
        {
            if(health.IsDead()) return;
            
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                //Player�i�J�ĤHĵ�ٽd��ӧ���Player
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                //Player�����ĤH�ĤHĵ�ٽd��A�ĤH�i�J�h�ê��A
                SuspicionBehaviour();
            }
            else
            {
                //fighter.Cancel();
                //�ĤH��^�ܨ��ަ欰
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
            if(patrolPath != null) //�p�G�s�b���޸��|
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
            if (distanceToWayPoint < wayPointTolerance) //�O�_�w��FwayPoint(�ĤH�Z��wayPoint�p��1�Y���w��F)
            {
                return true; //�w��FwayPoint
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

        private Vector3 GetCurrentWayPoint() //���o�n�쪺wayPoint��m
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }



        private bool InAttackRangeOfPlayer()  //Player�i�J�ĤHĵ�ٽd��
        {
            
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;  //�p�Gplayer �P Enemy���Z���p��chaseDistance�h�^��true
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
}
