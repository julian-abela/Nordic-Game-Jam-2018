using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour {

    public float speed = 100f;

    private Rigidbody rb;
    private bool fired;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fired = true;
            Debug.Log("FIRE");
        }

        if (fired)
        {
            rb.AddForce(transform.forward * speed);
        }
	}

    public void DriveTruck() {
    }
}
