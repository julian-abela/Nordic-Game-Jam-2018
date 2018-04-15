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
            CanvasScore.instance.NextTurn();
            eventStart.start();

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point - transform.position;
                Rigidbody ballRigid = GetComponent<Rigidbody>();
                ballRigid.useGravity = true;
                ballRigid.AddForce(target.normalized* fireSpeed + Vector3.up * fireSpeed/10, ForceMode.Impulse);
                ballRigid.AddTorque(transform.up * 1000 - transform.right * 1000);

                WeaponController.instance.firingWeapon = true;
                ballShot = true;
                audioParam.setValue(1f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 2);
    }

    private void OnDestroy()
    {
        eventStart.stop(STOP_MODE.IMMEDIATE);
    }
}
