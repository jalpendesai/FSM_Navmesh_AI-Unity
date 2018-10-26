using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour {

    public int speed = 1;

    private float moveHorizontal;
    private float moveVertical;

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        moveVertical = Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Rotate(0, moveHorizontal * speed, 0);
        transform.Translate(0, 0, moveVertical * speed);
    }
}
