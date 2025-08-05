using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    //Enemy Controller
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5.0f; //敵人開始追擊player的距離

        GameObject player;
        Fighter fighter;
        Health health;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
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
