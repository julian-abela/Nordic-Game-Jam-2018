using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TruckScript : MonoBehaviour {

    public float speed = 100f;
    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;

    private Rigidbody rb;
    private bool fired;
    private bool isOnFloor;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        eventStart = RuntimeManager.CreateInstance(audioStart);
        eventStart.start();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isOnFloor)
        {
            eventStart.setPitch(1.5f);
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

    private void OnDestroy()
    {
        eventStart.stop(STOP_MODE.IMMEDIATE);
    }
}
