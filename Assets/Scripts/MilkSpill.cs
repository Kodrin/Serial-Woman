using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkSpill : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<MeshRenderer>().enabled = false; 
    }

    public void Spill()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }
}
