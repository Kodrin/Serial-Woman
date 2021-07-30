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

    [System.Serializable]
    public enum LetterType
    {
        A,
        B,
        C,
        D
    }

    // public bool canInteract = true;

    public GameObject firstSelection;
    public GameObject secondSelection;
    public float swapTime = 1.0f;
    
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
        GameObject selection = GetPaintingWithMouse();

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

    protected GameObject GetPaintingWithMouse()
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
                Debug.Log(objectHit.gameObject.name);
                return component.gameObject;
            }
        }

        //else return null
        return null;
    }

    protected void Swap()
    {
        //if both of them are not null
        //swap their position
        Vector3 temp = firstSelection.transform.position;
        // firstSelection.transform.position = secondSelection.transform.position;
        // secondSelection.transform.position = temp;

        StartCoroutine(WaitForSwap(firstSelection, firstSelection.transform.position, secondSelection.transform.position));
        StartCoroutine(WaitForSwap(secondSelection, secondSelection.transform.position, temp));

        //then clear the selection to be able to select some more
        ClearSelection();
    }
    

    protected override void CheckSolveCondition()
    {
        
    }

    protected override void Resolve()
    {
        
    }

    protected IEnumerator WaitForSwap(GameObject paintingObj, Vector3 originalPos, Vector3 targetPos)
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
