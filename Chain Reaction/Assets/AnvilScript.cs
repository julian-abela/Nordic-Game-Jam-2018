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
    private Vector3 upEnd;
    private Vector3 downEnd;
    private float range = 10f;
    private float horizontalLerpValue = .5f;
    private float verticalLerpValue = 1;
    private float inputSensitivity;
    private bool hasBeenUsed;
    private bool oneShot;

    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;

    private void Start()
    {
        inputSensitivity = 0.1f / range;
        body = GetComponent<Rigidbody>();
        //transform.position += new Vector3(0,6f);
        leftEnd = transform.position + transform.right * range;
        rightEnd = transform.position + transform.right * range * -1f;
        upEnd = transform.position + transform.forward * range;
        downEnd = transform.position + transform.forward * range * -1f;

        eventStart = RuntimeManager.CreateInstance(audioStart);

        oneShot = true;

    }

    void Update ()
    {
        if (hasBeenUsed)
        {
            return;
        }
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)))
        {
            body.useGravity = true;
            body.AddForce(Vector3.down * fireSpeed, ForceMode.Force);
            hasBeenUsed = true;
            WeaponController.instance.cameraLocked = false;
        }
        else
        {
            horizontalLerpValue += Input.GetAxisRaw("Horizontal") * inputSensitivity;
            horizontalLerpValue = Mathf.Max(0, Mathf.Min(horizontalLerpValue, 1));
            Vector3 horizontalPosition = Vector3.Lerp(rightEnd, leftEnd, horizontalLerpValue);

            verticalLerpValue -= Input.GetAxisRaw("Vertical") * inputSensitivity;
            verticalLerpValue = Mathf.Max(0, Mathf.Min(verticalLerpValue, 1));
            Vector3 verticalPosition = Vector3.Lerp(upEnd, downEnd, verticalLerpValue);

            transform.position = (horizontalPosition + verticalPosition) / 2;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (oneShot)
        {
            eventStart.start();
            oneShot = false;
        }

        Destroy(this.gameObject, 2);
    }

}
