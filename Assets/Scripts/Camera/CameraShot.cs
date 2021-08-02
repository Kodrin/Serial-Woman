using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShot : MonoBehaviour
{
    public List<CameraShot> nextCameraShots = new List<CameraShot>();
    public CinemachineVirtualCamera vCamComponent;
    public Hotspot hotspot;

    public delegate void HotspotClickEvent();
    public event HotspotClickEvent OnHotspotClick;
    
    protected void Start()
    {
        
    }

    protected void Update()
    {
        
    }

    protected void OnEnable()
    {
        SubcribeToHotspot();
    }

    protected void OnDisable()
    {
        UnsubscribeToHotspot();
    }

    //todo if this camera is not enabled, disable its hotspot
    public void GoToShot(CinemachineVirtualCamera shot)
    {
        // CameraController.Instance.SwitchCameraTo(shot);
    }

    protected void SubcribeToHotspot()
    {
        
    }

    protected void UnsubscribeToHotspot()
    {
        
    }
    
}
