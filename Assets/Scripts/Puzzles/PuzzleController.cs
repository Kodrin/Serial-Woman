using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] protected Puzzle clockPuzzle;
    [SerializeField] protected Puzzle paintingPuzzle;
    

    protected void OnEnable()
    {
        EventManager.Instance.OnCameraSwitch += CheckPuzzleControls; //add so it will enable interaction when camera is switching
    }

    protected void OnDisable()
    {
        EventManager.Instance.OnCameraSwitch -= CheckPuzzleControls; //add so it will enable interaction when camera is switching
    }

    protected void CheckPuzzleControls()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        
        if (currentShotType == ShotType.GRANDFATHER_SHOT)
        {
            EnablePuzzle(clockPuzzle);
            DisablePuzzle(paintingPuzzle);
        }
        else if (currentShotType == ShotType.PAINTING_SHOT)
        {
            //todo need to fix the can interact bool on the coroutine so it doesnt conflict with this 
            EnablePuzzle(paintingPuzzle);
            DisablePuzzle(clockPuzzle);
        }
        else
        {
            DisablePuzzle(paintingPuzzle);
            DisablePuzzle(clockPuzzle);
        }
    }

    protected void EnablePuzzle(Puzzle puzzle)
    {
        puzzle.canInteract = true;
    }

    protected void DisablePuzzle(Puzzle puzzle)
    {
        puzzle.canInteract = false;
    }
}
