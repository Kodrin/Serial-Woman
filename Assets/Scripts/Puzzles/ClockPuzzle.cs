using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClockPuzzle : MonoBehaviour
{
    [System.Serializable]
    public enum MoveDirection
    {
        LEFT,
        RIGHT
    }
    
    [System.Serializable]
    public class ClockArm
    {
        public GameObject armObject;
        public uint currentPosition;
        public float currentRotation;
    }
    
    [SerializeField] protected ClockArm currentSelectedArm;
    [SerializeField] protected ClockArm shortArm;
    [SerializeField] protected ClockArm longArm;
    
    
    [SerializeField] protected float moveAngleIncrement = 30.0f;

    [Header("Solve Condition")] 
    [Range(1, 12)] public uint targetShortArmPos = 1; 
    [Range(1, 12)] public uint targetLongArmPos = 1; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClockControls();
    }

    protected void ClockControls()
    {
        if(Input.GetKeyDown(KeyCode.A))
            MoveArmLeft();
        if(Input.GetKeyDown(KeyCode.D))
            MoveArmRight();
        
        if(Input.GetKeyDown(KeyCode.E))
            SwitchArm();
        
        
    }

    protected void RotateArm(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.LEFT:
                currentSelectedArm.armObject.transform.Rotate(-moveAngleIncrement, 0,0);
                break;
                
            case MoveDirection.RIGHT:
                currentSelectedArm.armObject.transform.Rotate(moveAngleIncrement, 0,0);
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
        if (currentSelectedArm == shortArm)
        {
            currentSelectedArm = longArm;
        }
        else
        {
            currentSelectedArm = shortArm;
        }
    }

    protected void Resolve()
    {
        Debug.Log("CLOCK PUZZLE IS SOLVED!!");
    }

    protected void CheckSolveCondition()
    {
        if (shortArm.currentPosition + 1 == targetShortArmPos && longArm.currentPosition + 1 == targetLongArmPos)
        {
            Resolve();
        }
    }
}
