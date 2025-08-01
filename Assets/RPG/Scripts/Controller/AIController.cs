using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5.0f; //敵人開始追擊player的距離

        private void Update()
        {
            
            if(DistanceToPlayer() < chaseDistance)
            {
                print(gameObject.name + "should chase");
            }

        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}
