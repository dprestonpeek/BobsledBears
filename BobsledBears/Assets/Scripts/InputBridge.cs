using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBridge : MonoBehaviour
{
    public static InputBridge Instance;
    public static bool Left { get; private set; }
    public static bool Right { get; private set; }

    bool LeftTouch = false;
    bool RightTouch = false;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || LeftTouch) && count == 0)
        {
            Left = true;
            LeftTouch = false;
        }
        else
        {
            Left = false;
            count = 0;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || RightTouch) && count == 0)
        {
            Right = true;
            RightTouch = false;
        }
        else
        {
            Right = false;
            count = 0;
        }
    }

    public void TouchLeft()
    {
        LeftTouch = true;
    }

    public void TouchRight()
    {
        RightTouch = true;
    }
}
