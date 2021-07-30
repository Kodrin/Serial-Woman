using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    
    [System.Serializable]
    public enum PaintingType
    {
        EYE,
        BURNING_HOUSE,
        SELF
    }

    public PaintingType paintingType;

}
