using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesStructure
{
    public List<NoteStructure> notes = new List<NoteStructure>();

    public void AddNote(NoteStructure nt)
    {
        notes.Add(nt);
    }
}
