using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealPuzzle : MonoBehaviour
{
    private bool youWin;
    public List<Cereal> cereals = new List<Cereal>();
    public List<CerealContainer> containers = new List<CerealContainer>();

    public void CheckWin()
    {
        bool youWin = true; //assume you won
        foreach(Cereal c in cereals)
        {
            //if one cereal is not on the correct slot, you did not win
            if (c.isMatching == false)
                youWin = false;
        }
        if (youWin)
            Debug.Log("YOU WIN!");
    }
}
