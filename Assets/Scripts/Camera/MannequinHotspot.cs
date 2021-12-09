using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinHotspot : Hotspot, ISubscribe
{
    public GameObject gui;

    private void Start()
    {
        gui.SetActive(false);
    }

    public void DisableInteraction(string colorName, Color color)
    {
        if (colorName == "blue")
        {
            gui.SetActive(true);
        }
        else
        {
            gui.SetActive(false);
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
