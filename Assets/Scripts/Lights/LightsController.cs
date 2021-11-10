using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightsController : Singleton<LightsController>, ISubscribe
{

    [SerializeField] protected Light[] lights;

    public Light[] Lights
    {
        get { return lights;}
        set { lights = value; }
    }


    private void Awake()
    {
        lights = this.GetComponentsInChildren<Light>();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha9))
    //     {
    //         ToggleAllLights(false);
    //     }
    // }

    public void ToggleAllLights(bool isOn)
    {
        foreach (var l in lights)
        {
            l.enabled = isOn;
        }
    }

    public void SwitchLightColor(string colorName, Color color)
    {
        foreach (var l in lights)
        {
            l.color = color;
        }
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void Subscribe()
    {
        EventHandler.OnLampConfigSwitch += SwitchLightColor;
        EventHandler.OnLampPowerToggle += ToggleAllLights;
    }

    public void Unsubscribe()
    {
        EventHandler.OnLampConfigSwitch -= SwitchLightColor;
        EventHandler.OnLampPowerToggle -= ToggleAllLights;
        
    }
}
