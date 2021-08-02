using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealContainer : MonoBehaviour
{
    public char associatedLetter;
    private bool isOccupied;
    private Cereal associatedPiece;
    public bool isMatching;

    public Cereal getPiece()
    {
        return associatedPiece;
    }

    public void setPiece()
    {
        associatedPiece = null;
    }
    public void setPiece(Cereal obj)
    {
        associatedPiece = obj;
    }

    public bool getOccupied()
    {
        return isOccupied;
    }

    public void setOccupied(bool inUse)
    {
        isOccupied = inUse;
    }

    public bool getMatch()
    {
        return isMatching;
    }

    public void setMatch(bool match)
    {
        isMatching = match;
    }
}
