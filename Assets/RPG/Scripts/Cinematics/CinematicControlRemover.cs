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
            GetComponent<PlayableDirector>().played += DisableControl;  //將DisableControl()註冊到played事件
            GetComponent<PlayableDirector>().stopped += EnableControl;  //EnableControl()註冊到stopped事件

            ///
            ///測試Event
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
        /// 測試Event
        /// </summary>
        /// <param name="a">測試Event</param>
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
