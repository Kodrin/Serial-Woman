using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInteraction : MonoBehaviour
{
    // public Text txt;
    private int currentLine = 0;
    // private int lastLine;
    public List<string> textLines = new List<string>();
    // private static float timeToDisplay = 3f; // The length of time to display text
    // private static float timeToClear; // The time when the text must be cleared

    // void Start()
    // {
    //     txt.enabled = false;
    //     lastLine = textLines.Count;
    // }
    //
    // void Update()
    // {
    //     if (txt.enabled && (Time.time >= timeToClear))
    //     {
    //         txt.enabled = false;
    //     }
    // }
    //
    // void PrintText(string textToPrint)
    // {
    //     txt.text = textToPrint;
    //     txt.enabled = true;
    //     timeToClear = Time.time + timeToDisplay;
    // }

    void OnMouseOver() 
    {
        // Do stuff on mouse-over
    }

    void OnMouseDown()
    {
        if (textLines.Count > 0)
        {
            EventHandler.PublishOnTextControllerMsg(textLines[currentLine]);
            if (currentLine + 1 < textLines.Count)
            {
                currentLine++;
            }
            else
            {
                currentLine = 0;
            }
        }

        // if(textLines[currentLine-1] != null) //check if line is non-null before trying to print text
        // {
        //     PrintText(textLines[currentLine - 1]);
        //     if (currentLine != lastLine)
        //         currentLine++;
        //     else
        //         currentLine = 1;
        // }
    }

    void OnMouseExit()
    {
        //Do stuff on mouse-off
    }
}
