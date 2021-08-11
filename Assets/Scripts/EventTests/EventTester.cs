using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventHandlerTest.PublishOnTestEvent();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventHandlerTest.PublishOnTestParameterEvent(10);
        }
    }

    void OnEnable()
    {
        EventHandlerTest.OnTestEvent += DebugRed;
        EventHandlerTest.OnTestEvent += DebugGreen;
        EventHandlerTest.OnTestEvent += DebugBlue;
        EventHandlerTest.OnTestParameterEvent += Arithmetic;
    }

    void OnDisable()
    {
        EventHandlerTest.OnTestEvent -= DebugRed;
        EventHandlerTest.OnTestEvent -= DebugGreen;
        EventHandlerTest.OnTestEvent -= DebugBlue;
        EventHandlerTest.OnTestParameterEvent -= Arithmetic;
    }

    void DebugRed()
    {
        Debug.Log("Red Event!");
    }
    
    void DebugGreen()
    {
        Debug.Log("Green Event!");
    }
    
    void DebugBlue()
    {
        Debug.Log("Blue Event!");
    }

    void Arithmetic(int number)    
    {
        int result = number + 5;
        Debug.Log($"Result is {result}");
    }
    
}
