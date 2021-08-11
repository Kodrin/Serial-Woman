using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextQueuer : MonoBehaviour
{
    public Note targetNote;
    public List<string> textSample = new List<string>();
    private int count = 0;

    void Start()
    {
        //if a note object is associated, load all of its pages to Queue
        if (targetNote)
        {
            for(int i = 0; i < targetNote.pages.Count; i++)
            {
                textSample.Add(targetNote.pages[i].content);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventHandler.PublishOnTextControllerMsg(GetNextLine());
        }
    }

    string GetNextLine()
    {
        if (count + 1 < textSample.Count)
        {
            count++;
            return textSample[count];
        }
        else
        {
            count = 0;
            return textSample[count];
        }
    }
}
