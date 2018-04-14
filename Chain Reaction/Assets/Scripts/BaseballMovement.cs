using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BaseballMovement : MonoBehaviour
{
    public float fireSpeed;

    private new Camera camera;
    private bool ballShot;
    private Vector3 target;

    //audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;
    ParameterInstance audioParam;
    
    void Start()
    {
        camera = Camera.main;

        eventStart = RuntimeManager.CreateInstance(audioStart);
        eventStart.getParameter("Baseball", out audioParam);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !ballShot)
        {
            eventStart.start();

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.transform.position - transform.position;
                Rigidbody ballRigid = GetComponent<Rigidbody>();
                ballRigid.useGravity = true;
                ballRigid.AddForce((target + Vector3.up) * fireSpeed, ForceMode.Impulse);

                audioParam.setValue(1f);
            }
        }
    }



    private void OnDestroy()
    {
        eventStart.stop(STOP_MODE.IMMEDIATE);
    }
}
