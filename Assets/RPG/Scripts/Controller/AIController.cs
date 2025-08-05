using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    //Enemy Controller
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5.0f; //�ĤH�}�l�l��player���Z��

        GameObject player;
        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 guardPosition; //ĵ�í쥻��m

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            guardPosition = transform.position; //�@�}�l�ᤩĵ�í쥻����m(ĵ�ín��^���a��)
        }
        private void Update()
        {
            if(health.IsDead()) return;
            
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                //fighter.Cancel();
                mover.StartMoveAction(guardPosition); //ĵ�ê�^
            }
        }

        private bool InAttackRangeOfPlayer()
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
