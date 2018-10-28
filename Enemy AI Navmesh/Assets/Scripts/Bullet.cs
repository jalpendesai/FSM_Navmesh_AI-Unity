using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.transform.forward * 50;
        StartCoroutine(SpawnTime());
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }
}
