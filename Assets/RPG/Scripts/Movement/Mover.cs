using RPG.Combat;
using RPG.Core;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] float maxSpeed = 6f;

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

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);  //�i�DActionScheduler this�{�b���ʧ@
            //GetComponent<Fighter>().Cancel();
            MoveTo(destination, speedFraction);
        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel() //��@IAction������Cancel��k
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

        public object CaptureState() //��@ISaveable������CaptureState��k
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state) //��@ISaveable������RestoreState��k
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false; //����NavMeshAgent�~�ઽ���]�w��m
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true; //���s�ҥ�NavMeshAgent
        }
    }
}
