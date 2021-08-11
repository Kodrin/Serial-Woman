using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Note", order = 1)]
public class Note : ScriptableObject
{
    [System.Serializable]
    public class Page
    {
        public string content; 
    }

    public string noteName;
    public List<Page> pages = new List<Page>();
}