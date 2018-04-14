using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilScript : MonoBehaviour {
    private float fireSpeed = 3f;
    private Rigidbody body;
    private Vector3 leftEnd;
    private Vector3 rightEnd;
    private float range = 10f;
    private float lerpValue = .5f;
    private float inputSensitivity;

    private void Start()
    {
        inputSensitivity = 1f / range;
        body = GetComponent<Rigidbody>();
        transform.position += new Vector3(0,2f);
        leftEnd = transform.position + transform.right * range;
        rightEnd = transform.position + transform.right * range * -1f;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.useGravity = true;
            body.AddForce(Vector3.down * fireSpeed, ForceMode.Force);
            Destroy(this);
        }
        else
        {
            lerpValue += Input.GetAxisRaw("Horizontal") * inputSensitivity;
            lerpValue = Mathf.Max(0, Mathf.Min(lerpValue, 1));
            transform.position = Vector3.Lerp(rightEnd, leftEnd, lerpValue);
        }
	}
}
