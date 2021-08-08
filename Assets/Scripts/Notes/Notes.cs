using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes
{
    public List<Note> notes = new List<Note>();

    public void AddNote(Note nt)
    {
        notes.Add(nt);
    }
}
