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

    protected virtual void Controls(){}
    protected virtual void CheckSolveCondition(){}
    protected virtual void Resolve(){}
    protected virtual void Reset(){}
    
}
