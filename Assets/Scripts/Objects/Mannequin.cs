using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mannequin : MonoBehaviour, ISubscribe
{
    protected MeshRenderer meshRend;

    protected void Awake()
    {
        meshRend = this.GetComponent<MeshRenderer>();
    }

    protected void Start()
    {
        meshRend.enabled = false;
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
        EventHandler.OnSmallArmMove += CheckTime;
    }

    public void Unsubscribe()
    {
        EventHandler.OnSmallArmMove -= CheckTime;
    }


    public void CheckTime(int armPosition)
    {
        if (armPosition == 11)
        {
            meshRend.enabled = true;
        }
        else
        {
            meshRend.enabled = false;
        }
    }
}
