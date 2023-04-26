using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject objToFollow;

    bool follow = true;

    Vector3 objStartPos;
    Vector3 adjObjPos;
    Vector3 camStartPos;

    // Start is called before the first frame update
    void Start()
    {
        if (objToFollow == null)
        {
            Destroy(this);
        }
        if (follow)
        {
            objStartPos = objToFollow.transform.position;
            camStartPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            //Get the 0'd out or "adjusted" object position
            adjObjPos = objToFollow.transform.position - objStartPos;

            //Set the camera position to the 0'd out obj position + the camera's initial position
            transform.position = adjObjPos + camStartPos;
        }
    }

    public void StopFollowing()
    {
        follow = false;
    }
}
