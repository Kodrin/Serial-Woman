using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    //virtual cameras
    public CinemachineVirtualCamera establishingShot;
    public CinemachineVirtualCamera bowlShot;
    public CinemachineVirtualCamera grandfatherShot;
    public CinemachineVirtualCamera paintingShot;
    
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        CameraSwitchControl();
    }

    protected void CameraSwitchControl()
    {
        //switch to establishing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            establishingShot.Priority = 1;
            bowlShot.Priority = 0;
            grandfatherShot.Priority = 0;
            paintingShot.Priority = 0;
        }
        
        
        //switch to bowlShot
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            establishingShot.Priority = 0;
            bowlShot.Priority = 1;
            grandfatherShot.Priority = 0;
            paintingShot.Priority = 0;
        }
        
        //switch to grandfatherShot
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            establishingShot.Priority = 0;
            bowlShot.Priority = 0;
            grandfatherShot.Priority = 1;
            paintingShot.Priority = 0;
        }
        
        //switch to paintingShot
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            establishingShot.Priority = 0;
            bowlShot.Priority = 0;
            grandfatherShot.Priority = 0;
            paintingShot.Priority = 1;
        }
    }
    
    
}
