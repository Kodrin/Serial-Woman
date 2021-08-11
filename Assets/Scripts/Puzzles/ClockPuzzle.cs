using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClockPuzzle : Puzzle
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
    }
    
    [Header("ARMS")]
    [SerializeField] protected ClockArm currentSelectedArm;
    [SerializeField] protected ClockArm shortArm;
    [SerializeField] protected ClockArm middleArm;
    [SerializeField] protected ClockArm longArm;
    
    
    [SerializeField] protected float moveAngleIncrement = 30.0f;

    [Header("Solve Condition")] 
    [Range(1, 12)] public int targetShortArmPos = 1; 
    [Range(1, 12)] public int targetMiddleArmPos = 1; 
    [Range(1, 12)] public int targetLongArmPos = 1; 
    


    protected override void Update()
    {
        if (!canInteract) return;        
        Controls();
    }

    protected override void Controls()
    {
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


    }

    protected void RotateArm(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.LEFT:
                currentSelectedArm.armObject.transform.Rotate(-moveAngleIncrement, 0,0);
                currentSelectedArm.currentPosition = UpdateArmPosition(dir, currentSelectedArm.currentPosition);
                UpdateArmParams();
                break;
                
            case MoveDirection.RIGHT:
                currentSelectedArm.armObject.transform.Rotate(moveAngleIncrement, 0,0);
                currentSelectedArm.currentPosition = UpdateArmPosition(dir, currentSelectedArm.currentPosition);
                UpdateArmParams();
                break;
            
        }
    }

    protected void MoveArmToNumber(int number)
    {
        
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
}
