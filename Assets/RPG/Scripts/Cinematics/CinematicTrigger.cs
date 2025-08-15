using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            bool alreadyTriggered = false;

            if(alreadyTriggered == false && other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                alreadyTriggered = true;
            }
            
            if(alreadyTriggered == true)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}

