using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShot : MonoBehaviour
{
    public ShotType shotType;
    public CinemachineVirtualCamera vCamComponent;
    public List<Hotspot> hotspots = new List<Hotspot>();

    protected void Awake()
    {
        vCamComponent = this.GetComponent<CinemachineVirtualCamera>();
        hotspots = this.GetComponentsInChildren<Hotspot>().ToList();
    }

    protected void Start()
    {
        if (CameraController.Instance.currentCameraShot != this)
        {
            ShowHotspots(false);
        }
    }

    public void ShowHotspots(bool active)
    {
        if (hotspots.Count > 0)
        {
            foreach (var hotspot in hotspots)
            {
                hotspot.gameObject.SetActive(active);
            }
        }
    }
}
