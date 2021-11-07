using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealPuzzle : Puzzle
{
    public List<CerealContainer> containers = new List<CerealContainer>();

    protected override void CheckSolveCondition()
    {
        bool temp = true; //assume you won
        foreach(CerealContainer c in containers)
        {
            //if one of the containers does not have a matching cereal bit, you did not win
            if (c.getMatch() == false)
                temp = false;
        }
        if (temp == true)
        {
            solved = temp;
            ResolveState();
        }
    }

    protected override void ResolveState()
    {
        EventHandler.PublishOnCerealSolve();
    }

    public void CheckWin()
    {
        CheckSolveCondition();
    }
}
