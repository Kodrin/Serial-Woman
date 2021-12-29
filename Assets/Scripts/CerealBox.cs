using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealBox : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        if ((currentShotType != ShotType.TABLE_SHOT) && (currentShotType != ShotType.CHAIR_SHOT)) return;
        EventHandler.PublishOnTextControllerReset();
        EventHandler.PublishOnTextControllerMsg("An empty cereal box.");
    }
}
