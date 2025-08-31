using RPG.Movement;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        

        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);  //Mathf.Max(a,b)它會傳回所有輸入參數中的最大值
            //print(healthPoints);
            if (healthPoints == 0)
            {
                Die();
            }

        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            //GetComponent<Collider>().enabled = false;
        }

        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}
