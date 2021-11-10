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


    [SerializeField] protected bool lightOn = true;
    [SerializeField] protected LampConfiguration lampConfiguration;
    [SerializeField] protected Color normalColor;
    [SerializeField] protected Color redColor;
    [SerializeField] protected Color greenColor;
    [SerializeField] protected Color blueColor;
    protected Light lightComponent;

    public bool LightOn => lightOn;
    
    protected void Awake()
    {
        lightComponent = this.GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleLight();
        }
    }

    protected void OnMouseDown()
    {
        ToggleLight();
    }

    public void ToggleLight()
    {
        lightComponent.enabled = !lightComponent.enabled;
        lightOn = lightComponent.enabled;
        EventHandler.PublishOnLampPowerToggle(lightComponent.enabled);

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
        EventHandler.PublishOnLampConfigSwitch("yellow", normalColor);

    }

    public void RedConfiguration()
    {
        lightComponent.color = redColor;
        EventHandler.PublishOnLampConfigSwitch("red", Color.red);

    }

    public void GreenConfiguration()
    {
        lightComponent.color = greenColor;
        EventHandler.PublishOnLampConfigSwitch("green", Color.green);

    }

    public void BlueConfiguration()
    {
        lightComponent.color = blueColor;
        EventHandler.PublishOnLampConfigSwitch("blue", Color.blue);

    }

}
