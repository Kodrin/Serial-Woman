using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Color lampColor;
    public Light associatedLight;

    void Start()
    {
        lampColor = associatedLight.color;
    }

    void OnMouseDown()
    {
        SetColor(Color.red);
        associatedLight.enabled = !associatedLight.enabled;
    }

    void SetColor(Color c)
    {
        lampColor = c;
        associatedLight.color = lampColor;
    }

    Color GetColor()
    {
        return lampColor;
    }
}
