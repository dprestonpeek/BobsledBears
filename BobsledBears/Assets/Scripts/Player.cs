using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int left = -1;
    int right = 1;

    bool movingLeft = false;
    bool movingRight = false;
    float newZ = 0;

   
    public int lane = 0;
    int prevLane = 0;
    bool betweenLanes = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetermineLane();
        if (InputBridge.Left || movingLeft)
        {
            MoveLeft();
        }
        if (InputBridge.Right || movingRight)
        {
            MoveRight();
        }
    }

    void DetermineLane()
    {
        betweenLanes = movingLeft || movingRight;
        float z = transform.position.z;
        prevLane = lane;
        if (z >= -150 && z < -100)
        {
            lane = -3;
        }
        else if (z >= -100 && z < -50)
        {
            lane = -2;
        }
        else if (z >= -50 && z < 0)
        {
            lane = -1;
        }
        else if (z >= 0 && z < 50)
        {
            lane = 0;
        }
        else if (z >= 50 && z < 100)
        {
            lane = 1;
        }
        else if (z >= 100 && z < 150)
        {
            lane = 2;
        }
        else if (z >= 150)
        {
            lane = 3;
        }
    }

    void MoveRight()
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

    void MoveLeft()
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
