using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    private SphereCollider sc;
    private Rigidbody rb;

    public float explosionForce = 5f;
    public float maxSize;
    public float timer;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0f)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, maxSize);
            foreach (Collider hit in colliders)
            {
                Rigidbody rigidBody = hit.GetComponent<Rigidbody>();

                if (rigidBody != null)
                    rigidBody.AddExplosionForce(explosionForce, explosionPos, maxSize);
            }
        }
        else
            timer -= Time.deltaTime;
	}
}
