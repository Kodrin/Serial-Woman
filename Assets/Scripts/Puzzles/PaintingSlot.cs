using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSlot : MonoBehaviour
{
    public int slotID;
    public Painting currentPainting;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);        
    }
}

