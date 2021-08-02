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
    public ShotType currentShotType = ShotType.ESTABLISHING_SHOT;

    //virtual cameras
    public CinemachineVirtualCamera establishingShot;
    public CinemachineVirtualCamera chairShot;
    public CinemachineVirtualCamera bowlShot;
    public CinemachineVirtualCamera grandfatherShot;
    public CinemachineVirtualCamera paintingShot;
    
    public Dictionary<ShotType, CinemachineVirtualCamera> cameras = new Dictionary<ShotType, CinemachineVirtualCamera>();
    


    // Start is called before the first frame update
    protected void Start()
    {
        InitializeCameras();
    }

    // Update is called once per frame
    protected void Update()
    {
        CameraSwitchControl();
        
    }

    //add them to dictionary
    protected void InitializeCameras()
    {
        cameras.Add(ShotType.ESTABLISHING_SHOT, establishingShot);
        cameras.Add(ShotType.CHAIR_SHOT, chairShot);
        cameras.Add(ShotType.BOWL_SHOT, bowlShot);
        cameras.Add(ShotType.GRANDFATHER_SHOT, grandfatherShot);
        cameras.Add(ShotType.PAINTING_SHOT, paintingShot);
    }

    //will get coresponding 
    public CinemachineVirtualCamera GetVirtualCameraOfType(ShotType type)
    {
        return cameras[type];
    }

    //will switch to wanted camera with priority setting 
    public void SwitchCameraTo(ShotType to)
    {
        foreach (var cam in cameras.Keys)
        {
            if (cam == to)
                cameras[cam].Priority = 1;
            else
                cameras[cam].Priority = 0;

        }

        currentShotType = to; //update current camera shot 
    }
    
    // public void SwitchCameraTo(CinemachineVirtualCamera to)
    // {
    //     foreach (var cam in cameras.Values)
    //     {
    //         if (cam == to)
    //             cameras[cam].Priority = 1;
    //         else
    //             cameras[cam].Priority = 0;
    //
    //     }
    //
    //     currentShotType = to; //update current camera shot 
    // }
    
    protected void CameraSwitchControl()
    {
        //switch to establishing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraTo(ShotType.ESTABLISHING_SHOT);
        }
        
        //switch to chair shot
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraTo(ShotType.CHAIR_SHOT);
        }

        //switch to bowlShot
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraTo(ShotType.BOWL_SHOT);
        }
        
        //switch to grandfatherShot
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCameraTo(ShotType.GRANDFATHER_SHOT);
        }
        
        //switch to paintingShot
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchCameraTo(ShotType.PAINTING_SHOT);
        }
    }
    
    
}
