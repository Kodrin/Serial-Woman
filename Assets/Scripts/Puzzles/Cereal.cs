using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cereal : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialPosition, previousPosition;
    public char letter;
    public bool isMatching;
    public CerealPuzzle puzzle;
    public CerealContainer associatedContainer;

    void Start()
    {
        initialPosition = gameObject.transform.position;
    }
    void OnMouseDown()
    {
        previousPosition = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    
    void OnMouseUp()
    {
        float iDist; // temp var for loop to avoid computing Distance twice
        float nearestDist = float.MaxValue; // init distance at max value
        float maxDist = 0.3f;
        CerealContainer nearestContainer = null;
        Cereal occupyingCereal;

        //Check which container is closest to the position we let go of the mouse at
        foreach(CerealContainer cont in puzzle.containers)
        {
            iDist = Vector3.Distance(gameObject.transform.position, cont.transform.position);
            if (iDist< nearestDist)
            {
                nearestDist = iDist;
                nearestContainer = cont;      
            }
        }
        //if the slot is not too far and not already occupied, snap the cereal to the slot
        if (nearestDist <= maxDist)
        {
            if (nearestContainer.getOccupied() == false) 
            {
                nearestContainer.setOccupied(true);
                nearestContainer.setPiece(this);
                associatedContainer = nearestContainer;
                transform.position = nearestContainer.transform.position;
            }
            else //if the slot is already occupied with another cereal piece
            {
                occupyingCereal = nearestContainer.getPiece();
                occupyingCereal.transform.position = previousPosition;
                transform.position = nearestContainer.transform.position;
                nearestContainer.setPiece(this);
                //swap Ceareal <-> CerealContainer associations as we swap the position of the pieces
                if (associatedContainer)
                    occupyingCereal.associatedContainer = associatedContainer;
                associatedContainer = nearestContainer;
            }
            //check if the letter attributed to the cereal and the cereal container match
            checkMatch();
            puzzle.CheckWin();
        }
        else
        {
            transform.position = initialPosition;
            isMatching = false;
        }
    }

    void checkMatch()
    {
        Debug.Log("SLOT: " + associatedContainer.associatedLetter + ", LETTER: " + letter);
        if (associatedContainer.associatedLetter == letter)
            isMatching = true;
        else
            isMatching = false;
    }

    //COLLISION METHOD (REQUIRES RIGIDBODY) ***DEPRECATED***
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CerealContainer component))
        {
            isOnSlot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CerealContainer component))
        {
            isOnSlot = false;
        }
    }
    */
}
