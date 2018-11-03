using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

    // AI ai;
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
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            //if(other.gameObject.tag == "Enemy")
            //{
            //    ai.rotateHead();
            //}
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
