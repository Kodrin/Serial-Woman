using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealInteraction : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialPosition;
    private bool isOnSlot;

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
        if (!isOnSlot)
        {
            transform.position = initialPosition;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("letter"))
            isOnSlot = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("letter"))
            isOnSlot = true;
        else
            Debug.Log("Collided but not slot.");
    }
}
