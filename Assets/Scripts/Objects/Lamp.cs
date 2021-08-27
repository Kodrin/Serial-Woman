using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, ISubscribe
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
        EventHandler.PublishOnLampPowerToggle();

    }

    protected void OnEnable()
    {
        Subscribe();
    }

    protected void OnDisable()
    {
        Unsubscribe();
    }

    public void Subscribe()
    {
        EventHandler.OnSmallArmMove += CheckConfiguration;
    }

    public void Unsubscribe()
    {
        EventHandler.OnSmallArmMove -= CheckConfiguration;
    }

    

    //CONFIGURATIONS

    public void CheckConfiguration(int armPosition)
    {
        //if its 11 o clock, change light to blue, else keep normal light
        if (armPosition == 11)
        {
            BlueConfiguration();
        }
        else
        {
            NormalConfiguration();
        }
    }
    
    public void NormalConfiguration()
    {
        lightComponent.color = normalColor;
        EventHandler.PublishOnLampConfigSwitch("yellow");

    }

    public void RedConfiguration()
    {
        lightComponent.color = redColor;
        EventHandler.PublishOnLampConfigSwitch("red");

    }

    public void GreenConfiguration()
    {
        lightComponent.color = greenColor;
        EventHandler.PublishOnLampConfigSwitch("green");

    }

    public void BlueConfiguration()
    {
        lightComponent.color = blueColor;
        EventHandler.PublishOnLampConfigSwitch("blue");

    }

}
