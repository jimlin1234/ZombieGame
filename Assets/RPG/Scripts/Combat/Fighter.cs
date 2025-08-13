using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 2f; //�i�J�����d��
        [SerializeField] float timeBetweenAttacks = 1.0f;
        [SerializeField] float weaponDamage = 5.0f;

        Health target; //�Q�I�����ؼ�(���W����Health���󪺡A�C�ӥؼг��|��Health)
        float timeSinceLastAttack = Mathf.Infinity;
        private void Update()
        {
            timeSinceLastAttack = timeSinceLastAttack + Time.deltaTime;
            //print(timeSinceLastAttack);
            if (target == null) return;

            if (target.IsDead()) return;
                //bool isInRange = GetIsInRange();
            if (!GetIsInRange()) //���i�J�����d��
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);  //�~�򩹥ؼв���
            }
            else
            {
                GetComponent<Mover>().Cancel(); //�w�i�J�����d��A�����
                AttackBehaviour(); //�}�l����
                
            }
        }
        private bool GetIsInRange()  //�O�_�i�J�����d��
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
            return targetToTest != null && !targetToTest.IsDead(); //Health���󤣬O�Ū� �B �S�����`  �^��ture
        }
        public void Attack(GameObject combatTarget)  //��PlayerController �� AIController�I�s
        {
            GetComponent<ActionScheduler>().StartAction(this); //�i�DActionScheduler this�{�b���ʧ@(Fighter)
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
