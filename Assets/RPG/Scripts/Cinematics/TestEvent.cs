using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Cinematics
{
    public class TestEvent : MonoBehaviour
    {
        public event Action<float> testFunction;
        public event Action<float> testFunction2;

        private void Start()
        {
            Invoke("TestFunction", 10f);
            Invoke("TestFunction2", 5.0f);
            //TestFunction();
        }
        void TestFunction()
        {
            //print("123456");
            testFunction(2.3f);
        }

        void TestFunction2()
        {
            testFunction2(2.2f);
        }
    }
}

