using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : Puzzle, ISubscribe
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
    public List<RotationHeading> solveRotation = new List<RotationHeading>();
    public List<Painting> paintings = new List<Painting>();

    public bool baronsSmallSolved = false;
    public bool baronsMiddleSolved = false;
    public bool baronsLongSolved = false;

    // Update is called once per frame
    protected override void Update()
    {
        if(!canInteract) return;
            
        if(Input.GetMouseButtonDown(0))
            Controls();
    }
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    public void Subscribe()
    {
        EventHandler.OnSmallArmMove += CheckSmallArm;
        EventHandler.OnMiddleArmMove += CheckMiddleArm;
        EventHandler.OnLongArmMove += CheckLongArm;
    }
    public void Unsubscribe()
    {
        EventHandler.OnSmallArmMove -= CheckSmallArm;
        EventHandler.OnMiddleArmMove += CheckMiddleArm;
        EventHandler.OnLongArmMove += CheckLongArm;
    }

    public void CheckSmallArm(int smallArmPosition)
    {
        switch(smallArmPosition)
        {
            //Eye is half open, sun is out
            case 1:
            case 4:
                paintings[0].paintingType=PaintingType.SUNOWL;
                paintings[2].paintingType = PaintingType.EYE;
                baronsSmallSolved = false;
                break;
            //Eye is fully open, sun is out
            case 2:
            case 5:
            case 6:
                paintings[0].paintingType = PaintingType.SUNOWL;
                paintings[2].paintingType = PaintingType.OTHER;
                baronsSmallSolved = false;
                break;
            //Eye is fully open, sun is out and Three Barons small arm is True
            case 3:
                paintings[0].paintingType = PaintingType.SUNOWL;
                paintings[2].paintingType = PaintingType.OTHER;
                baronsSmallSolved = true;
                break;
            //Eye is half open, moon is out
            case 7:
            case 10:
            case 12:
                paintings[0].paintingType = PaintingType.OTHER;
                paintings[2].paintingType = PaintingType.EYE;
                baronsSmallSolved = false;
                break;
            //Eye is fully open, moon is out
            case 8:
            case 9:
            case 11:
                paintings[0].paintingType = PaintingType.OTHER;
                paintings[2].paintingType = PaintingType.OTHER;
                baronsSmallSolved = false;
                break;
        }
    }

    public void CheckMiddleArm(int middleArmPosition)
    {
        if (middleArmPosition == 7)
            baronsMiddleSolved = true;
        else
            baronsMiddleSolved = false;
    }   

    public void CheckLongArm(int longArmPosition)
    {
        if (longArmPosition == 11)
            baronsLongSolved = true;
        else
            baronsLongSolved = false;
    }
    private void CheckBaronSolved()
    {
        if (baronsSmallSolved && baronsMiddleSolved && baronsLongSolved)
        {
            //Set texture for cat.
            paintings[1].paintingType = PaintingType.CAT;
        }
    }
    protected override void Controls()
    {
        Painting selection = GetPaintingWithMouse();

        //if its not null and thereis no first selection
        if (selection && firstSelection == null)
        {
            firstSelection = selection; //make current selection equal to first selection
        }
        else if(selection && firstSelection && secondSelection == null)
        {
            secondSelection = selection;
        }

        //if both selections are full, swap them 
        if (firstSelection && secondSelection)
        {
            if (firstSelection == secondSelection)
            {
                Rotate();
            }
            else
            {
                Swap();
            }
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

    protected void Rotate()
    {
        Debug.Log("Rotate");
        StartCoroutine(WaitForRotate(firstSelection));
        ChangeHeading(firstSelection);

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        //then clear the selection to be able to select some more
        ClearSelection();

        //and check the solve condition
        CheckSolveCondition();
    }
    protected void Swap()
    {
        Debug.Log("Swap");
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
                // if current painting matches solve index and current rotation matches solve index
                if ((paintings[i].paintingType == solveSequence[i]) && (paintings[i].currentRotation == solveRotation[i]))
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

    protected IEnumerator WaitForRotate(Painting paintingObj)
    {
        canInteract = false; //make sure there is no interaction whilte the painting rotates

        Quaternion targetRotation = paintingObj.transform.rotation;
        Vector3 targetEuler = new Vector3(paintingObj.transform.eulerAngles.x, paintingObj.transform.eulerAngles.y, paintingObj.transform.eulerAngles.z + 90);
        targetRotation *= Quaternion.AngleAxis(90, Vector3.forward);
        float elapsedTime = 0;
        while (elapsedTime < swapTime)
        {
            paintingObj.transform.rotation = Quaternion.Lerp(paintingObj.transform.rotation, targetRotation, (elapsedTime / swapTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        paintingObj.transform.eulerAngles = targetEuler; //make sure it snaps to that rotation
        canInteract = true; //re-enable interaction
        yield return null;
    }

    protected void ChangeHeading(Painting paintingObj)
    {
        switch (paintingObj.currentRotation)
        {
            case RotationHeading.UP:
                paintingObj.currentRotation = RotationHeading.RIGHT;
                break;
            case RotationHeading.RIGHT:
                paintingObj.currentRotation = RotationHeading.DOWN;
                break;
            case RotationHeading.DOWN:
                paintingObj.currentRotation = RotationHeading.LEFT;
                break;
            case RotationHeading.LEFT:
                paintingObj.currentRotation = RotationHeading.UP;
                break;
        }
    }

    protected void ClearSelection()
    {
        firstSelection = null;
        secondSelection = null;
    }
}
