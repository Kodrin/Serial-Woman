using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInteraction : MonoBehaviour
{
    public Text txt;
    private static int bowlCount = 1;
    private static int cerealCount = 1;
    private static int milkCount = 1;
    private static float timeToDisplay = 4f; // The length of time to display text
    private static float timeToClear; // The time when the text must be cleared

    void Start()
    {
        txt.enabled = false;
    }

    void Update()
    {
        if (txt.enabled && (Time.time >= timeToClear))
        {
            txt.enabled = false;
        }
    }

    void PrintText(string textToPrint)
    {
        txt.text = textToPrint;
        txt.enabled = true;
        timeToClear = Time.time + timeToDisplay;
    }

    void OnMouseOver() 
    {
        // Do stuff on mouse-over
    }

    void OnMouseDown()
    {
        if (this.tag == "bowl")
        {
            if (bowlCount == 1)
            {
                PrintText("There is a generous portion of milk inside.");
                bowlCount = 2;
            }
            else
            {
                PrintText("Do I always pour my milk first? You bet I do.");
                bowlCount = 1;
            }
        }

        else if (this.tag == "cereal")
        {
            if (cerealCount == 1)
            {
                PrintText("Letter-O's. I've been eating these since I was kid.");
                cerealCount = 2;
            }
            else if (cerealCount == 2)
            {
                PrintText("I swear they talk to me sometimes.");
                cerealCount = 3;
            }
            else
            {
                PrintText("Sometimes... Not all the time...");
                cerealCount = 1;
            }
        }

        else if (this.tag == "milk")
        {
            if (milkCount == 1)
            {
                PrintText("Already out of milk? I just bought some yesterday morning...");
                milkCount++; 
            }
            else
                PrintText("There is already milk in the bowl.");
        }
    }

    void OnMouseExit()
    {
        //Do stuff on mouse-off
    }
}
