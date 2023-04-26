using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    [SerializeField]
    [Range(0, 1000)]
    int detectDist = 250;

    bool choseADir = false;
    int attemptsToAvoid = 0;

    bool avoidObstacle = false;
    bool hitObstacle = false;

    enum Difficulty { EASY, NORMAL };
    [SerializeField]
    Difficulty difficulty = Difficulty.EASY;

    enum Direction { LEFT, RIGHT, NONE };
    Direction path = Direction.NONE;

    public override void Update()
    {
        base.Update();

        //if we are not already avoiding an obstacle and detected one
        if (DetectObstacle(transform.position.z))
        {
            avoidObstacle = true;
            switch (difficulty)
            {
                case Difficulty.EASY:
                    ChoosePathEasy();
                    break;
                case Difficulty.NORMAL:
                    ChoosePathNormal();
                    break;
            }
        }
    }

    bool DetectObstacle(float zPos)
    {
        bool detectedObstacle = false;
        //3 different rays to cover the left, right and center of the sled
        for (int i = -20; i < 20; i += 20)
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            RaycastHit hit;
            Vector3 newPos = transform.position;
            newPos.z = zPos;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(newPos, transform.TransformDirection(Vector3.forward * 400), out hit, detectDist, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //is the object an obstacle?
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    detectedObstacle = true;
                }
            }
            else
            {
                if (hitObstacle || attemptsToAvoid >= 20)
                {
                    //we are no longer hitting the obstacle
                    hitObstacle = false;
                    avoidObstacle = false;
                    attemptsToAvoid = 0;
                    path = Direction.NONE;
                    choseADir = false;
                }
            }
        }
        return detectedObstacle;
    }

    List<int> ScanForOpenLanes()
    {
        int lane = -3;
        List<int> openLanes = new List<int>();
        for (int i = -150; i <= 150; i += 50)
        {
            if (!DetectObstacle(i))
            {
                openLanes.Add(lane);
            }
            lane++;
        }

        return openLanes;
    }

    void ChoosePathEasy()
    {
        ChooseRandomPath();
    }

    void ChoosePathNormal()
    {
        ChooseOpenPath();
    }

    void ChooseOpenPath()
    {
        List<int> openLanes = ScanForOpenLanes();
        Direction firstDir = GetRandomDir();
        if (firstDir == Direction.LEFT)
        {
            for (int i = 1; i < 6; i++)
            {
                if (openLanes.Contains(currentLane - i))
                {
                    MoveLeft();
                    return;
                }
            }
            for (int i = 1; i < 6; i++)
            {
                if (openLanes.Contains(currentLane + i))
                {
                    MoveRight();
                    return;
                }
            }
        }
        else
        {
            for (int i = 1; i < 6; i++)
            {
                if (openLanes.Contains(currentLane + i))
                {
                    MoveRight();
                    return;
                }
            }
            for (int i = 1; i < 6; i++)
            {
                if (openLanes.Contains(currentLane - i))
                {
                    MoveLeft();
                    return;
                }
            }
        }
    }

    void ChooseRandomPath()
    {
        //pick a random path on first time thru
        if (!choseADir)
        {
            path = GetRandomDir();
            choseADir = true;
        }

        if (attemptsToAvoid < 20)
        {
            if (path == Direction.LEFT)
            {
                if (currentLane == -3) //we hit the edge, change dir
                {
                    path = Direction.RIGHT;
                    attemptsToAvoid++;
                }
                else
                {
                    MoveLeft();
                }
            }
            if (path == Direction.RIGHT)
            {
                if (currentLane == 3) //we hit the edge, change dir
                {
                    path = Direction.LEFT;
                    attemptsToAvoid++;
                }
                else
                {
                    MoveRight();
                }
            }
        }
    }

    Direction GetRandomDir()
    {
        int dir = GetRandom01();
        if (dir == 0)
        {
            return Direction.LEFT;
        }
        if (dir == 1)
        {
            return Direction.RIGHT;
        }
        return Direction.NONE;
    }

    int GetRandom01()
    {
        return Random.Range(0, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            hitObstacle = true;
        }
    }
}
