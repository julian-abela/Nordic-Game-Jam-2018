using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AnvilScript : MonoBehaviour {
    
    private float fireSpeed = 3f;
    private Rigidbody body;
    private Vector3 leftEnd;
    private Vector3 rightEnd;
    private float range = 10f;
    private float lerpValue = .5f;
    private float inputSensitivity;
    private bool hasBeenUsed;
    private bool oneShot;

    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;

    private void Start()
    {
        inputSensitivity = 1f / range;
        body = GetComponent<Rigidbody>();
        transform.position += new Vector3(0,6f);
        leftEnd = transform.position + transform.right * range;
        rightEnd = transform.position + transform.right * range * -1f;

        eventStart = RuntimeManager.CreateInstance(audioStart);

        oneShot = true;

    }

    void Update ()
    {
        if (hasBeenUsed)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.useGravity = true;
            body.AddForce(Vector3.down * fireSpeed, ForceMode.Force);
            hasBeenUsed = true;
            WeaponController.instance.cameraLocked = false;
        }
        else
        {
            lerpValue += Input.GetAxisRaw("Horizontal") * inputSensitivity;
            lerpValue = Mathf.Max(0, Mathf.Min(lerpValue, 1));
            transform.position = Vector3.Lerp(rightEnd, leftEnd, lerpValue);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (oneShot)
        {
            eventStart.start();
            oneShot = false;
        }

    }

}
