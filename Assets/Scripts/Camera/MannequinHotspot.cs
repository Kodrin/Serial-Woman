using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinHotspot : Hotspot, ISubscribe
{
    public GameObject gui;

    private void Start()
    {
        // gui.SetActive(false);
    }

    public void DisableInteraction(string colorName, Color color)
    {
        if (colorName == "blue")
        {
            gui.SetActive(true);
            // Debug.Log("Switched to blue");
        }
        else
        {
            gui.SetActive(false);
            // Debug.Log("Switched to disabled");
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
        EventHandler.OnLampConfigSwitch += DisableInteraction;
    }

    public void Unsubscribe()
    {
        EventHandler.OnLampConfigSwitch -= DisableInteraction;
    }
}
