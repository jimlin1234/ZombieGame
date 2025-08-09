using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead()) return; 

            if (InteractWithCombat() == true) //�p�G�b�԰�(==ture)�A�h���X(���|���沾��)
            {
                return;
            }

            if (InteractWithMovement() == true)//�p�G�b����(==ture)�A�h���X(���|����԰�)
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
                CombatTarget target =  hit.transform.GetComponent<CombatTarget>(); //���o�֦�CombatTarget�ե�hit
                if(target == null) continue;
                GameObject targetGameObject = target.gameObject; //�N�Qhit�쪺�֦�CombatTarget�ե�target �ର GameObject
                if (!GetComponent<Fighter>().CanAttack(targetGameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    
                    GetComponent<Fighter>().Attack(targetGameObject);
                    
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