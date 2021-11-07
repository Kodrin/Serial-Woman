using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : Singleton<LightsController>
{
    
    [SerializeField] protected List<Light> lights = new List<Light>();

    public List<Light> Lights
    {
        get { return lights;}
        set { lights = value; }
    }


    public void TurnOnAllLights()
    {
        foreach (var l in lights)
        {
            l.enabled = true;
        }
    }

    public void TurnOffAllLights()
    {
        foreach (var l in lights)
        {
            l.enabled = false;
        }
    }
}
