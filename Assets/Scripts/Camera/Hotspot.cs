using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class Hotspot : MonoBehaviour
{
    public CameraShot cameraShot; 
    
    // Update is called once per frame
    void Update()
    {
        FaceCamera();
    }

    protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if you click on the hotspot, send event to camera shot
            CameraController.Instance.SwitchCameraToCamShot(cameraShot);
        }
    }

    protected void FaceCamera()
    {
        this.transform.LookAt(Camera.main.transform.position);
    }
}
