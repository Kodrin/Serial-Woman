using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [System.Serializable]
    public enum LampConfiguration
    {
        NORMAL,
        RED,
        GREEN,
        BLUE
    }

    [SerializeField] protected LampConfiguration lampConfiguration;
    [SerializeField] protected Color normalColor;
    [SerializeField] protected Color redColor;
    [SerializeField] protected Color greenColor;
    [SerializeField] protected Color blueColor;
    protected Light lightComponent;

    protected void Awake()
    {
        lightComponent = this.GetComponentInChildren<Light>();
    }

    
    protected void OnMouseDown()
    {
        ToggleLight();
    }

    public void ToggleLight()
    {
        lightComponent.enabled = !lightComponent.enabled;
    }

    //CONFIGURATIONS
    public void NormalConfiguration()
    {
        lightComponent.color = normalColor;
        InvokeOnLampConfigSwitch();
    }

    public void RedConfiguration()
    {
        lightComponent.color = redColor;
        InvokeOnLampConfigSwitch();
    }

    public void GreenConfiguration()
    {
        lightComponent.color = greenColor;
        InvokeOnLampConfigSwitch();
    }

    public void BlueConfiguration()
    {
        lightComponent.color = blueColor;
        InvokeOnLampConfigSwitch();
    }

    protected void InvokeOnLampConfigSwitch()
    {

        EventHandler.CallOnLampConfigSwitch();
    }
}
