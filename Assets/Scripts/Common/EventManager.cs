using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    //events
    
    public delegate void CameraSwitchEvent();
    public event CameraSwitchEvent OnCameraSwitch;
    
    public void CallOnCameraSwitch() { if (OnCameraSwitch != null) { OnCameraSwitch(); } }

    public delegate void PaintingSolveEvent();
    public event PaintingSolveEvent OnPaintingSolve;
    public void CallOnPaintingSolve() { if (OnPaintingSolve != null) { OnPaintingSolve(); } }
    
    public delegate void CerealSolveEvent();
    public event CerealSolveEvent OnCerealSolve;
    public void CallOnCerealSolve() { if (OnCerealSolve != null) { OnCerealSolve(); } }
    
    public delegate void ClockSolveEvent();
    public event ClockSolveEvent OnClockSolve;
    public void CallOnClockSolve() { if (OnClockSolve != null) { OnClockSolve(); } }
    
    
    //LAMP
    public delegate void LampSwitchEvent();
    public event LampSwitchEvent OnLampConfigSwitch;
    public void CallOnLampConfigSwitch() { if (OnLampConfigSwitch != null) { OnLampConfigSwitch(); } }


    //Note on how to use events and how to subscribe functions to them 
    protected void OnEnable()
    {
        // OnCameraSwitch += DebugEvent;
    }

    protected void OnDisable()
    {
        // OnCameraSwitch -= DebugEvent;
    }

    protected void DebugEvent()
    {
        Debug.Log("OnCameraSwitch has been triggered!");
    }
}
