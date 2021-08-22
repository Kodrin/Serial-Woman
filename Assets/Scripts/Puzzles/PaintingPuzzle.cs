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

    public Texture ladyDog;

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
            case 1:
            case 4:
                RotateToHeading(paintings[0], RotationHeading.LEFT);
                baronsSmallSolved = false;
                break;
            case 2:
            case 5:
            case 6:
                RotateToHeading(paintings[0], RotationHeading.DOWN);
                baronsSmallSolved = false;
                break;
            case 3:
                RotateToHeading(paintings[0], RotationHeading.RIGHT);
                baronsSmallSolved = true;
                CheckBaronSolved();
                break;
            case 7:
            case 10:
            case 12:
                RotateToHeading(paintings[0], RotationHeading.UP);
                baronsSmallSolved = false;
                break;
            case 8:
            case 9:
            case 11:
                RotateToHeading(paintings[0], RotationHeading.UP);
                baronsSmallSolved = false;
                break;
        }
    }

    public void CheckMiddleArm(int middleArmPosition)
    {
        switch (middleArmPosition)
        {
            case 2:
            case 6:
                RotateToHeading(paintings[1], RotationHeading.UP);
                baronsMiddleSolved = false;
                break;
            case 1:
            case 4:
                RotateToHeading(paintings[1], RotationHeading.LEFT);
                baronsMiddleSolved = false;
                break;
            case 3:
            case 5:
                RotateToHeading(paintings[1], RotationHeading.RIGHT);
                baronsMiddleSolved = false;
                break;
            case 8:
            case 11:
            case 12:
                RotateToHeading(paintings[1], RotationHeading.DOWN);
                baronsMiddleSolved = false;
                break;
            case 7:
                RotateToHeading(paintings[1], RotationHeading.UP);
                baronsMiddleSolved = true;
                CheckBaronSolved();
                break;
            case 9:
            case 10:
                RotateToHeading(paintings[1], RotationHeading.RIGHT);
                baronsMiddleSolved = false;
                break;
        }
    }

    public void CheckLongArm(int longArmPosition)
    {
        switch (longArmPosition)
        {
            case 1:
            case 3:
                RotateToHeading(paintings[2], RotationHeading.DOWN);
                baronsLongSolved = false;
                break;
            case 5:
            case 7:
                RotateToHeading(paintings[2], RotationHeading.RIGHT);
                baronsLongSolved = false;
                break;
            case 2:
            case 4:
                RotateToHeading(paintings[2], RotationHeading.DOWN);
                baronsLongSolved = false;
                break;
            case 6:
            case 10:
                RotateToHeading(paintings[2], RotationHeading.UP);
                baronsLongSolved = false;
                break;
            case 11:
                RotateToHeading(paintings[2], RotationHeading.LEFT);
                baronsLongSolved = true;
                CheckBaronSolved();
                break;
            case 8:
            case 9:
            case 12:
                RotateToHeading(paintings[2], RotationHeading.UP);
                baronsLongSolved = false;
                break;
        }
    }

    private void CheckBaronSolved()
    {
        if (baronsSmallSolved && baronsMiddleSolved && baronsLongSolved)
        {
            //Set texture for cat.
            paintings[3].paintingType = PaintingType.DOG;
            paintings[3].SetTexture(ladyDog);
        }
    }
    protected override void Controls()
    {
        Painting selection = GetPaintingWithMouse();

        //if its not null and there is no first selection
        if (selection && firstSelection == null)
        {
            firstSelection = selection; //make current selection equal to first selection
        }

        //DEPRECATED ROTATE LOGIC
        //else if (selection && firstSelection && secondSelection == null)

        //make sure that the second selection is not the same as the first
        else if((selection && firstSelection && secondSelection == null) && (selection != firstSelection))
        {
            secondSelection = selection;
        }

        //if both selections are full, swap them 
        if (firstSelection && secondSelection)
        {
            //DEPRECATED ROTATE LOGIC
            /*
            if (firstSelection == secondSelection)
            {
                Rotate();
            }
            else
            {
                Swap();
            }*/
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
                //if the painting is not of the interactable kind (not part of the puzzle) then the click should not count as a selection
                if (component.isInteractable == false)
                    return null; 

                Debug.Log(component.paintingType);
                return component;
            }
        }

        //else return null
        return null;
    }

    /* DEPRECATED ROTATE LOGIC
    protected void Rotate()
    {
        Debug.Log("Rotate");
        StartCoroutine(WaitForRotate(firstSelection,90));
        ChangeHeading(firstSelection);

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        //then clear the selection to be able to select some more
        ClearSelection();

        //and check the solve condition
        CheckSolveCondition();
    }*/

    protected void RotateToHeading(Painting p, RotationHeading desiredhHeading)
    {
        if(p.currentRotation != desiredhHeading)
        {
            if(p.currentRotation == RotationHeading.UP)
            {
                if (desiredhHeading == RotationHeading.RIGHT)
                    StartCoroutine(WaitForRotate(p, 90));

                else if (desiredhHeading == RotationHeading.DOWN)
                    StartCoroutine(WaitForRotate(p, 180));

                else if (desiredhHeading == RotationHeading.LEFT)
                    StartCoroutine(WaitForRotate(p, 270));
            }
            else if (p.currentRotation == RotationHeading.RIGHT)
            {
                if (desiredhHeading == RotationHeading.DOWN)
                    StartCoroutine(WaitForRotate(p, 90));

                else if (desiredhHeading == RotationHeading.LEFT)
                    StartCoroutine(WaitForRotate(p, 180));

                else if (desiredhHeading == RotationHeading.UP)
                    StartCoroutine(WaitForRotate(p, 270));
            }
            else if (p.currentRotation == RotationHeading.DOWN) 
            {
                if (desiredhHeading == RotationHeading.LEFT)
                    StartCoroutine(WaitForRotate(p, 90));

                else if (desiredhHeading == RotationHeading.UP)
                    StartCoroutine(WaitForRotate(p, 180));

                else if (desiredhHeading == RotationHeading.RIGHT)
                    StartCoroutine(WaitForRotate(p, 270));
            }
            else if (p.currentRotation == RotationHeading.LEFT)
            {
                if (desiredhHeading == RotationHeading.UP)
                    StartCoroutine(WaitForRotate(p, 90));

                else if (desiredhHeading == RotationHeading.RIGHT)
                    StartCoroutine(WaitForRotate(p, 180));

                else if (desiredhHeading == RotationHeading.DOWN)
                    StartCoroutine(WaitForRotate(p, 270));
            }

            p.currentRotation = desiredhHeading;
        }
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

    protected IEnumerator WaitForRotate(Painting paintingObj, int angle)
    {
        canInteract = false; //make sure there is no interaction whilte the painting rotates

        Quaternion targetRotation = paintingObj.transform.rotation;
        Vector3 targetEuler = new Vector3(paintingObj.transform.eulerAngles.x, paintingObj.transform.eulerAngles.y, paintingObj.transform.eulerAngles.z + angle);
        targetRotation *= Quaternion.AngleAxis(angle, Vector3.forward);
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
