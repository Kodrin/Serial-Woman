using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    //events
    public delegate void CameraChangeEvent();
    public event CameraChangeEvent OnCameraChange;

    public void CallOnCameraChange() { if (OnCameraChange != null) { OnCameraChange(); } }

    public delegate void PaintingSolveEvent();
    public event PaintingSolveEvent OnPaintingSolve;
    public void CallOnPaintingSolve() { if (OnPaintingSolve != null) { OnPaintingSolve(); } }
    
    public delegate void CerealSolveEvent();
    public event CerealSolveEvent OnCerealSolve;
    public void CallOnCerealSolve() { if (OnCerealSolve != null) { OnCerealSolve(); } }
    
    public delegate void ClockSolveEvent();
    public event ClockSolveEvent OnClockSolve;
    public void CallOnClockSolve() { if (OnClockSolve != null) { OnClockSolve(); } }
}
