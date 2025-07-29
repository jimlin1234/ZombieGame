using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);  //Mathf.Max(a,b)它會傳回所有輸入參數中的最大值
            print(health);
            /*
            if (health > 0)
            {
                health -= damage;
            }*/

        }
        
    }
}
