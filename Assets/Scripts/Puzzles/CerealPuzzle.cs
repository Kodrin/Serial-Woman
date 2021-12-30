using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealPuzzle : Puzzle, ISubscribe
{
    public List<CerealContainer> containers = new List<CerealContainer>();
    public Bowl bowl;
    public MilkSpill milkSpill;
    public bool spillAll;
    public GameObject welcomeNote;

    protected override void Start()
    {
        foreach (CerealContainer c in containers)
        {
            c.gameObject.SetActive(false);
        }
    }

    protected override void Update()
    {
        //disable puzzle if a note is open
        if (!canInteract || noteOpen) return;
        if (spillAll)
        {
            SpillAll();
            spillAll = false;
        }
    }

    public override void Subscribe()
    {
        EventHandler.OnPaintingSolve += SpillAll;
        EventHandler.OnNoteOpen += DetectNoteOpen;
    }
    public override void Unsubscribe()
    {
        EventHandler.OnPaintingSolve -= SpillAll;
        EventHandler.OnNoteOpen -= DetectNoteOpen;
    }

    protected override void CheckSolveCondition()
    {
        bool temp = true; //assume you won
        foreach(CerealContainer c in containers)
        {
            //if one of the containers does not have a matching cereal bit, you did not win
            if (c.getMatch() == false)
                temp = false;
        }
        if (temp == true)
        {
            solved = temp;
            ResolveState();
        }
    }

    protected override void ResolveState()
    {
        this.canInteract = false;
        StartCoroutine(DelaySwap(0, 3, 0.0f));
        StartCoroutine(DelaySwap(1, 5, 1.0f));
        StartCoroutine(DelaySwap(2, 7, 2.0f));
        StartCoroutine(DelaySwap(5, 6, 3.0f));
        StartCoroutine(DelaySwap(7, 8, 4.0f));
        EventHandler.PublishOnCerealSolve();
    }

    IEnumerator DelaySwap(int a, int b, float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        containers[a].SwapCereal(containers[b].getPiece());
    }

    public void CheckWin()
    {
        CheckSolveCondition();
    }

    void SpillAll()
    {
        welcomeNote.SetActive(false);
        foreach (CerealContainer c in containers)
        {
            c.gameObject.SetActive(true);
        }
        bowl.Spill();
        milkSpill.Spill();
    }

    void DetectNoteOpen(bool isOpen)
    {
        noteOpen = isOpen;
        //Debug.Log("PUZZLE " + isOpen);
    }
}
