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

    
    
    #region Clock Events
    //CLOCK
    public delegate void ClockSolveEvent();
    public event ClockSolveEvent OnClockSolve;
    public void CallOnClockSolve() { if (OnClockSolve != null) { OnClockSolve(); } }
    
    public delegate void AnyArmMoveEvent();
    public event AnyArmMoveEvent OnAnyArmMove;
    public void CallOnAnyArmMove() { if (OnAnyArmMove != null) { OnAnyArmMove(); } }
    
    public delegate void SmallArmMoveEvent(int position);
    public event SmallArmMoveEvent OnSmallArmMove;
    public void CallOnSmallArmMove(int position) { if (OnSmallArmMove != null) { OnSmallArmMove(position); } }
    
    public delegate void MiddleArmMoveEvenet(int position);
    public event MiddleArmMoveEvenet OnMiddleArmMove;
    public void CallOnMiddleArmMove(int position) { if (OnMiddleArmMove != null) { OnMiddleArmMove(position); } }
    
    public delegate void LongArmMoveEvent(int position);
    public event LongArmMoveEvent OnLongArmMove;
    public void CallOnLongArmMove(int position) { if (OnLongArmMove != null) { OnLongArmMove(position); } }

    #endregion

    
    
    //LAMP
    public delegate void LampSwitchEvent();
    public event LampSwitchEvent OnLampConfigSwitch;
    public void CallOnLampConfigSwitch() { if (OnLampConfigSwitch != null) { OnLampConfigSwitch(); } }


    //Note on how to use events and how to subscribe functions to them 
    protected void OnEnable()
    {
        // OnCameraSwitch += DebugEvent;
        // OnSmallArmMove += SignatureFunction;
    }

    protected void SignatureFunction(int position)
    {
        
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
