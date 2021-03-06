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
    public bool noteOpen;
    public float maxTime;
    float elapsedTime = 0.0f;

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
        if (lightComponent.color == blueColor)
        {
            if (elapsedTime >= maxTime)
            {
                maxTime = UnityEngine.Random.Range(0.1f, 1.0f);
                ToggleLight();
                elapsedTime = 0.0f;
                return;
            }
            elapsedTime += Time.deltaTime;
        }
    }

    protected void OnMouseDown()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        if (!noteOpen && (currentShotType == ShotType.CHAIR_SHOT || currentShotType == ShotType.TABLE_SHOT))
        {
            if (lightComponent.color == blueColor)
            {
                EventHandler.PublishOnTextControllerReset();
                EventHandler.PublishOnTextControllerMsg("The lamp is malfunctioning.");
            }
            else
                ToggleLight();
        }
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
        EventHandler.OnNoteOpen += DetectNoteOpen;
    }

    public void Unsubscribe()
    {
        EventHandler.OnSmallArmMove -= CheckConfiguration;
        EventHandler.OnNoteOpen -= DetectNoteOpen;
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
        EventHandler.PublishOnLampConfigSwitch("red", redColor);

    }

    public void GreenConfiguration()
    {
        lightComponent.color = greenColor;
        EventHandler.PublishOnLampConfigSwitch("green", greenColor);

    }

    public void BlueConfiguration()
    {
        maxTime = UnityEngine.Random.Range(0.1f, 1.0f);
        lightComponent.color = blueColor;
        EventHandler.PublishOnLampConfigSwitch("blue", blueColor);

    }

    void DetectNoteOpen(bool isOpen)
    {
        noteOpen = isOpen;
    }

}
