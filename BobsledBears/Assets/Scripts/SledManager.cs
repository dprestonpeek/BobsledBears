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

    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = Vector3.down * gravity;
        foreach (Sled sled in sleds)
        {
            sled.SetSpeed(defaultSpeed);
        }
    }
}
