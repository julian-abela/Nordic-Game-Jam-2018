using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class MissileMovement : MonoBehaviour
{
    public float fireSpeed;
    public float explosionStrength;
    public float explosionRadius;
    public GameObject explosionPrefab;

    private bool missileShot = false;
    private Vector3 upEnd;
    private Vector3 downEnd;
    private float range = 100f;
    private float verticalLerpValue = 0.5f;
    private float inputSensitivity;

    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;
    ParameterInstance impactParam;

    private void Start()
    {
        eventStart = RuntimeManager.CreateInstance(audioStart);
        //RuntimeManager.AttachInstanceToGameObject(eventStart, transform, GetComponent<Rigidbody>());
        eventStart.getParameter("Missile", out impactParam);

        
        inputSensitivity = 0.5f / range;
        upEnd = transform.position + transform.up * range;
        downEnd = transform.position + transform.up * range * -1f;
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) && !missileShot)
        {
            eventStart.start();
            GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed, ForceMode.Force);
            transform.parent = null;
            missileShot = true;
        }

        //verticalLerpValue -= Input.GetAxisRaw("Vertical") * inputSensitivity;
        //verticalLerpValue = Mathf.Max(0, Mathf.Min(verticalLerpValue, 1));
        //transform.root.rotation = Quaternion.Euler(Vector3.Lerp(upEnd, downEnd, verticalLerpValue));
    }

    private void OnCollisionEnter(Collision collision)
    {
        impactParam.setValue(1);

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
        Debug.Log(transform.root.gameObject.name);
        Destroy(transform.root.gameObject, 0.05f);
    }
}
