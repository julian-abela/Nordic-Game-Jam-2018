using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BombScript : MonoBehaviour {

    private Rigidbody rb;
    private MeshRenderer mr;
    private float timeCount;
    private float lastTickSecond;
    private bool exploded;

    public GameObject explosion;
    public float explosionForce = 5f;
    public float maxSize;
    public float timer;



    //Audio
    [EventRef]
    public string audioBombBip;
    [EventRef]
    public string audioExplosion;

    EventInstance eventBombBip;
    EventInstance eventExplosion;


	// Use this for initialization
	void Start () {

        timeCount = timer;
        lastTickSecond = timeCount;

        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timeCount <= 0f && !exploded)
        {
            eventBombBip.stop(STOP_MODE.IMMEDIATE);
            ExplosionSound();
            exploded = true;
            var particle = Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(particle, 1f);
            Destroy(gameObject, 2f);
            mr.enabled = false;

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, maxSize);
            foreach (Collider hit in colliders)
            {
                Rigidbody rigidBody = hit.GetComponent<Rigidbody>();

                if (rigidBody != null)
                {
                    rigidBody.AddExplosionForce(explosionForce, explosionPos, maxSize);
                }
            }
        }
        else if(!exploded)
        {
            if(timeCount < lastTickSecond - 1){
                lastTickSecond = timeCount;
                eventBombBip = RuntimeManager.CreateInstance(audioBombBip);
                eventBombBip.start();
            }
            timeCount -= Time.deltaTime;
            
        }

	}

    private void ExplosionSound()
    {
        eventExplosion = RuntimeManager.CreateInstance(audioExplosion);
        eventExplosion.start();
    }
}
