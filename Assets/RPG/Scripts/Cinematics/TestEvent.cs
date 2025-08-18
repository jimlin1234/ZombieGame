using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public event Action<float> testFunction;

    private void Start()
    {
        Invoke("TestFunction", 10f);
        //TestFunction();
    }
    void TestFunction()
    {
        //print("123456");
        testFunction(2.3f);
    }
}
