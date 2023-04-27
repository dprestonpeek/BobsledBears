using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    [SerializeField]
    [Range(0, 1000)]
    int detectDist = 250;

    bool choseADir = false;
    int attemptsToMove = 0;

    bool avoidObstacle = false;
    bool hitObstacle = false;
    bool touchingObstacle = false;
    bool hitBoost = false;
    bool touchingBoost = false;

    public enum Difficulty { EASY, NORMAL, HARD };
    [SerializeField]
    public Difficulty difficulty = Difficulty.EASY;

    enum Direction { LEFT, RIGHT, NONE };
    Direction path = Direction.NONE;

    public override void Update()
    {
        base.Update();

        //if we are not already avoiding an obstacle and detected one
        if (DetectObstacle(transform.position.z) || touchingObstacle)
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

        //if difficulty is hard, choose a boost path if exists, otherwise avoid obstacles
        if (difficulty == Difficulty.HARD)
        {
            if (ScanForBoostLanes().Count > 0)
            {
                ChooseBoostPath();
            }
            else if (DetectObstacle(transform.position.z))
            {
                ChooseOpenPath();
            }
        }
    }

    public void SetDifficulty(float diff)
    {
        switch (diff)
        {
            case 0:
                difficulty = Difficulty.EASY;
                break;
            case 1:
                difficulty = Difficulty.NORMAL;
                break;
            case 2:
                difficulty = Difficulty.HARD;
                break;
        }
    }

    bool DetectObstacle(float zPos)
    {
        bool detectedObstacle = false;
        string tag = DetectObstacleTag(zPos);

        //is the object an obstacle?
        if (tag.Equals("Obstacle"))
        {
            detectedObstacle = true;
        }
        return detectedObstacle;
    }

    bool DetectBoost(float zPos)
    {
        bool detectedBoost = false;
        string tag = DetectObstacleTag(zPos);

        //is the object a booster?
        if (tag.Equals("Jump") || tag.Equals("IceStrip"))
        {
            detectedBoost = true;
        }
        return detectedBoost;
    }

    string DetectObstacleTag(float zPos)
    {
        string tag = "";
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
                tag = hit.transform.gameObject.tag;
            }
            else
            {
                if (hitObstacle || attemptsToMove >= 20)
                {
                    //we are no longer hitting the obstacle
                    hitObstacle = false;
                    avoidObstacle = false;

                    attemptsToMove = 0;
                    path = Direction.NONE;
                    choseADir = false;
                }
            }
        }
        return tag;
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

    List<int> ScanForBoostLanes()
    {
        int lane = -3;
        List<int> boostLanes = new List<int>();
        for (int i = -150; i <= 150; i += 50)
        {
            if (DetectBoost(i))
            {
                boostLanes.Add(lane);
            }
            lane++;
        }

        return boostLanes;
    }

    void ChoosePathEasy()
    {
        Invoke("ChooseRandomPath", .35f);
        //ChooseRandomPath();
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

    void ChooseBoostPath()
    {
        List<int> boostLanes = ScanForBoostLanes();
        Direction firstDir = GetRandomDir();
        if (boostLanes.Contains(currentLane))
        {
            return;
        }
        else
        {
            foreach (int lane in boostLanes)
            {
                if (lane < currentLane)
                {
                    firstDir = Direction.LEFT;
                }
                else
                {
                    firstDir = Direction.RIGHT;
                }
            }
        }
        if (firstDir == Direction.LEFT)
        {
            for (int i = 1; i < 6; i++)
            {
                if (boostLanes.Contains(currentLane - i))
                {
                    MoveLeft();
                    return;
                }
            }
            for (int i = 1; i < 6; i++)
            {
                if (boostLanes.Contains(currentLane + i))
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
                if (boostLanes.Contains(currentLane + i))
                {
                    MoveRight();
                    return;
                }
            }
            for (int i = 1; i < 6; i++)
            {
                if (boostLanes.Contains(currentLane - i))
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

        if (attemptsToMove < 20)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (path == Direction.LEFT)
        {
            if (currentLane == -3) //we hit the edge, change dir
            {
                path = Direction.RIGHT;
                attemptsToMove++;
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
                attemptsToMove++;
            }
            else
            {
                MoveRight();
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            touchingObstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            touchingObstacle = false;
        }
    }
}
