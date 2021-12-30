using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public Milk milk;
    public bool spill;
    public bool nomilk = false;

    private void Update()
    {
        if (spill)
        {
            Spill();
            spill = false;
        }
    }

    private void OnMouseDown()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        if ((currentShotType != ShotType.TABLE_SHOT) && (currentShotType != ShotType.CHAIR_SHOT)) return;
        EventHandler.PublishOnTextControllerReset();
        if (!nomilk)
            EventHandler.PublishOnTextControllerMsg("It's a cereal bowl filled with milk.");
        else
            EventHandler.PublishOnTextControllerMsg("The bowl was knocked over somehow...");
    }
    public void Spill()
    {
        if (nomilk) return;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.07f, this.transform.localPosition.z);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z + 47.3f);
        milk.gameObject.SetActive(false);
        nomilk = true;
    }

}
