using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WallText : MonoBehaviour, ISubscribe
{
    [SerializeField] protected List<Material> textMaterials;
    protected MeshRenderer meshRend;

    protected void Awake()
    {
        meshRend = this.GetComponent<MeshRenderer>();
    }

    protected void Start()
    {
        meshRend.enabled = false;  //disable text on start
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
        EventHandler.OnLampPowerToggle += LampToggle;
    }

    public void Unsubscribe()
    {
        EventHandler.OnSmallArmMove -= CheckTime;
        EventHandler.OnLampPowerToggle -= LampToggle;
    }

    public void LampToggle(bool isOn)
    {
        meshRend.enabled = !isOn;
    }

    public void CheckTime(int armPosition)
    {
        switch(armPosition)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                meshRend.material = textMaterials[0];
                break;
            case 7:
                meshRend.material = textMaterials[1];
                break;
            case 8:
                meshRend.material = textMaterials[2];
                break;
            case 9:
                meshRend.material = textMaterials[3];
                break;
            case 10:
                meshRend.material = textMaterials[4];
                break;
            case 11:
                meshRend.material = textMaterials[5];
                break;
            case 12:
                meshRend.material = textMaterials[6];
                break;
        }
    }
}
