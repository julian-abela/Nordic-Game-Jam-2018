using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public float rotationSpeed = 100;
    public Vector3 lookTarget = Vector3.zero;
    public float spawnDistance = 10;

    public GameObject baseballPrefab;
    public GameObject missilePrefab;
    public GameObject bowlingBallPrefab;
    public GameObject carPrefab;

    private GameObject currentWeapon;
    private bool cameraLocked = false;

    void Update()
    {
        if (!cameraLocked)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAround(lookTarget, -Vector3.up, Time.deltaTime * rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.RotateAround(lookTarget, Vector3.up, Time.deltaTime * rotationSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCurrentWeapon(Weapon.Baseball);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCurrentWeapon(Weapon.Missile);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCurrentWeapon(Weapon.BowlingBall);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeCurrentWeapon(Weapon.Car);
        }
    }

    void ChangeCurrentWeapon(Weapon weaponType)
    {
        //cameraLocked = true;

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        switch (weaponType)
        {
            case Weapon.Baseball:
                currentWeapon = Instantiate(baseballPrefab, Vector3.MoveTowards(transform.position, lookTarget, spawnDistance), transform.rotation);
                break;
            case Weapon.Missile:
                currentWeapon = Instantiate(missilePrefab, Vector3.MoveTowards(transform.position, lookTarget, spawnDistance), transform.rotation);
                break;
            case Weapon.BowlingBall:
                currentWeapon = Instantiate(bowlingBallPrefab, Vector3.MoveTowards(transform.position, lookTarget, spawnDistance*2), transform.rotation);
                break;
            case Weapon.Car:
                currentWeapon = Instantiate(carPrefab, Vector3.MoveTowards(transform.position, lookTarget, spawnDistance), transform.rotation);
                break;
        }
    }
}
