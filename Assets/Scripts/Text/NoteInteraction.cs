using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    public Note targetNote;
    public Text page;
    public Text title;
    private int currentLine = 0;
    private int lastLine;
    public List<string> textLines = new List<string>();

    void Start()
    {
        page.enabled = false;

        //if a note object is associated, load all of its pages to Queue
        if (targetNote)
        {
            for (int i = 0; i < targetNote.pages.Count; i++)
            {
                textLines.Add(targetNote.pages[i].content);
            }
            title.text = targetNote.noteName;
            title.enabled = true;
        }

        lastLine = textLines.Count;
    }

    void OnMouseDown()
    {
        page.enabled = false;
        if (textLines[currentLine] != null) //check if line is non-null before trying to print text
        {
            page.text = textLines[currentLine];
            page.enabled = true;
            if (currentLine != lastLine-1)
                currentLine++;
            else
            {
                currentLine = 0;
            }
        }
    }
}
