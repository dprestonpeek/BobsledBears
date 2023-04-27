using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledManager : MonoBehaviour
{
    [Header("Sled Properties")]
    [SerializeField]
    [Range(1, 1000)]
    int gravity = 500;

    [SerializeField]
    [Range(1, 1000)]
    float defaultSpeed = 250;

    [SerializeField]
    [Range(1, 1000)]
    public int iceSpeed = 800;
    [SerializeField]
    [Range(1, 1000)]
    public int jumpSpeed = 500;


    [Header("Sled References")]
    [SerializeField]
    Sled Blue;
    [SerializeField]
    Sled Red;
    [SerializeField]
    Sled Yellow;
    [SerializeField]
    Sled Green;

    public Vector3 blueVel;
    public Vector3 redVel;
    public Vector3 YellowVel;
    public Vector3 GreenVel;

    List<Sled> sleds;
    List<Rigidbody> sledRbs;

    // Start is called before the first frame update
    void Start()
    {
        sleds = new List<Sled>() { Blue, Red, Yellow, Green };

    }

    // Update is called once per frame
    void Update()
    {
        blueVel = Blue.velocity;
        redVel = Red.velocity;
        YellowVel = Yellow.velocity;
        GreenVel = Green.velocity;
        Physics.gravity = Vector3.down * gravity;
        foreach (Sled sled in sleds)
        {
            sled.SetSpeed(defaultSpeed);
            sled.iceSpeed = iceSpeed;
            sled.jumpSpeed = jumpSpeed;
        }
    }
}
