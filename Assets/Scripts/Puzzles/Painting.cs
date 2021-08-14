using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PaintingType
{
    DEMON,
    HORSE,
    SLEEP,
    LADY,
    DOG
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
    private Texture currentTexture;
    public bool isInteractable = true;

    private void Start()
    {
        if ((paintingType == PaintingType.LADY) || (paintingType == PaintingType.DOG))
            isInteractable = false; 

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

    public Texture GetTexture()
    {
        return currentTexture;
    }

    public void SetTexture(Texture t)
    {
        currentTexture = t;
        gameObject.GetComponent<Renderer>().material.mainTexture = currentTexture;
    }
}


