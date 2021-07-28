using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

[System.Serializable]
public enum VantagePointType
{
    ESTABLISHING_SHOT,
    BOWL_SHOT,
    GRANDFATHER_SHOT,
    PAINTING_SHOT
}

public class CameraController : Singleton<CameraController>
{

    //virtual cameras
    public CinemachineVirtualCamera establishingShot;
    public CinemachineVirtualCamera bowlShot;
    public CinemachineVirtualCamera grandfatherShot;
    public CinemachineVirtualCamera paintingShot;
    
    public Dictionary<VantagePointType, CinemachineVirtualCamera> cameras = new Dictionary<VantagePointType, CinemachineVirtualCamera>();
    
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
        cameras.Add(VantagePointType.ESTABLISHING_SHOT, establishingShot);
        cameras.Add(VantagePointType.BOWL_SHOT, bowlShot);
        cameras.Add(VantagePointType.GRANDFATHER_SHOT, grandfatherShot);
        cameras.Add(VantagePointType.PAINTING_SHOT, paintingShot);
    }

    //will get coresponding 
    public CinemachineVirtualCamera GetVirtualCameraOfType(VantagePointType type)
    {
        return cameras[type];
    }

    //will switch to wanted camera with priority setting 
    public void SwitchCameraTo(VantagePointType to)
    {
        foreach (var cam in cameras.Keys)
        {
            if (cam == to)
                cameras[cam].Priority = 1;
            else
                cameras[cam].Priority = 0;

        }
    }
    
    protected void CameraSwitchControl()
    {
        //switch to establishing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraTo(VantagePointType.ESTABLISHING_SHOT);
        }
        
        
        //switch to bowlShot
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraTo(VantagePointType.BOWL_SHOT);
        }
        
        //switch to grandfatherShot
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraTo(VantagePointType.GRANDFATHER_SHOT);
        }
        
        //switch to paintingShot
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCameraTo(VantagePointType.PAINTING_SHOT);
        }
    }
    
    
}
