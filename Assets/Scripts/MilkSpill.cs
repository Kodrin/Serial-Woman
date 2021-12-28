using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkSpill : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Spill()
    {
        this.gameObject.SetActive(true);
    }
}
