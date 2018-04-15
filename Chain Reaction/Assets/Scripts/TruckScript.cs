using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TruckScript : MonoBehaviour {

    public float speed = 100f;
    public float explosionStrength;
    public float explosionRadius;
    public GameObject explosionPrefab;

    private Rigidbody rb;
    private bool fired;
    private bool isOnFloor;


    //Audio
    [EventRef]
    public string audioStart;
    [EventRef]
    public string explosionStart;

    EventInstance eventStart;
    EventInstance explosionEvent;
    ParameterInstance truckDrive;
    ParameterInstance truckCrash;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        eventStart = RuntimeManager.CreateInstance(audioStart);
        explosionEvent = RuntimeManager.CreateInstance(explosionStart);
        RuntimeManager.AttachInstanceToGameObject(eventStart, transform, GetComponent<Rigidbody>());
        eventStart.getParameter("Truck_drive", out truckDrive);
        eventStart.getParameter("Truck_Crash", out truckCrash);
        eventStart.start();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) && isOnFloor)
        {
            if(!fired)
                CanvasScore.instance.NextTurn();
            WeaponController.instance.cameraLocked = false;
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
            StartCoroutine(DestroyTruck());
        }
    }

    IEnumerator DestroyTruck()
    {
        yield return new WaitForSeconds(2);

        explosionEvent.start();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rigidBody = hit.GetComponent<Rigidbody>();

            if (rigidBody != null)
            {
                rigidBody.AddExplosionForce(explosionStrength, explosionPos, explosionRadius);
            }
        }

        var particle = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(particle, 1f);
        Destroy(transform.root.gameObject, 0.05f);

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        eventStart.stop(STOP_MODE.IMMEDIATE);
    }
}
