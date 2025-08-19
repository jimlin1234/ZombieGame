using RPG.Controller;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;  //�NDisableControl()���U��played�ƥ�  (��}�l����(play)�ɡA������Player������)
            GetComponent<PlayableDirector>().stopped += EnableControl;  //EnableControl()���U��stopped�ƥ�  (��������(stop)�ɡA�}�ҹ�Player������)

            ///
            ///����Event
            ///
            GetComponent<TestEvent>().testFunction += Test;
            GetComponent<TestEvent>().testFunction2 += Test2;
            GetComponent<TestEvent>().testFunction3 += Test3;
            ////////////////
        }
        void DisableControl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();

            player.GetComponent<PlayerController>().enabled = false;

            //print("DisableControl");
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
            //print("EnableControl");
        }


        /// <summary>
        /// ����Event
        /// </summary>
        /// <param name="a">����Event</param>
        void Test(float a)
        {
            print("456789");
        }

        void Test2(float b)
        {
            print("AAAAAA");
        }

        void Test3()
        {
            print("BBBBB");
        }
        //////////////////////////////////
    }
}
