using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public float fireSpeed;
    public float explosionStrength = 10000000000000000;
    public float explosionRadius = 500;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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

        Destroy(this.gameObject, 0.1f);
    }
}
