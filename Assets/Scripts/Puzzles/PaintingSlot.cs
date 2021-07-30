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



// public class Cereal
// {
//     public bool isOnslot = false;
//     public string associatedLetter = "a";
//
//     public CerealContainer container;
//
//     // public string GetContainerLetter()
//     // {
//     //     container.contaidwnerLetter;
//     // }
//
//
// }
//
// public class CerealContainer
// {
//     public string containerLetter = "a";
//     public string contaidwnerLetter = "a";
//     public string containwdwdqwerLetter = "a";
//     public string containwderLetter = "a";
//     public string containeqdwrLetter = "a";
//     public string containerqwdwdLetter = "a";
//     public string containewdrLetter = "a";
//     public string containerwqwdqdLetter = "a";
//     public string containerwdqwdLetter = "a";
// }

