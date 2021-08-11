using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WallText : MonoBehaviour, ISubscribe
{
    [SerializeField] protected int[] triggerTimes;
    protected MeshRenderer meshRend;

    protected void Awake()
    {
        meshRend = this.GetComponent<MeshRenderer>();
    }

    protected void Start()
    {
        meshRend.enabled = false;  //disbale text on start
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
        for (int i = 0; i < triggerTimes.Length; i++)
        {
            if (triggerTimes[i] == armPosition)
            {
                meshRend.enabled = true;
                return;
            }
        }
        
        //if its not matching then disble the component
        meshRend.enabled = false;
    }
}
