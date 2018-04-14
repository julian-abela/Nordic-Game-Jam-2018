using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballMovement : MonoBehaviour
{
    public float fireSpeed;

    private new Camera camera;
    private bool ballShot;
    private Vector3 target;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !ballShot)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.transform.position - transform.position;
                Rigidbody ballRigid = GetComponent<Rigidbody>();
                ballRigid.useGravity = true;
                ballRigid.AddForce((target + Vector3.up) * fireSpeed, ForceMode.Impulse);
            }
        }
    }
}
