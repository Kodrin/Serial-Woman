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
    private bool justOpened;
    private bool noteOpen;
    public bool canInteract;
    private int currentLine = 0;
    private int lastLine;
    public CameraController cameraController;
    public List<string> textLines = new List<string>();

    void Start()
    {
        img.enabled = false;
        page.enabled = false;
        title.enabled = false;
        noteOpen = false;
        canInteract = false;

        //if a note object is associated, load all of its pages
        if (targetNote)
        {
            AddWithLineBreaks(targetNote);
            title.text = targetNote.noteName;
        }

        lastLine = textLines.Count;
    }

    void Update()
    {
        ShotType currentShotType = CameraController.Instance.currentCameraShot.shotType;
        if ((targetNote.noteName == "THE THREE BARONS") && (currentShotType == ShotType.GRANDFATHER_SHOT))
            canInteract = true;
        else if ((targetNote.noteName == "HELP!") && (currentShotType == ShotType.NOTE_ON_FLOOR_SHOT))
            canInteract = true;
        else if ((targetNote.noteName == "WALL PLAQUE") && (currentShotType == ShotType.PAINTING_SHOT))
            canInteract = true;
        else
            canInteract = false;

        if (!canInteract) return;

        // Clicking anywhere on the screen should allow us to read through the note if it has already been open
        if (noteOpen)
        {
            // Change page on left click
            if (Input.GetMouseButtonDown(0))
            {
                if (currentLine != lastLine) //check if line is non-null before trying to print text
                {
                    Debug.Log("Next Page.");
                    page.text = textLines[currentLine];
                    page.enabled = true;
                    title.enabled = true;
                    img.enabled = true;
                    currentLine++;
                }
                else
                {
                    Debug.Log("Note Closed.");
                    img.enabled = false;
                    page.enabled = false;
                    title.enabled = false;
                    currentLine = 0;
                    noteOpen = false;
                    cameraController.EnableHotspots();
                    EventHandler.PublishOnNoteOpen(false);
                    //Debug.Log("PUBLISHED NOTE OPEN FALSE");
                }
            }
        }
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
        if (!noteOpen && canInteract)
        {
            Debug.Log("Note Opened.");
            SetNoteProperties();
            if (currentLine != lastLine) //check if line is non-null before trying to print text
            {
                page.text = textLines[currentLine];
                page.enabled = true;
                Debug.Log(targetNote.noteName);
                title.text = targetNote.noteName;
                title.enabled = true;
                img.enabled = true;
                currentLine++;
            }
            justOpened = true;
            EventHandler.PublishOnNoteOpen(true);
            //Debug.Log("PUBLISHED NOTE OPEN TRUE");
        }
    }

    void SetNoteProperties()
    {
        if (targetNote.noteName == "WALL PLAQUE")
        {
            img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250);
            page.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150);
            title.GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);

        }
        else
        {
            img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400);
            page.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            title.GetComponent<RectTransform>().localPosition = new Vector3(0, 180, 0);
        }
    }
    private void OnMouseUp()
    {
        if(justOpened)
        {
            noteOpen = true;
            cameraController.DisableHotspots();
            justOpened = false;
        }
    }
}
