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
