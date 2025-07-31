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

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //this will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
                

            }
            
        }
        //Animation Eent
        void Hit()
        {
            //Health healthComponent = target.GetComponent<Health>();
            target.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        

        public void Cancel() //��@IAction������Cancel��k
        {
            GetComponent<Animator>().SetTrigger("StopAttack");
            target = null;
        }

        
    }
}
