using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PaintingType
{
    EYE,
    SUNOWL,
    CAT,
    OTHER
}

public enum RotationHeading
{
    UP, //0
    RIGHT, //90
    DOWN, //180
    LEFT //270
}

public class Painting : MonoBehaviour
{
    public PaintingType paintingType;
    public RotationHeading initialRotation;
    public RotationHeading currentRotation;

    private void Start()
    {
        currentRotation = initialRotation;

        switch (initialRotation)
        {
            case RotationHeading.UP:
                break;
            case RotationHeading.RIGHT:
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z + 90);
                break;
            case RotationHeading.DOWN:
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z + 180);
                break;
            case RotationHeading.LEFT:
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z + 270);
                break;
        }
    }
}


