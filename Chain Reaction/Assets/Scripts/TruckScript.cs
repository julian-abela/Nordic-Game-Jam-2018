using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour {

    public float speed = 100f;

    private Rigidbody rb;
    private bool fired;
    private bool isOnFloor;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isOnFloor)
        {
            fired = true;
        }

        if (fired)
        {
            rb.AddForce(transform.parent.forward * speed);
        }
	}

    public void DriveTruck() {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor")) {
            isOnFloor = true;
        }
    }
}
