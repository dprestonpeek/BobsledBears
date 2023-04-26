using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
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
        if (other.CompareTag("Sled") && !GameManager.Instance.finished.Contains(other.gameObject))
        {
            GameManager.Instance.AddToScoreboard(other.GetComponent<Sled>());
        }
    }
}
