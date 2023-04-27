using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    public string Name = "Sled";

    public bool onIceStrip = false;
    bool exitedIceStrip = false;

    public bool onJump = false;
    bool exitedJump = false;

    public float currentMaxSpeed = 250;
    float defaultSpeed = 250;

    [HideInInspector]
    public int iceSpeed = 800;
    [HideInInspector]
    public int jumpSpeed = 500;

    [SerializeField]
    public Vector3 velocity;

    Rigidbody rb;
    Player player;
    Bot bot;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        player = GetComponent<Player>();
        bot = GetComponent<Bot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Slide(currentMaxSpeed);
        //ClampRotation();
        if (exitedIceStrip)
        {
            PostIceStrip();
        }
        else
        {
            exitedIceStrip = false;
        }
        if (exitedJump)
        {
            PostJump();
        }
        else
        {
            exitedJump = false;
        }
    }

    public void Slide(float speed)
    {
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), Mathf.Clamp(rb.velocity.y, -speed, speed), Mathf.Clamp(rb.velocity.z, -speed, speed));
        velocity = rb.velocity;
    }

    public void SetSpeed(float defaultSpeed)
    {
        this.defaultSpeed = defaultSpeed;

        if (onIceStrip)
        {
            exitedIceStrip = false;
            exitedJump = false;
            currentMaxSpeed = defaultSpeed + iceSpeed;
            //rb.AddForce(Vector3.forward * 100, ForceMode.Acceleration);
        }
        else if (exitedIceStrip)
        {
            PostIceStrip();
        }
        if (onJump)
        {
            exitedIceStrip = false;
            exitedJump = false;
            currentMaxSpeed = defaultSpeed + jumpSpeed;
            //rb.AddForce(Vector3.forward * 10000, ForceMode.Impulse);
        }
        else if (exitedJump)
        { 
            PostJump(); 
        }

        if (!onIceStrip && !onJump && !exitedIceStrip && !exitedJump)
        {
            currentMaxSpeed = defaultSpeed;
        }
    }

    public void PostIceStrip()
    {
        onIceStrip = false;
        exitedIceStrip = true;
        //currentMaxSpeed = Mathf.Lerp(currentMaxSpeed, defaultSpeed, .001f * Time.deltaTime);
        if (currentMaxSpeed > defaultSpeed)
        {
            currentMaxSpeed -= 5;
        }
        else if (currentMaxSpeed == defaultSpeed)
        {
            exitedIceStrip = false;
        }
    }

    public void PostJump()
    {
        onJump = false;
        exitedJump = true;

        if (currentMaxSpeed > defaultSpeed)
        {
            currentMaxSpeed -= 5;
        }
        else if (currentMaxSpeed == defaultSpeed)
        {
            exitedJump = false;
        }
    }

    public void SetDifficulty(float diff)
    {
        GetComponent<Bot>().SetDifficulty(diff);
    }

    public void ToggleBotControl()
    {
        if (player.enabled && player.isPlayer1)
        {
            player.enabled = false;
            bot.enabled = true;
            return;
        }
        if (bot.enabled)
        {
            player.enabled = true;
            bot.enabled = false;
        }
    }
}
