using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour, ISubscribe
{
    [Header("PUZZLE")]
    public bool canInteract = false;
    public bool noteOpen = false; 
    public bool solved = false;
    public bool isSolvable = true;
    
    protected virtual void Start(){}
    protected virtual void Update()
    {
        //disable puzzle if a note is open
        if (!canInteract || noteOpen) return;
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

    protected void OnEnable()
    {
        Subscribe();
    }

    protected void OnDisable()
    {
        Unsubscribe();
    }
    public virtual void Subscribe()
    {
        EventHandler.OnNoteOpen += DetectNoteOpen;

    }

    public virtual void Unsubscribe()
    {
        EventHandler.OnNoteOpen -= DetectNoteOpen;

    }

    void DetectNoteOpen(bool isOpen)
    {
        noteOpen = isOpen;
        Debug.Log("PUZZLE " + isOpen);
    }
}
