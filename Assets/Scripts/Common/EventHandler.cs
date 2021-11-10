using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler 
{
    //MAIN EVENTS
    
    public delegate void CameraSwitchEvent();
    public static event CameraSwitchEvent OnCameraSwitch;
    
    public static void PublishOnCameraSwitch() { if (OnCameraSwitch != null) { OnCameraSwitch(); } }

    public delegate void BaronSolveEvent();
    public static event BaronSolveEvent OnBaronSolve;
    public static void PublishOnBaronSolve() { if (OnBaronSolve != null) { OnBaronSolve(); } }

    public delegate void PaintingSolveEvent();
    public static event PaintingSolveEvent OnPaintingSolve;
    public static void PublishOnPaintingSolve() { if (OnPaintingSolve != null) { OnPaintingSolve(); } }
    
    public delegate void CerealSolveEvent();
    public static event CerealSolveEvent OnCerealSolve;
    public static void PublishOnCerealSolve() { if (OnCerealSolve != null) { OnCerealSolve(); } }

    
    
    #region Clock Events
    //CLOCK
    public delegate void AnyArmMoveEvent();
    public static event AnyArmMoveEvent OnAnyArmMove;
    public static void PublishOnAnyArmMove() { if (OnAnyArmMove != null) { OnAnyArmMove(); } }
    
    public delegate void SmallArmMoveEvent(int position);
    public static event SmallArmMoveEvent OnSmallArmMove;
    public static void PublishOnSmallArmMove(int position) { if (OnSmallArmMove != null) { OnSmallArmMove(position); } }
    
    public delegate void MiddleArmMoveEvenet(int position);
    public static event MiddleArmMoveEvenet OnMiddleArmMove;
    public static void PublishOnMiddleArmMove(int position) { if (OnMiddleArmMove != null) { OnMiddleArmMove(position); } }
    
    public delegate void LongArmMoveEvent(int position);
    public static event LongArmMoveEvent OnLongArmMove;
    public static void PublishOnLongArmMove(int position) { if (OnLongArmMove != null) { OnLongArmMove(position); } }

    #endregion

    //TEXTCONTROLLER
    public delegate void TextControllerMsgEvent(string message);
    public static event TextControllerMsgEvent OnTextControllerMsg;
    public static void PublishOnTextControllerMsg(string message) { if (OnTextControllerMsg != null) { OnTextControllerMsg(message); } }
    
    //LAMP
    public delegate void LampSwitchEvent(string colorName, Color color);
    public static event LampSwitchEvent OnLampConfigSwitch;
    public static void PublishOnLampConfigSwitch(string colorName, Color color) { if (OnLampConfigSwitch != null) { OnLampConfigSwitch(colorName, color); } }

    public delegate void LampPowerEvent(bool isOn);
    public static event LampPowerEvent OnLampPowerToggle;
    public static void PublishOnLampPowerToggle(bool isOn) { if (OnLampPowerToggle != null) { OnLampPowerToggle(isOn); } }

    //RADIO
    public delegate void IntroCompleteEvent();
    public static event IntroCompleteEvent OnIntroComplete;
    public static void PublishOnIntroComplete() { if (OnIntroComplete != null) { OnIntroComplete(); } }

    
}
