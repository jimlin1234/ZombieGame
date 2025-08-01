using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            if (InteractWithCombat() == true) //如果在戰鬥(==ture)，則跳出(不會執行移動)
            {
                return;
            }

            if (InteractWithMovement() == true)//如果在移動(==ture)，則跳出(不會執行戰鬥)
            {
                return; 
            }
            //print("nothing");
        }

        private bool InteractWithCombat()  
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
               CombatTarget target =  hit.transform.GetComponent<CombatTarget>(); //擁有CombatTarget組件的hit
                if(target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    
                    GetComponent<Fighter>().Attack(target);
                    
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement() 
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                    
                }
                return true;
            }
            return false;
        }
        /*
        private void MoveToCorsor()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0))
                GetComponent<Mover>().MoveTo(hit.point);
            }
            return true;
        }*/

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}