using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 5.0f;

        Health target; //被點擊的目標(身上掛有Health元件的，每個目標都會有Health)
        float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack = timeSinceLastAttack + Time.deltaTime;
            
            if (target == null) return;

            if (target.IsDead()) return;
                //bool isInRange = GetIsInRange();
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
                
            }
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); //攻擊時看向被攻擊物件
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //this will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;


            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");  //此動畫將觸發Hit()
        }

        //Animation Eent
        void Hit()
        {
            if (target == null)
            {
                return;
            }
            //Health healthComponent = target.GetComponent<Health>();
            target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(GameObject combatTarget)  //CombatTarget combatTarget 為Ray射到的有效敵人(身上有CombatTarget組件的)
        {
            if(combatTarget == null) return false;
            //if(!combatTarget.GetComponent<Health>()) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Attack(GameObject combatTarget)  //由PlayerController 或 AIController呼叫
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        

        public void Cancel() //實作IAction介面的Cancel方法
        {
            TriggerStopAttak();
            target = null;
        }

        private void TriggerStopAttak()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

    }
}
