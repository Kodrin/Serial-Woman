using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public delegate void TestEvent();
    public static event TestEvent OnTestEvent;
    public static void PublishOnTestEvent() { if (OnTestEvent != null) { OnTestEvent(); } }
    
    
    public delegate void TestParameterEvent(int number);
    public static event TestParameterEvent OnTestParameterEvent;
    public static void PublishOnTestParameterEvent(int number) { if (OnTestParameterEvent != null) { OnTestParameterEvent(number); } }
}
