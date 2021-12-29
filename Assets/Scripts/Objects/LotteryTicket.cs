using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryTicket : MonoBehaviour, ISubscribe
{
    public Material clueLightOn;
    public Material clueLightOff;
    public bool paintingSolved = false;
    public bool lightOn = true;

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
        EventHandler.OnPaintingSolve += ClueToggle;
        EventHandler.OnLampPowerToggle += LampToggle;
    }

    public void Unsubscribe()
    {
        EventHandler.OnPaintingSolve -= ClueToggle;
        EventHandler.OnLampPowerToggle -= LampToggle;
    }

    private void OnMouseDown()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        if ((currentShotType == ShotType.TABLE_SHOT) || (currentShotType == ShotType.CHAIR_SHOT))
        {
            EventHandler.PublishOnTextControllerReset();
            EventHandler.PublishOnTextControllerMsg("It's a lottery ticket.");
            if (!lightOn && paintingSolved)
            {
                EventHandler.PublishOnTextControllerMsg("It's wet to the touch. There appear to be blood stains and other markings on the ticket.");
            }
            else
            {
                EventHandler.PublishOnTextControllerMsg("It's wet to the touch but there are no visible markings on the ticket.");
            }
        }

        else if (currentShotType == ShotType.LOTTERY_SHOT)
        {
            EventHandler.PublishOnTextControllerReset();
            if (paintingSolved)
            {
                EventHandler.PublishOnTextControllerMsg("There are strange markings on the ticket. Perhaps they hold some importance.");
            }
            else
            {
                EventHandler.PublishOnTextControllerMsg("Just a plain old lottery ticket.");
            }
        }
    }

    // Start is called before the first frame update
    public void LampToggle(bool lampOn)
    {
        lightOn = lampOn; 
        if (paintingSolved)
        {
            if (lampOn)
            {
                GetComponent<Renderer>().material = clueLightOn;
            }
            else
            {
                GetComponent<Renderer>().material = clueLightOff;
            }
        }
    }
    public void ClueToggle()
    {
        if (lightOn)
        {
            GetComponent<Renderer>().material = clueLightOn;
        }
        else
        {
            GetComponent<Renderer>().material = clueLightOff;
        }
        paintingSolved = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
