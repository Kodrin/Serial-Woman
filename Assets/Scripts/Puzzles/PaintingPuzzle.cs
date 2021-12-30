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
    public List<Painting> paintings = new List<Painting>();
    public Painting demon;
    public Painting horse;
    public Painting sleep;
    public bool baronsSmallSolved = false;
    public bool baronsMiddleSolved = false;
    public bool baronsLongSolved = false;
    public bool baronSolved = false;
    public bool rotationSolved = false;

    //public Texture ladyDog;
    public Material ladyDog; 

    // Update is called once per frame
    protected override void Update()
    {
        if(!canInteract || noteOpen) return;
            
        if(Input.GetMouseButtonDown(0))
            Controls();
    }
    public override void Subscribe()
    {
        EventHandler.OnSmallArmMove += CheckSmallArm;
        EventHandler.OnMiddleArmMove += CheckMiddleArm;
        EventHandler.OnLongArmMove += CheckLongArm;
        EventHandler.OnNoteOpen += DetectNoteOpen;
    }
    public override void Unsubscribe()
    {
        EventHandler.OnSmallArmMove -= CheckSmallArm;
        EventHandler.OnMiddleArmMove -= CheckMiddleArm;
        EventHandler.OnLongArmMove -= CheckLongArm;
        EventHandler.OnNoteOpen -= DetectNoteOpen;
    }

    public void CheckSmallArm(int smallArmPosition)
    {
        if (!solved)
        {
            switch (smallArmPosition)
            {
                case 1:
                case 4:
                case 8:
                    RotateToHeading(sleep, RotationHeading.LEFT);
                    baronsSmallSolved = false;
                    break;
                case 2:
                case 5:
                case 6:
                case 11:
                    RotateToHeading(sleep, RotationHeading.DOWN);
                    baronsSmallSolved = false;
                    break;
                case 3:
                    RotateToHeading(sleep, RotationHeading.LEFT);
                    baronsSmallSolved = true;
                    CheckBaronSolved();
                    break;
                case 9:
                    RotateToHeading(sleep, RotationHeading.RIGHT);
                    baronsSmallSolved = false;
                    break;
                case 7:
                case 10:
                case 12:
                    RotateToHeading(sleep, RotationHeading.UP);
                    baronsSmallSolved = false;
                    break;
            }
        }
    }

    public void CheckMiddleArm(int middleArmPosition)
    {
        if (!solved)
        {
            switch (middleArmPosition)
            {
                case 2:
                case 6:
                case 9:
                    RotateToHeading(horse, RotationHeading.UP);
                    baronsMiddleSolved = false;
                    break;
                case 1:
                case 12:
                case 4:
                case 10:
                    RotateToHeading(horse, RotationHeading.LEFT);
                    baronsMiddleSolved = false;
                    break;
                case 3:
                case 5:
                case 8:
                    RotateToHeading(horse, RotationHeading.RIGHT);
                    baronsMiddleSolved = false;
                    break;
                case 11:
                    RotateToHeading(horse, RotationHeading.DOWN);
                    baronsMiddleSolved = false;
                    break;
                case 7:
                    RotateToHeading(horse, RotationHeading.UP);
                    baronsMiddleSolved = true;
                    CheckBaronSolved();
                    break;
            }
        }
    }

    public void CheckLongArm(int longArmPosition)
    {
        if (!solved)
        {
            switch (longArmPosition)
            {
                case 1:
                case 3:
                case 6:
                case 9:
                    RotateToHeading(demon, RotationHeading.LEFT);
                    baronsLongSolved = false;
                    Debug.Log("Demon has Rotation " + paintings[2].currentRotation);
                    break;
                case 5:
                case 7:
                case 10:
                    RotateToHeading(demon, RotationHeading.RIGHT);
                    baronsLongSolved = false;
                    Debug.Log("Demon has Rotation " + paintings[2].currentRotation);
                    break;
                case 8:
                case 4:
                case 12:
                    RotateToHeading(demon, RotationHeading.DOWN);
                    baronsLongSolved = false;
                    Debug.Log("Demon has Rotation " + paintings[2].currentRotation);
                    break;
                case 11:
                    RotateToHeading(demon, RotationHeading.LEFT);
                    baronsLongSolved = true;
                    CheckBaronSolved();
                    Debug.Log("Demon has Rotation " + paintings[2].currentRotation);
                    break;
                case 2:
                    RotateToHeading(demon, RotationHeading.UP);
                    baronsLongSolved = false;
                    Debug.Log("Demon has Rotation " + paintings[2].currentRotation);
                    break;
            }
        }
    }

    private void CheckBaronSolved()
    {
        if (baronsSmallSolved && baronsMiddleSolved && baronsLongSolved)
        {
            paintings[3].paintingType = PaintingType.DOG;
            //paintings[3].SetTexture(ladyDog);
            Material[] allMaterials = paintings[3].GetComponent<Renderer>().materials;
            allMaterials[1] = ladyDog;
            baronSolved = true;
            paintings[3].GetComponent<Renderer>().materials = allMaterials;
            EventHandler.PublishOnBaronSolve();
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
                p.setHeading(desiredhHeading);
                if (desiredhHeading == RotationHeading.RIGHT)
                    QuickRotate(p, 90);

                else if (desiredhHeading == RotationHeading.DOWN)
                    QuickRotate(p, 180);

                else if (desiredhHeading == RotationHeading.LEFT)
                    QuickRotate(p, 270);
            }
            else if (p.currentRotation == RotationHeading.RIGHT)
            {
                p.setHeading(desiredhHeading);
                if (desiredhHeading == RotationHeading.DOWN)
                    QuickRotate(p, 90);

                else if (desiredhHeading == RotationHeading.LEFT)
                    QuickRotate(p, 180);

                else if (desiredhHeading == RotationHeading.UP)
                    QuickRotate(p, 270);
            }
            else if (p.currentRotation == RotationHeading.DOWN) 
            {
                p.setHeading(desiredhHeading);
                if (desiredhHeading == RotationHeading.LEFT)
                    QuickRotate(p, 90);

                else if (desiredhHeading == RotationHeading.UP)
                    QuickRotate(p, 180);

                else if (desiredhHeading == RotationHeading.RIGHT)
                    QuickRotate(p, 270);
            }
            else if (p.currentRotation == RotationHeading.LEFT)
            {
                p.setHeading(desiredhHeading);
                if (desiredhHeading == RotationHeading.UP)
                    QuickRotate(p, 90);

                else if (desiredhHeading == RotationHeading.RIGHT)
                    QuickRotate(p, 180);

                else if (desiredhHeading == RotationHeading.DOWN)
                    QuickRotate(p, 270);
            }
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
        if ((solveSequence.Count == paintings.Count) && rotationSolved)
        {
            for (int i = 0; i < paintings.Count; i++)
            {
                // if current painting matches solve index and current rotation matches solve index
                if (paintings[i].paintingType == solveSequence[i])
                {
                    // continue;
                }
                else
                {
                    Debug.Log(paintings[i].paintingType + " does not match " + solveSequence[i]);
                    return;
                }
            }

            //if it didn't return, that means it was solved so mark as solved
            solved = true;
            ResolveState();
        }
    }

    protected void CheckRotationCondition()
    {
        if ((solveSequence.Count == paintings.Count) && baronSolved)
        {

            for (int i = 0; i < paintings.Count; i++)
            {
                // if current painting matches solve index and current rotation matches solve index
                if (paintings[i].currentRotation == paintings[i].targetRotation)
                {
                    // continue;
                }
                else
                {
                    Debug.Log(paintings[i].paintingType + " is " + paintings[i].currentRotation + " and must be " + paintings[i].targetRotation);
                    return;
                }
            }
            rotationSolved = true;
            CheckSolveCondition();
        }
    }

    protected override void ResolveState()
    {
        Debug.Log("Painting is Solved!");
        canInteract = false;
        for (int i = 0; i < paintings.Count; i++)
        {
            paintings[i].isInteractable = false;
        }
        EventHandler.PublishOnPaintingSolve();

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

    protected void QuickRotate(Painting paintingObj, int angle)
    {
        Vector3 targetEuler = new Vector3(paintingObj.transform.eulerAngles.x, paintingObj.transform.eulerAngles.y, paintingObj.transform.eulerAngles.z + angle);
        paintingObj.transform.eulerAngles = targetEuler; //snap to desired rotation
        CheckRotationCondition();
        CheckSolveCondition();
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

    void DetectNoteOpen(bool isOpen)
    {
        noteOpen = isOpen;
        //Debug.Log("PUZZLE " + isOpen);
    }
}
