using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] protected Puzzle clockPuzzle;
    [SerializeField] protected Puzzle paintingPuzzle;
    [SerializeField] protected Puzzle cerealPuzzle;
    [SerializeField] protected CameraController cameraController;
    [SerializeField] protected GameObject outro;

    protected void OnEnable()
    {
        EventHandler.OnCameraSwitch += CheckPuzzleControls; //add so it will enable interaction when camera is switching
        EventHandler.OnCerealSolve += EndGame;
        EventHandler.OnLastTrack += PlayEnding;
        EventHandler.OnIntroComplete += StartGame;
    }

    protected void OnDisable()
    {
        EventHandler.OnCameraSwitch -= CheckPuzzleControls; //add so it will enable interaction when camera is switching
        EventHandler.OnCerealSolve -= EndGame;
        EventHandler.OnLastTrack -= PlayEnding;
        EventHandler.OnIntroComplete -= StartGame;
    }

    private void Start()
    {
        //Hotspots should be disabled until intro cutscene is complete
        cameraController.DisableHotspots();
    }

    protected void CheckPuzzleControls()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        Debug.Log("Curren Shot Type is : " + currentShotType);
        if (currentShotType == ShotType.GRANDFATHER_SHOT)
        {
            EnablePuzzle(clockPuzzle);
            DisablePuzzle(paintingPuzzle);
            DisablePuzzle(cerealPuzzle);
        }
        else if (currentShotType == ShotType.PAINTING_SHOT)
        {
            //todo need to fix the can interact bool on the coroutine so it doesnt conflict with this 
            EnablePuzzle(paintingPuzzle);
            DisablePuzzle(clockPuzzle);
            DisablePuzzle(cerealPuzzle);
        }
        else if (currentShotType == ShotType.BOWL_DETAIL_SHOT)
        {
            EnablePuzzle(cerealPuzzle);
            DisablePuzzle(paintingPuzzle);
            DisablePuzzle(clockPuzzle);
        }
        else
        {
            DisablePuzzle(paintingPuzzle);
            DisablePuzzle(clockPuzzle);
            DisablePuzzle(cerealPuzzle);
        }
    }

    protected void StartGame()
    {
        cameraController.EnableHotspots();
    }
    protected void EndGame()
    {
        cameraController.DisableHotspots();
    }

    protected void PlayEnding()
    {
        outro.SetActive(true);
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
