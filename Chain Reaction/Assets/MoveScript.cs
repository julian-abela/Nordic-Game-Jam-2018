using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    Rigidbody rigidbody;
    public Vector3 direction;
    public float speed;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.Space))
            rigidbody.AddForce(direction * speed);
    }
}
