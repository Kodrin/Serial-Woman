using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    public Note targetNote;
    public Image img;
    public Text page;
    public Text title;
    private int currentLine = 0;
    private int lastLine;
    public List<string> textLines = new List<string>();

    void Start()
    {
        img.enabled = false;
        page.enabled = false;
        title.enabled = false;

        //if a note object is associated, load all of its pages to Queue
        if (targetNote)
        {
            AddWithLineBreaks(targetNote);
            title.text = targetNote.noteName;
        }

        lastLine = textLines.Count;
    }

    void AddWithLineBreaks(Note nt)
    {
        for (int i = 0; i < nt.pages.Count; i++)
        {
            string[] lines = nt.pages[i].content.Split('*');
            string tmp = "";
            bool firstLine = true;
            foreach (string line in lines)
            {
                if (firstLine)
                {
                    tmp = line;
                    firstLine = false;
                }
                else
                    tmp = tmp + "\n" + line;
            }
            textLines.Add(tmp);
        }
    }

    void OnMouseDown()
    {

        page.enabled = false;
        if (currentLine != lastLine) //check if line is non-null before trying to print text
        {
            page.text = textLines[currentLine];
            page.enabled = true;
            title.enabled = true;
            img.enabled = true;
            currentLine++;
        }
        else
        {
            img.enabled = false;
            page.enabled = false;
            title.enabled = false;
            currentLine = 0;
        }
    }
}
