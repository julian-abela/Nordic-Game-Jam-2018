using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Transform target;
    public float speed;
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
            // transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.position = transform.position + transform.forward * step;
        } else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position = transform.position - transform.forward * step;
        }
    }
}
