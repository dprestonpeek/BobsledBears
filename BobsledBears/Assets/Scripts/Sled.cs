using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    public string Name = "Sled";

    public bool onIceStrip = false;
    public bool exitedIceStrip = false;

    public float currentMaxSpeed = 250;
    float defaultSpeed = 250;

    [SerializeField]
    Vector3 velocity;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Slide(currentMaxSpeed);
        if (exitedIceStrip)
        {
            PostIceStrip();
        }
    }

    public void Slide(float speed)
    {
        //rb.velocity = (targetEnd.transform.position.normalized - targetStart.transform.position.normalized) * speed;
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), Mathf.Clamp(rb.velocity.y, -speed, speed), Mathf.Clamp(rb.velocity.z, -speed, speed));
        velocity = rb.velocity;
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
    }

    public void SetSpeed(float defaultSpeed)
    {
        this.defaultSpeed = defaultSpeed;
        if (!exitedIceStrip)
        {
            if (onIceStrip)
            {
                currentMaxSpeed = defaultSpeed + 500;
            }
            else
            {
                currentMaxSpeed = defaultSpeed;
            }
        }
    }

    public void PostIceStrip()
    {
        onIceStrip = false;
        exitedIceStrip = true;
        currentMaxSpeed = Mathf.Lerp(currentMaxSpeed, defaultSpeed, 5 * Time.deltaTime);
        if (currentMaxSpeed - .2 <= defaultSpeed)
        {
            currentMaxSpeed = defaultSpeed;
            exitedIceStrip = false;
        }
    }
}
