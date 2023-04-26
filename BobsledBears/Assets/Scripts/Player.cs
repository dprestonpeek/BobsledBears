using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool isPlayer1 = false;

    bool movingLeft = false;
    bool movingRight = false;
    bool adjustLeft = false;
    bool adjustRight = false;
    float leftAdjustment = 0;
    float rightAdjustment = 0;
    float newZ = 0;
    int oneSlot = 50;
   
    public int currentLane = 0;
    int prevLane = -5;
    bool betweenLanes = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        DetermineLane(transform.position.z);
        if (((isPlayer1 && InputBridge.Left) || movingLeft) && !adjustLeft)
        {
            MoveLeft();
        }
        if (((isPlayer1 && InputBridge.Right) || movingRight) && !adjustRight)
        {
            MoveRight();
        }

        if (adjustLeft)
        {
            MoveLeft(leftAdjustment);
        }
        if (adjustRight)
        {
            MoveRight(rightAdjustment);
        }
    }

    //Keep note of the "Lane" so we can calculate a "perfect run" + score
    public void DetermineLane(float zPos)
    {
        betweenLanes = movingLeft || movingRight || adjustLeft || adjustRight;
        Vector3 newPos = transform.position;
        float z = transform.position.z;
        prevLane = currentLane;
        if (!betweenLanes)
        {
            if (z >= -150 && z < -100)
            {
                currentLane = -3;
                if ((z > -150 || z < -150) && z < -125)
                {
                    newPos.z = -150;
                }
                else if (z > -125)
                {
                    newPos.z = -100;
                }
            }
            else if (z >= -100 && z < -50)
            {
                currentLane = -2;
                if ((z > -100 || z < -100) && z < -75)
                {
                    newPos.z = -100;
                }
                else if (z > -75)
                {
                    newPos.z = -50;
                }
            }
            else if (z >= -50 && z < 0)
            {
                currentLane = -1;
                if ((z > -50 || z < -50) && z < -25)
                {
                    newPos.z = -50;
                }
                else if (z > -25)
                {
                    newPos.z = 0;
                }
            }
            else if (z >= 0 && z < 50)
            {
                currentLane = 0;
                if ((z > 0 || z < 0) && z < 25)
                {
                    newPos.z = 0;
                }
                else if (z > 25)
                {
                    newPos.z = 50;
                }
            }
            else if (z >= 50 && z < 100)
            {
                currentLane = 1;
                if ((z > 50 || z < 50) && z < 75)
                {
                    newPos.z = 50;
                }
                else if (z > 75)
                {
                    newPos.z = 100;
                }
            }
            else if (z >= 100 && z < 150)
            {
                currentLane = 2;
                if ((z > 100 || z < 100) && z < 125)
                {
                    newPos.z = 100;
                }
                else if (z > 125)
                {
                    newPos.z = 150;
                }
            }
            else if (z >= 150)
            {
                currentLane = 3;
                if ((z > 150 || z < 150))
                {
                    newPos.z = 150;
                }
            }
        }
        transform.position = newPos;
    }

    public void MoveRight()
    {
        MoveRight(oneSlot);
    }

    public void MoveRight(float amount)
    {
        if (transform.position.z < 150)
        {
            if (!movingRight)
            {
                movingRight = true;
                if (amount != 50)
                {
                    adjustRight = true;
                    rightAdjustment = amount;
                }
                newZ = transform.position.z + amount;
            }
            Vector3 newPos = transform.position;
            newPos.z = Mathf.Lerp(newPos.z, newZ, 20 * Time.deltaTime);
            if (newPos.z + 3f >= newZ)
            {
                newPos.z = newZ;
                movingRight = false;
                adjustRight = false;
            }
            transform.position = newPos;
        }
        else
        {
            movingRight = false;
        }
    }

    public void MoveLeft()
    {
        MoveLeft(oneSlot);
    }

    public void MoveLeft(float amount)
    {
        if (transform.position.z > -150)
        {
            if (!movingLeft)
            {
                movingLeft = true;
                if (amount != 50)
                {
                    adjustLeft = true;
                    leftAdjustment = amount;
                }
                newZ = transform.position.z - amount;
            }
            Vector3 newPos = transform.position;
            newPos.z = Mathf.Lerp(newPos.z, newZ, 20 * Time.deltaTime);
            if (newPos.z - 3f <= newZ)
            {
                newPos.z = newZ;
                movingLeft = false;
                adjustLeft = false;
            }
            transform.position = newPos;
        }
        else
        {
            movingLeft = false;
        }
    }
}
