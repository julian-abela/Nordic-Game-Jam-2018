using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public bool tennisBall;

    private void Start()
    {
        if (tennisBall)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - transform.right * step;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + transform.right * step;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - transform.up * step;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + transform.up * step;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (tennisBall)
            {
                GetComponent<Rigidbody>().AddForce((transform.forward * speed + transform.up * speed/8), ForceMode.Impulse);

            } else
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * speed * 50, ForceMode.Force);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!tennisBall)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
