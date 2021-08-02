using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] protected Puzzle clockPuzzle;
    [SerializeField] protected Puzzle paintingPuzzle;

    [SerializeField] protected CameraController cameraController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //todo use camera events to call this , only testing right now
        //e.g on camera switch
        if (cameraController.currentShotType == ShotType.GRANDFATHER_SHOT)
        {
            EnablePuzzle(clockPuzzle);
            DisablePuzzle(paintingPuzzle);
        }

        if (cameraController.currentShotType == ShotType.PAINTING_SHOT)
        {
            EnablePuzzle(paintingPuzzle);
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
