using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;

            ///
            ///´ú¸ÕEvent
            ///
            GetComponent<TestEvent>().testFunction += Test;
            GetComponent<TestEvent>().testFunction2 += Test2;
        }
        void DisableControl(PlayableDirector pd)
        {
            print("DisableControl");
        }

        void EnableControl(PlayableDirector pd)
        {
            print("EnableControl");
        }


        /// <summary>
        /// ´ú¸ÕEvent
        /// </summary>
        /// <param name="a">´ú¸ÕEvent</param>
        void Test(float a)
        {
            print("456789");
        }

        void Test2(float b)
        {
            print("AAAAAA");
        }
    }
}
