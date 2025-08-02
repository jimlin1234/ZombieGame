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

        Health target; //�Q�I�����ؼ�(���W����Health���󪺡A�C�ӥؼг��|��Health)
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
            transform.LookAt(target.transform); //�����ɬݦV�Q��������
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
            GetComponent<Animator>().SetTrigger("Attack");  //���ʵe�NĲ�oHit()
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

        public bool CanAttack(GameObject combatTarget)  //CombatTarget combatTarget ��Ray�g�쪺���ļĤH(���W��CombatTarget�ե�)
        {
            if(combatTarget == null) return false;
            //if(!combatTarget.GetComponent<Health>()) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Attack(GameObject combatTarget)  //��PlayerController �� AIController�I�s
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        

        public void Cancel() //��@IAction������Cancel��k
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
