using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : Puzzle
{
    [System.Serializable]
    public class PaintingSlot
    {
        public int positionID;
        public Vector3 position;
        public GameObject paintingObj;
    }
    
    // public bool canInteract = true;

    public Painting firstSelection;
    public Painting secondSelection;
    public float swapTime = 1.0f;
    
    public List<PaintingType> solveSequence = new List<PaintingType>();
    public List<Painting> paintings = new List<Painting>();
    
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(!canInteract) return;
            
        if(Input.GetMouseButtonDown(0))
            Controls();
    }

    protected override void Controls()
    {
        Painting selection = GetPaintingWithMouse();

        //if its not null and thereis no first selection
        if (selection && firstSelection == null)
        {
            firstSelection = selection; //make current selection equal to first selection
        }
        else if(selection && firstSelection && secondSelection == null && selection != firstSelection)
        {
            secondSelection = selection;
        }

        //if both selections are full, swap them 
        if (firstSelection && secondSelection)
        {
            Swap();
        }

    }

    protected Painting GetPaintingWithMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            GameObject hitObj = hit.transform.gameObject;
            
            //if it has a component painting, return 
            if (hitObj.TryGetComponent(out Painting component))
            {
                Debug.Log(component.paintingType);
                return component;
            }
        }

        //else return null
        return null;
    }

    protected void Swap()
    {
        //if both of them are not null
        //swap their position
        Vector3 tempPos = firstSelection.transform.position;
        // firstSelection.transform.position = secondSelection.transform.position;
        // secondSelection.transform.position = temp;
        
        //swap their index in the painting list
        int firstIndex = paintings.IndexOf(firstSelection);
        int secondIndex = paintings.IndexOf(secondSelection);
        paintings[firstIndex] = secondSelection;
        paintings[secondIndex] = firstSelection;
        

        StartCoroutine(WaitForSwap(firstSelection, firstSelection.transform.position, secondSelection.transform.position));
        StartCoroutine(WaitForSwap(secondSelection, secondSelection.transform.position, tempPos));

        //then clear the selection to be able to select some more
        ClearSelection();
        
        //and check the solve condition
        CheckSolveCondition();
    }
    

    protected override void CheckSolveCondition()
    {
        //make sure that solve sequence matches the amount of paintings
        if (solveSequence.Count == paintings.Count)
        {
            
            for (int i = 0; i < paintings.Count; i++)
            {
                // if current painting matches solve index
                if (paintings[i].paintingType == solveSequence[i])
                {
                    // continue;
                }
                else
                {
                    return;
                }
            }
        }
        
        //if it didn't return, that means it was solved so mark as solved
        solved = true;
        ResolveState();
    }

    protected override void ResolveState()
    {
        Debug.Log("Painting is Solved!");
    }

    protected IEnumerator WaitForSwap(Painting paintingObj, Vector3 originalPos, Vector3 targetPos)
    {
        canInteract = false; //make sure there is no interaction whilte they switch
        
        float elapsedTime = 0;
        while (elapsedTime < swapTime)
        {
            paintingObj.transform.position = Vector3.Slerp(originalPos, targetPos, (elapsedTime / swapTime));
            elapsedTime += Time.deltaTime;
 
            // Yield here
            yield return null;
        }  
        
        // Make sure we got there
        paintingObj.transform.position = targetPos; //make sure it snaps to that position
        canInteract = true; //re-enable interaction
        yield return null;
    }

    protected void ClearSelection()
    {
        firstSelection = null;
        secondSelection = null;
    }
}
