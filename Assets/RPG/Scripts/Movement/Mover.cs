using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction
    {
        NavMeshAgent navMeshAgent;
        Health health;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead())
            {
                navMeshAgent.enabled = false; //���`������navMeshAgent
            }
            
            UpdateAnimator(); //�ʵe
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);  //�i�DActionScheduler this�{�b���ʧ@
            //GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel() //��@IAction������Cancel��k   Mover �� Fighter�����~�ӡA�ҥH���n��@����k
        {
            navMeshAgent.isStopped = true;
        }

        
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

    }
}
