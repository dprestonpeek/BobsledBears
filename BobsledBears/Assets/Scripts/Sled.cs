using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    [SerializeField]
    Vector3 velocity;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void Slide(Vector3 speed)
    {
        rb.AddForce(speed, ForceMode.VelocityChange);
    }
}
