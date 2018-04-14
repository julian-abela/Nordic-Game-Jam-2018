using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BombScript : MonoBehaviour {

    private SphereCollider sc;
    private Rigidbody rb;

    public float explosionForce = 5f;
    public float maxSize;
    public float timer;
    private float timeCount;
    private int lastTickSecond;


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
        lastTickSecond = Mathf.FloorToInt(timer);

        sc = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timeCount <= 0f)
        {

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, maxSize);
            foreach (Collider hit in colliders)
            {
                Rigidbody rigidBody = hit.GetComponent<Rigidbody>();

                if (rigidBody != null)
                {
                    rigidBody.AddExplosionForce(explosionForce, explosionPos, maxSize);
                    ExplosionSound();
                }
            }
        }
        else
        {
            int second = Mathf.FloorToInt(timeCount);
            if(second < lastTickSecond){
                lastTickSecond = second;
                eventBombBip = RuntimeManager.CreateInstance(audioBombBip);
                eventBombBip.start();
                Debug.Log(timeCount);
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
