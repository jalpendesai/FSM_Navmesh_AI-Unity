using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    //Public

    public Transform PlayerDirection;

    public Text GameFinish;

    // Use this for initialization
    void Start () {
        GameFinish.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(this.PlayerDirection.position, this.PlayerDirection.forward, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Finish"))

            {

                GameFinish.gameObject.SetActive(true);

            }

            else

            {

                GameFinish.gameObject.SetActive(false);

            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(this.PlayerDirection.position, this.PlayerDirection.forward, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Finish"))

                {
                    SceneManager.LoadScene("Main Scene");
                }
            }
        }

        }
}
