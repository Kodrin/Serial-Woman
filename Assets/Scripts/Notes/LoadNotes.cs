using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System;
using UnityEngine;


public class LoadNotes : Singleton<LoadNotes>
{
    public NotesStructure noteList; 

    void CreateXML(string nPath)
    {
        NotesStructure noteList = new NotesStructure();
        NoteStructure threeBarons = new NoteStructure();
        threeBarons.AddPage("This is the text for page 1.");
        threeBarons.AddPage("This is the text for page 2.");
        threeBarons.AddPage("This is the text for page 3.");
        threeBarons.noteName = "Three Barons";
        NoteStructure wallPlaque = new NoteStructure();
        wallPlaque.AddPage("This is the text for page 1.");
        wallPlaque.noteName = "Wall Plaque";
        noteList.AddNote(threeBarons);
        noteList.AddNote(wallPlaque);

        XmlSerializer makeSerial = new XmlSerializer(typeof(NotesStructure));
        StreamWriter writer = new StreamWriter(nPath);
        makeSerial.Serialize(writer, noteList);
    }

    public static NotesStructure LoadXML(string path)
    {
        XmlSerializer makeSerial = new XmlSerializer(typeof(NotesStructure));
        StreamReader reader = new StreamReader(path);

        NotesStructure nt = (NotesStructure)makeSerial.Deserialize(reader);

        return nt;
    }
    void Start()
    {
        //CreateXML("D:\\Five\\Assets\\Resources\\Notes3.xml");
        noteList = LoadXML(Path.Combine(Application.dataPath, "Resources/Notes2.xml"));
        Debug.Log("Notes Loaded, OK.");
        if (noteList.notes[0].pages[0] != null)
        {
            Debug.Log(noteList.notes[0].pages[0]);
        }
    }

    void Update()
    {
        
    }
}
