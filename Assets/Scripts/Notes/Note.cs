using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    public string noteName;
    public List<string> pages = new List<string>();

    public void AddPage(string txt)
    {
        pages.Add(txt);
    }
}
