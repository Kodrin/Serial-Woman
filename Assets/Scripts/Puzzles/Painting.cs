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
    public RotationHeading targetRotation;
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

    private void OnMouseDown()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        if ((paintingType == PaintingType.LADY) || (paintingType == PaintingType.DOG))
        {
            if (currentShotType == ShotType.PAINTING_SHOT)
            {
                if (paintingType == PaintingType.LADY)
                    EventHandler.PublishOnTextControllerMsg("The woman in the frame seems to be pointing to something.");
                else
                    EventHandler.PublishOnTextControllerMsg("A white dog has appeared in the frame. The woman is now pointing in a different direction.");
            }
            else
                return;
        }
        else
            return;
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

    public void setHeading(RotationHeading r)
    {
        currentRotation = r;
    }
}


