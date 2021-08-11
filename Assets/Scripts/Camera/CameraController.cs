using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

[System.Serializable]
public enum ShotType
{
    ESTABLISHING_SHOT,
    CHAIR_SHOT,
    BOWL_SHOT,
    GRANDFATHER_SHOT,
    PAINTING_SHOT
}

public class CameraController : Singleton<CameraController>
{

    public CameraShot currentCameraShot;
    public List<CameraShot> cameraShots = new List<CameraShot>();

    protected void OnEnable()
    {

        // EventManager.OnCameraChange += SwitchCameraToCamShot;
    }


    //will get coresponding 
    public CameraShot GetCameraShotOfType(ShotType type)
    {
        foreach (var cam in cameraShots)
        {
            if (cam.shotType == type)
                return cam;
        }

        return null;
    }

    //will switch to wanted camera with priority setting 
    public void SwitchCameraToShotType(ShotType to)
    {
        CameraShot toCamShot = null;
        foreach (var cam in cameraShots)
        {
            if (cam.shotType == to)
            {
                cam.vCamComponent.Priority = 1;
                toCamShot = cam;
            }
            else
                cam.vCamComponent.Priority = 0;
    
        }
    
        if(currentCameraShot) currentCameraShot.ShowHotspots(false); //disable hotspots of previous camera
        currentCameraShot = toCamShot; //update current camera shot 
        currentCameraShot.ShowHotspots(true); //enable hotspots of current camera
        
        //trigger camera switch event
        EventHandler.CallOnCameraSwitch();

    }
    
    public void SwitchCameraToCamShot(CameraShot to)
    {
        foreach (var cam in cameraShots)
        {
            if (cam == to)
                cam.vCamComponent.Priority = 1;
            else
                cam.vCamComponent.Priority = 0;
        
        }
        
        //disable / enable hotspots
        if(currentCameraShot) currentCameraShot.ShowHotspots(false); //disable hotspots of previous camera
        currentCameraShot = to; //update current camera shot 
        currentCameraShot.ShowHotspots(true); //enable hotspots of current camera
        
        //trigger camera switch event
        EventHandler.CallOnCameraSwitch();
    }
    
    
    protected void DebugCameraSwitchControl()
    {
        //switch to establishing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraToShotType(ShotType.ESTABLISHING_SHOT);
        }
        
        //switch to chair shot
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraToShotType(ShotType.CHAIR_SHOT);
        }
        
        //switch to bowlShot
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraToShotType(ShotType.BOWL_SHOT);
        }
        
        //switch to grandfatherShot
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCameraToShotType(ShotType.GRANDFATHER_SHOT);
        }
        
        //switch to paintingShot
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchCameraToShotType(ShotType.PAINTING_SHOT);
        }
    }
    
    
}
