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
            GetComponent<PlayableDirector>().played += DisableControl;  //�NDisableControl()���U��played�ƥ�
            GetComponent<PlayableDirector>().stopped += EnableControl;  //EnableControl()���U��stopped�ƥ�

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
            print("DisableControl");
        }

        void EnableControl(PlayableDirector pd)
        {
            print("EnableControl");
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
