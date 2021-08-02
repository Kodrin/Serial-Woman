using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PaintingType
{
    EYE,
    BURNING_HOUSE,
    SELF
}

public class Painting : MonoBehaviour
{
    public PaintingType paintingType;
}
