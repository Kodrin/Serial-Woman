using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealPuzzle : MonoBehaviour
{
    private bool youWin;
    public List<CerealContainer> containers = new List<CerealContainer>();

    public void CheckWin()
    {
        bool youWin = true; //assume you won
        foreach(CerealContainer c in containers)
        {
            //if one of the containers does not have a matching cereal bit, you did not win
            if (c.getMatch() == false)
                youWin = false;
        }
        if (youWin)
            Debug.Log("YOU WIN!");
    }
}
