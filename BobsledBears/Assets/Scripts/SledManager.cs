using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledManager : MonoBehaviour
{
    [Header("Sled Properties")]
    [SerializeField]
    [Range(1, 10000)]
    float speed = 5f;

    [Header("Sled References")]
    [SerializeField]
    Sled Blue;
    [SerializeField]
    Sled Red;
    [SerializeField]
    Sled Yellow;
    [SerializeField]
    Sled Green;

    List<Sled> sleds;
    List<Rigidbody> sledRbs;

    // Start is called before the first frame update
    void Start()
    {
        sleds = new List<Sled>() { Blue, Red, Yellow, Green };
        //sledRbs = new List<Rigidbody>();
        foreach (Sled sled in sleds)
        {
            //Rigidbody sledRb = sled.gameObject.GetComponent<Rigidbody>();
            //sledRb.useGravity = false;
            //sledRb.isKinematic = true;
            //sledRb.velocity = speed * Vector3.forward;
            //sledRbs.Add(sledRb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Sled sled in sleds)
        {
            //sled.Slide(speed * Vector3.forward);
            //sledRb.velocity = speed * Vector3.forward;
        }
    }
}
