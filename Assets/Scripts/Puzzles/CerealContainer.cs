using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealContainer : MonoBehaviour
{
    public char associatedLetter;
    public char trueLetter;
    private bool isOccupied;
    private Cereal associatedPiece;
    public bool isMatching;
    public float swapTime = 1.0f;

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
    public void SwapCereal(Cereal obj)
    {
        Vector3 tempPos = associatedPiece.transform.position; 
        StartCoroutine(WaitForSwap(associatedPiece, associatedPiece.transform.position, obj.transform.position));
        StartCoroutine(WaitForSwap(obj, obj.transform.position, tempPos));
    }

    protected IEnumerator WaitForSwap(Cereal cerealObj, Vector3 originalPos, Vector3 targetPos)
    {
        float elapsedTime = 0;
        while (elapsedTime < swapTime)
        {
            cerealObj.transform.position = Vector3.Slerp(originalPos, targetPos, (elapsedTime / swapTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        cerealObj.transform.position = targetPos; //make sure it snaps to that position
        yield return null;
    }
}
