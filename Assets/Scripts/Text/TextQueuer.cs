using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextQueuer : MonoBehaviour
{
    public List<string> textSample = new List<string>();
    private int count = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TextController.Instance.QueueText(GetNextLine());
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
