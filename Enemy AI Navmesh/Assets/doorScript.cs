using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

    GameObject player;
    Vector3 doorClose;
    Vector3 doorOpen = new Vector3(0, 3, 0);
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        doorClose = transform.position;     // Store original position
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Debug.Log("Collision Detected");
            
            transform.position += doorOpen;

            // Close the door in 4 seconds
            StartCoroutine(CloseDoor());
        }
    }

    

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(4.0f);
        transform.position = doorClose;
    }

 
}
