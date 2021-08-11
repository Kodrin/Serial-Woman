using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler 
{
    //events
    
    public delegate void CameraSwitchEvent();
    public static event CameraSwitchEvent OnCameraSwitch;
    
    public static void CallOnCameraSwitch() { if (OnCameraSwitch != null) { OnCameraSwitch(); } }

    public delegate void PaintingSolveEvent();
    public static event PaintingSolveEvent OnPaintingSolve;
    public static void CallOnPaintingSolve() { if (OnPaintingSolve != null) { OnPaintingSolve(); } }
    
    public delegate void CerealSolveEvent();
    public static event CerealSolveEvent OnCerealSolve;
    public static void CallOnCerealSolve() { if (OnCerealSolve != null) { OnCerealSolve(); } }

    
    
    #region Clock Events
    //CLOCK
    public delegate void ClockSolveEvent();
    public static event ClockSolveEvent OnClockSolve;
    public static void CallOnClockSolve() { if (OnClockSolve != null) { OnClockSolve(); } }
    
    public delegate void AnyArmMoveEvent();
    public static event AnyArmMoveEvent OnAnyArmMove;
    public static void CallOnAnyArmMove() { if (OnAnyArmMove != null) { OnAnyArmMove(); } }
    
    public delegate void SmallArmMoveEvent(int position);
    public static event SmallArmMoveEvent OnSmallArmMove;
    public static void CallOnSmallArmMove(int position) { if (OnSmallArmMove != null) { OnSmallArmMove(position); } }
    
    public delegate void MiddleArmMoveEvenet(int position);
    public static event MiddleArmMoveEvenet OnMiddleArmMove;
    public static void CallOnMiddleArmMove(int position) { if (OnMiddleArmMove != null) { OnMiddleArmMove(position); } }
    
    public delegate void LongArmMoveEvent(int position);
    public static event LongArmMoveEvent OnLongArmMove;
    public static void CallOnLongArmMove(int position) { if (OnLongArmMove != null) { OnLongArmMove(position); } }

    #endregion

    
    
    //LAMP
    public delegate void LampSwitchEvent();
    public static event LampSwitchEvent OnLampConfigSwitch;
    public static void CallOnLampConfigSwitch() { if (OnLampConfigSwitch != null) { OnLampConfigSwitch(); } }

    
}