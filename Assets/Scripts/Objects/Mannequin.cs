using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mannequin : MonoBehaviour, ISubscribe
{
    [SerializeField] protected GameObject mannequinHotspot;
    protected MeshRenderer meshRend;
    protected MeshRenderer[] meshRendChildren;

    protected void Awake()
    {
        meshRend = this.GetComponent<MeshRenderer>();
        meshRendChildren = GetComponentsInChildren<MeshRenderer>();
    }

    protected void Start()
    {
        // mannequinHotspot.SetActive(false);
        meshRend.enabled = false;
        foreach (var m in meshRendChildren)
        {
            m.enabled = false;
        }
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
            foreach (var m in meshRendChildren)
            {
                m.enabled = true;
                // mannequinHotspot.SetActive(true);
            }
        }
        else
        {
            meshRend.enabled = false;
            foreach (var m in meshRendChildren)
            {
                m.enabled = false;
                // mannequinHotspot.SetActive(false);
            }
        }
    }
}
