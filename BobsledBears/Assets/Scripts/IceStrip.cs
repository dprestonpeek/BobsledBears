using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStrip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Sled sled = other.GetComponent<Sled>();
        if (sled)
        {
            sled.onIceStrip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Sled sled = other.GetComponent<Sled>();
        if (sled)
        {
            sled.PostIceStrip();
        }
    }
}
