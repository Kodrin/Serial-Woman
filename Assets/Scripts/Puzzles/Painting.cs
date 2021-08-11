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
    public Texture alternateTexture;
    private Texture currentTexture;

    private void Start()
    {
        currentRotation = initialRotation;
        currentTexture = gameObject.GetComponent<Renderer>().material.mainTexture;

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

    private Texture GetTexture()
    {
        return currentTexture;
    }

    public void SetTexture(Texture t)
    {
        currentTexture = t;
        gameObject.GetComponent<Renderer>().material.mainTexture = currentTexture;
    }
}


