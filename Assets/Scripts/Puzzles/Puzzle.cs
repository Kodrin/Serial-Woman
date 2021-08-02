using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public bool canInteract = false;
    public bool solved = false;
    
    protected virtual void Start(){}
    protected virtual void Update()
    {
        if (!canInteract) return;
    }

    //where we encapsulate the controls for the puzzle
    protected virtual void Controls(){}
    
    //where we chech for the win condition
    protected virtual void CheckSolveCondition(){}
    
    //what happens if we win
    protected virtual void ResolveState(){}
    
    //what happens if we fail
    protected virtual void FailState(){}
    
    //How to reset the puzzle
    protected virtual void ResetPuzzle(){}
    
}
