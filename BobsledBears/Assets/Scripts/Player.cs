using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool isPlayer1 = false;

    bool movingLeft = false;
    bool movingRight = false;
    float newZ = 0;
   
    public int currentLane = 0;
    int prevLane = 0;
    bool betweenLanes = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        DetermineLane(transform.position.z);
        if ((isPlayer1 && InputBridge.Left) || movingLeft)
        {
            MoveLeft();
        }
        if ((isPlayer1 && InputBridge.Right) || movingRight)
        {
            MoveRight();
        }
    }

    //Keep note of the "Lane" so we can calculate a "perfect run" + score
    public void DetermineLane(float zPos)
    {
        betweenLanes = movingLeft || movingRight;
        float z = zPos;
        prevLane = currentLane;
        if (z >= -150 && z < -100)
        {
            currentLane = -3;
        }
        else if (z >= -100 && z < -50)
        {
            currentLane = -2;
        }
        else if (z >= -50 && z < 0)
        {
            currentLane = -1;
        }
        else if (z >= 0 && z < 50)
        {
            currentLane = 0;
        }
        else if (z >= 50 && z < 100)
        {
            currentLane = 1;
        }
        else if (z >= 100 && z < 150)
        {
            currentLane = 2;
        }
        else if (z >= 150)
        {
            currentLane = 3;
        }
    }

    public void MoveRight()
    {
        if (transform.position.z < 150)
        {
            if (!movingRight)
            {
                movingRight = true;
                newZ = transform.position.z + 50;
            }
            Vector3 newPos = transform.position;
            newPos.z = Mathf.Lerp(newPos.z, newZ, 20 * Time.deltaTime);
            if (newPos.z + 2f >= newZ)
            {
                newPos.z = newZ;
                movingRight = false;
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
        if (transform.position.z > -150)
        {
            if (!movingLeft)
            {
                movingLeft = true;
                newZ = transform.position.z - 50;
            }
            Vector3 newPos = transform.position;
            newPos.z = Mathf.Lerp(newPos.z, newZ, 20 * Time.deltaTime);
            if (newPos.z - 2f <= newZ)
            {
                newPos.z = newZ;
                movingLeft = false;
            }
            transform.position = newPos;
        }
        else
        {
            movingLeft = false;
        }
    }
}
