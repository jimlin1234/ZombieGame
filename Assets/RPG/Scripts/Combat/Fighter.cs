using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        
        public void Attack(CombatTarget target)
        {
            print(target.gameObject.name + " ,±µ¨ü§ðÀ»§a!!");
        }
    }
}
