using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

[System.Serializable]
public enum CameraShot
{
    ESTABLISHING_SHOT,
    CHAIR_SHOT,
    BOWL_SHOT,
    GRANDFATHER_SHOT,
    PAINTING_SHOT
}

public class CameraController : Singleton<CameraController>
{
    public CameraShot currentCameraShot = CameraShot.ESTABLISHING_SHOT;

    //virtual cameras
    public CinemachineVirtualCamera establishingShot;
    public CinemachineVirtualCamera chairShot;
    public CinemachineVirtualCamera bowlShot;
    public CinemachineVirtualCamera grandfatherShot;
    public CinemachineVirtualCamera paintingShot;
    
    public Dictionary<CameraShot, CinemachineVirtualCamera> cameras = new Dictionary<CameraShot, CinemachineVirtualCamera>();
    
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
        cameras.Add(CameraShot.ESTABLISHING_SHOT, establishingShot);
        cameras.Add(CameraShot.CHAIR_SHOT, chairShot);
        cameras.Add(CameraShot.BOWL_SHOT, bowlShot);
        cameras.Add(CameraShot.GRANDFATHER_SHOT, grandfatherShot);
        cameras.Add(CameraShot.PAINTING_SHOT, paintingShot);
    }

    //will get coresponding 
    public CinemachineVirtualCamera GetVirtualCameraOfType(CameraShot type)
    {
        return cameras[type];
    }

    //will switch to wanted camera with priority setting 
    public void SwitchCameraTo(CameraShot to)
    {
        foreach (var cam in cameras.Keys)
        {
            if (cam == to)
                cameras[cam].Priority = 1;
            else
                cameras[cam].Priority = 0;

        }

        currentCameraShot = to; //update current camera shot 
    }
    
    protected void CameraSwitchControl()
    {
        //switch to establishing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraTo(CameraShot.ESTABLISHING_SHOT);
        }
        
        //switch to chair shot
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraTo(CameraShot.CHAIR_SHOT);
        }

        //switch to bowlShot
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraTo(CameraShot.BOWL_SHOT);
        }
        
        //switch to grandfatherShot
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCameraTo(CameraShot.GRANDFATHER_SHOT);
        }
        
        //switch to paintingShot
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchCameraTo(CameraShot.PAINTING_SHOT);
        }
    }
    
    
}
