using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBridge : MonoBehaviour
{
    public static InputBridge Instance;
    public static bool Left { get; private set; }
    public static bool Right { get; private set; }

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
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && count == 0)
        {
            Left = true;
        }
        else
        {
            Left = false;
            count = 0;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && count == 0)
        {
            Right = true;
        }
        else
        {
            Right = false;
            count = 0;
        }
    }
}
