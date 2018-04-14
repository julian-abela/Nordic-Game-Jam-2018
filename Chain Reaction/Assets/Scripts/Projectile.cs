using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Baseball,
    Car,
    BowlingBall,
    Missile
}

public class Projectile : MonoBehaviour
{
    public float controlSpeed;
    public float fireSpeed;
    public Weapon weaponType;

    private bool carMoving;

    void Update()
    {
        /*
        float step = controlSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * step;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * step;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * step;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * step;
        }
        */

        if (Input.GetKey(KeyCode.Space))
        {
            switch(weaponType)
            {
                case Weapon.Baseball:
                    GetComponent<Rigidbody>().useGravity = true;
                    //GetComponent<Rigidbody>().AddForce((transform.forward * fireSpeed + transform.up * fireSpeed / 8), ForceMode.Impulse);
                    GetComponent<TruckScript>().DriveTruck();
                    break;
                case Weapon.Missile:
                    GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed, ForceMode.Force);
                    break;
                case Weapon.BowlingBall:
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().AddForce(Vector3.down * fireSpeed, ForceMode.Force);
                    break;
                case Weapon.Car:
                    carMoving = true;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed, ForceMode.Force);
                    break;
            }
        }

        if (carMoving && GetComponent<Rigidbody>().velocity.magnitude < fireSpeed/2)
        {
            GetComponent<Rigidbody>().AddForce(-transform.right * fireSpeed, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (weaponType == Weapon.Missile)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
