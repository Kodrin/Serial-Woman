using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System;
using UnityEngine;


public class LoadNotes : MonoBehaviour
{
    Notes noteList; 

    void CreateXML(string nPath)
    {
        Notes noteList = new Notes();
        Note threeBarons = new Note();
        threeBarons.AddPage("This is the text for page 1.");
        threeBarons.AddPage("This is the text for page 2.");
        threeBarons.AddPage("This is the text for page 3.");
        threeBarons.noteName = "Three Barons";
        Note wallPlaque = new Note();
        wallPlaque.AddPage("This is the text for page 1.");
        wallPlaque.noteName = "Wall Plaque";
        noteList.AddNote(threeBarons);
        noteList.AddNote(wallPlaque);

        XmlSerializer makeSerial = new XmlSerializer(typeof(Notes));
        StreamWriter writer = new StreamWriter(nPath);
        makeSerial.Serialize(writer, noteList);
    }

    public static Notes LoadXML(string path)
    {
        XmlSerializer makeSerial = new XmlSerializer(typeof(Notes));
        StreamReader reader = new StreamReader(path);

        Notes nt = (Notes)makeSerial.Deserialize(reader);

        return nt;
    }
    void Start()
    {
        //CreateXML("D:\\Five\\Assets\\Resources\\Notes3.xml");
        noteList = LoadXML(Path.Combine(Application.dataPath, "Resources/Notes2.xml"));
        Debug.Log("Loaded, OK.");
        if (noteList.notes[0].pages[0] != null)
        {
            Debug.Log(noteList.notes[0].pages[0]);
        }
    }

    void Update()
    {
        
    }
}
