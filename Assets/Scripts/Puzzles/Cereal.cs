using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cereal : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialPosition;
    private bool isOnSlot;
    public string letter;
    public CerealPuzzle puzzle;

    void Start()
    {
        initialPosition = gameObject.transform.position;
    }
    void OnMouseDown()
    {
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
        float cerealXpos = gameObject.transform.position.x;
        float cerealZpos = gameObject.transform.position.z;
        float curContXpos, curContZpos, nearestXpos, nearestZpos;
        double curDist;
        double nearestDist = 44; //0.1415 is the maximum allowed distance to be consider on slot
        CerealContainer nearestContainer;

        //Check whic container is closest to the position we let go of the mouse at
        foreach(CerealContainer cont in puzzle.containers)
        {
            curContXpos = cont.gameObject.transform.position.x;
            curContZpos = cont.gameObject.transform.position.z;
            curDist = computeDistance(cerealXpos, curContXpos, cerealZpos, curContZpos);
            if(curDist < nearestDist)
            {
                nearestDist = curDist;
                nearestXpos = curContXpos;
                nearestZpos = curContZpos;
                nearestContainer = cont;
                isOnSlot = true;
            }
            Debug.Log(nearestDist);
        }

        if (!isOnSlot)
        {
            transform.position = initialPosition;
        }
    }
    
    double computeDistance(float y1, float y2, float x1, float x2)
    {
        return (Math.Sqrt(Math.Exp(y2-y1) + Math.Exp(x2-x1)));
    }    

    //COLLISION METHOD
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
