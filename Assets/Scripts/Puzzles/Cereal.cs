using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cereal : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialPosition, previousPosition;
    private float initialRotationY;
    public char letter; //letter of the alphabet associated to this cereal piece
    public CerealPuzzle puzzle;
    public CerealContainer associatedContainer;

    void Start()
    {
        initialPosition = gameObject.transform.position; //store the initial out-of-bowl position
        initialRotationY = gameObject.transform.eulerAngles.y; //store the initial out-of-bowl rotation
    }
    void Update()
    {
        //disable interaction if a note is open
        if (!puzzle.canInteract || puzzle.noteOpen)
        {
            return;
        }
    }
    void OnMouseDown()
    {
        previousPosition = gameObject.transform.position; //store the position of the piece before move
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
        float iDist; //temp var for loop to avoid computing Distance twice
        float nearestDist = float.MaxValue; //init distance at max value
        //threshold distance to be considered on-slot. 
        float maxDist = 0.025f * puzzle.transform.lossyScale.x; //we must account for the scale of the puzzle
        CerealContainer nearestContainer = null;
        Cereal occupyingCereal = null;

        //Check which container is closest to the position we let go of the mouse at
        foreach (CerealContainer cont in puzzle.containers)
        {
            iDist = Vector3.Distance(gameObject.transform.position, cont.transform.position);
            if (iDist < nearestDist)
            {
                nearestDist = iDist;
                nearestContainer = cont;
            }
        }
        /* DEBUG BLOCK
        Debug.Log(puzzle.transform.lossyScale.x);
        Debug.Log("Nearest Container is: " + nearestContainer);
        Debug.Log("Distance to that container is: " + nearestDist +". Max dist was: " + maxDist);
        */
        occupyingCereal = nearestContainer.getPiece();
        //if the slot is not too far and not already occupied, snap the cereal to the slot
        if (nearestDist <= maxDist)
        {
            if (associatedContainer == nearestContainer) //if nearest container is current container, do nothing
                transform.position = previousPosition;
            else if (nearestContainer.getOccupied() == false)
            {
                if (associatedContainer) //if we are swapping between an empty CerealContainer and an occupied one
                {
                    associatedContainer.setPiece(); //clear the associated piece. 
                    associatedContainer.setOccupied(false);
                }
                nearestContainer.setOccupied(true);
                nearestContainer.setPiece(this);
                associatedContainer = nearestContainer;
                transform.position = nearestContainer.transform.position;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            else //if the slot is already occupied with another cereal piece
            {
                if (associatedContainer) // if we swap with a cereal thats already on a tile
                {
                    occupyingCereal.transform.position = previousPosition;
                    //swap Ceareal <-> CerealContainer associations as we swap the position of the pieces
                    occupyingCereal.associatedContainer = associatedContainer;
                    associatedContainer.setPiece(occupyingCereal);
                    checkMatch(occupyingCereal);
                }
                else //if we swap with a cereal thats out-of-bowl
                {
                    occupyingCereal.transform.position = occupyingCereal.initialPosition;
                    occupyingCereal.associatedContainer = null;
                }
                associatedContainer = nearestContainer;
                nearestContainer.setPiece(this);
                //swap Ceareal <-> CerealContainer associations as we swap the position of the pieces
                transform.position = nearestContainer.transform.position;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            //check if the letter attributed to the cereal and the cereal container match
            checkMatch(this);
            puzzle.CheckWin();
        }
        else //if we are not close enough to a slot, send the pice to its initial out-of-bowl position
        {
            transform.position = initialPosition;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, initialRotationY, transform.eulerAngles.z);
            //when moving from slot to initial position we must clear Container <=> Cereal associations 
            if (associatedContainer)
            {
                associatedContainer.setOccupied(false);
                associatedContainer.setPiece(); //sets associated cereal piece to NULL
                associatedContainer.setMatch(false);
                associatedContainer = null;
            }
        }
    }

    void checkMatch(Cereal c)
    {
        Debug.Log("SLOT: " + c.associatedContainer.associatedLetter + ", LETTER: " + c.letter);
        if (c.associatedContainer.associatedLetter == c.letter)
            c.associatedContainer.setMatch(true);
        else
            c.associatedContainer.setMatch(false);
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
