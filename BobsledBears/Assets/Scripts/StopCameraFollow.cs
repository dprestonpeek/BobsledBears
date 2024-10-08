using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraFollow : MonoBehaviour
{
    [SerializeField]
    CameraFollow camera;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player && player.isPlayer1)
        {
            camera.StopFollowing();
        }
    }
}
