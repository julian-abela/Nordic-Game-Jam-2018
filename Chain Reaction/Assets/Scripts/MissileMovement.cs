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


    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;
    ParameterInstance impactParam;

    private void Start()
    {
        eventStart = RuntimeManager.CreateInstance(audioStart);
        RuntimeManager.AttachInstanceToGameObject(eventStart, transform, GetComponent<Rigidbody>());
        eventStart.getParameter("Missile", out impactParam);
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
        Destroy(this.gameObject, 0.05f);
    }
}
