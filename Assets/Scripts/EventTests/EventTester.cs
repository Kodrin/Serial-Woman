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
            EventHandler.PublishOnTestEvent();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventHandler.PublishOnTestParameterEvent(10);
        }
    }

    void OnEnable()
    {
        EventHandler.OnTestEvent += DebugRed;
        EventHandler.OnTestEvent += DebugGreen;
        EventHandler.OnTestEvent += DebugBlue;
        EventHandler.OnTestParameterEvent += Arithmetic;
    }

    void OnDisable()
    {
        EventHandler.OnTestEvent -= DebugRed;
        EventHandler.OnTestEvent -= DebugGreen;
        EventHandler.OnTestEvent -= DebugBlue;
        EventHandler.OnTestParameterEvent -= Arithmetic;
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
