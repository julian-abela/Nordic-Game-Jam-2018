using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TruckScript : MonoBehaviour {

    public float speed = 100f;

    private Rigidbody rb;
    private bool fired;
    private bool isOnFloor;


    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;
    ParameterInstance truckDrive;
    ParameterInstance truckCrash;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        eventStart = RuntimeManager.CreateInstance(audioStart);
        RuntimeManager.AttachInstanceToGameObject(eventStart, transform, GetComponent<Rigidbody>());
        eventStart.getParameter("Truck_drive", out truckDrive);
        eventStart.getParameter("Truck_Crash", out truckCrash);
        eventStart.start();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isOnFloor)
        {
            truckDrive.setValue(1f);
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
        else 
        {
            truckCrash.setValue(1f);
        }
    }

    private void OnDestroy()
    {
        eventStart.stop(STOP_MODE.IMMEDIATE);
    }
}
