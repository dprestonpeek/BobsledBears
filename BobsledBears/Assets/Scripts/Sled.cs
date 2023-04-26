using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    public float maxSpeed;

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
        Slide(maxSpeed);
    }

    public void Slide(float speed)
    {
        //rb.velocity = (targetEnd.transform.position.normalized - targetStart.transform.position.normalized) * speed;
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), Mathf.Clamp(rb.velocity.y, -speed, speed), Mathf.Clamp(rb.velocity.z, -speed, speed));
        velocity = rb.velocity;
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
    }
}
