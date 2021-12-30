using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClockPuzzle : Puzzle, ISubscribe
{
    [System.Serializable]
    public enum MoveDirection
    {
        LEFT,
        RIGHT
    }
    
    [System.Serializable]
    public enum ArmType
    {
        SHORT,
        LONG
    }
    
    [System.Serializable]
    public class ClockArm
    {
        // public ArmType armType;
        public GameObject armObject;
        public int currentPosition;
        // public float currentRotation;

        public void DisableArm()
        {
            armObject.SetActive(false);
        }
    }
    
    [Header("ARMS")]
    [SerializeField] protected ClockArm currentSelectedArm;
    [SerializeField] protected ClockArm shortArm;
    [SerializeField] protected ClockArm middleArm;
    [SerializeField] protected ClockArm longArm;
    public bool armsInteractable = false;

    [SerializeField] protected float moveAngleIncrement = 30.0f;

    [Header("Solve Condition")] 
    [Range(1, 12)] public int targetShortArmPos = 1; 
    [Range(1, 12)] public int targetMiddleArmPos = 1; 
    [Range(1, 12)] public int targetLongArmPos = 1;


    public override void Subscribe()
    {
        EventHandler.OnNoteOpen += DetectNoteOpen;
        EventHandler.OnFloorNoteOpen += StartPuzzle;
        EventHandler.OnPaintingSolve += DisableHands;
    }

    public override void Unsubscribe()
    {
        EventHandler.OnNoteOpen -= DetectNoteOpen;
        EventHandler.OnFloorNoteOpen -= StartPuzzle;
        EventHandler.OnPaintingSolve -= DisableHands;
    }

    protected override void Update()
    {
        if (!canInteract || noteOpen) return;        
        Controls();
    }

    protected override void Controls()
    {
        string selectedHand = "";

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse down.");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You clicked on " + hit.transform.name);
                selectedHand = hit.transform.name;
                if (armsInteractable)
                {
                    SwitchArm(selectedHand);
                    MoveArmRight();
                }
                else
                {
                    EventHandler.PublishOnTextControllerReset();
                    EventHandler.PublishOnTextControllerMsg("I shouldn't mess with the time unless I have a reason to.");
                }
            }
        }
        /* DEPRECATED KEY CONTROLS
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveArmLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveArmRight();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchArm();
        }
        */
    }

    public void StartPuzzle()
    {
        armsInteractable = true;
    }
    public int GetHour()
    {
        return shortArm.currentPosition;
    }
    protected void RotateArm(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.LEFT:
                currentSelectedArm.armObject.transform.Rotate(-moveAngleIncrement, 0, 0);
                currentSelectedArm.currentPosition = UpdateArmPosition(dir, currentSelectedArm.currentPosition);
                UpdateArmParams();
                PublishArmEvents();
                break;

            case MoveDirection.RIGHT:
                currentSelectedArm.armObject.transform.Rotate(moveAngleIncrement, 0, 0);
                currentSelectedArm.currentPosition = UpdateArmPosition(dir, currentSelectedArm.currentPosition);
                UpdateArmParams();
                PublishArmEvents();
                break;

        }
    }

    protected int UpdateArmPosition(MoveDirection dir, int number)
    {
        if (dir == MoveDirection.LEFT)
        {
            if (number - 1 >= 1)
            {
                return number - 1;
            }
            else
            {
                return 12;
            }
        }
        else if (dir == MoveDirection.RIGHT)
        {
            if (number + 1 <= 12)
            {
                return number + 1;
            }
            else
            {
                return 1;
            }
        }

        return 0;
    }

    protected void UpdateArmParams()
    {
        if (currentSelectedArm.armObject == shortArm.armObject)
        {
            shortArm.currentPosition = currentSelectedArm.currentPosition;
        }
        else if (currentSelectedArm.armObject == middleArm.armObject)
        {
            middleArm.currentPosition = currentSelectedArm.currentPosition;
        }
        else if (currentSelectedArm.armObject == longArm.armObject)
        {
            longArm.currentPosition = currentSelectedArm.currentPosition;
        }
    }

    protected void MoveArmLeft()
    {
        RotateArm(MoveDirection.LEFT);
        CheckSolveCondition();
    }

    protected void MoveArmRight()
    {
        RotateArm(MoveDirection.RIGHT);
        CheckSolveCondition();
    }

    protected void SwitchArm()
    {
        if (currentSelectedArm.armObject == shortArm.armObject)
        {
            currentSelectedArm.armObject = middleArm.armObject; //set currrent object 
            shortArm.currentPosition = currentSelectedArm.currentPosition;
            currentSelectedArm.currentPosition = middleArm.currentPosition;
        }
        else if (currentSelectedArm.armObject == middleArm.armObject)
        {
            currentSelectedArm.armObject = longArm.armObject; //set currrent object 
            middleArm.currentPosition = currentSelectedArm.currentPosition;
            currentSelectedArm.currentPosition = longArm.currentPosition;
        }
        else if(currentSelectedArm.armObject == longArm.armObject)
        {
            currentSelectedArm.armObject = shortArm.armObject;
            longArm.currentPosition = currentSelectedArm.currentPosition;
            currentSelectedArm.currentPosition = shortArm.currentPosition;
        }
    }

    protected void SwitchArm(string selectedArm)
    {
        if (selectedArm == "Small_Hand")
        {
            currentSelectedArm.armObject = shortArm.armObject; //set currrent object 
            currentSelectedArm.currentPosition = shortArm.currentPosition;
        }
        else if (selectedArm == "Medium_Hand")
        {
            currentSelectedArm.armObject = middleArm.armObject; //set currrent object 
            currentSelectedArm.currentPosition = middleArm.currentPosition;
        }
        else if (selectedArm == "Large_Hand")
        {
            currentSelectedArm.armObject = longArm.armObject; //set currrent object 
            currentSelectedArm.currentPosition = longArm.currentPosition;
        }
    }

    //Clock Puzzle (Baron Puzzle) logic is in PaintingPuzzle.cs
    /*
    protected override void ResolveState()
    {
        Debug.Log("CLOCK PUZZLE IS SOLVED!!");
    }

    protected override void CheckSolveCondition()
    {
        if (shortArm.currentPosition == targetShortArmPos && 
            middleArm.currentPosition == targetMiddleArmPos && 
            longArm.currentPosition == targetLongArmPos)
        {
            ResolveState();
        }
    }
    */

    //event handling
    protected void PublishArmEvents()
    {
        EventHandler.PublishOnAnyArmMove();
        EventHandler.PublishOnSmallArmMove(shortArm.currentPosition);
        EventHandler.PublishOnMiddleArmMove(middleArm.currentPosition);
        EventHandler.PublishOnLongArmMove(longArm.currentPosition);
    }

    void DetectNoteOpen(bool isOpen)
    {
        noteOpen = isOpen;
        //Debug.Log("PUZZLE " + isOpen);
    }

    void DisableHands()
    {
        shortArm.DisableArm();
        middleArm.DisableArm();
        longArm.DisableArm();
    }
}
