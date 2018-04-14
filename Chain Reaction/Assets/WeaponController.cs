using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public static WeaponController instance;

    public float rotationSpeed = 100;
    public Vector3 lookTarget = Vector3.zero;
    public float spawnDistance = 10;

    public GameObject baseballPrefab;
    public GameObject missilePrefab;
    public GameObject bowlingBallPrefab;
    public GameObject carPrefab;
    public GameObject c4Prefab;

    private GameObject currentWeapon;
    private Weapon currentWeaponType;

    public bool cameraLocked;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(!cameraLocked)
            HorizontalMovementUpdate();

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
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeCurrentWeapon(Weapon.C4);
        }
    }

    private void HorizontalMovementUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(lookTarget, -Vector3.up, Time.deltaTime * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(lookTarget, Vector3.up, Time.deltaTime * rotationSpeed);
        }
    }

    void ChangeCurrentWeapon(Weapon weaponType)
    {
        cameraLocked = true;
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        switch (weaponType)
        {
            case Weapon.Baseball:
                currentWeapon = Instantiate(baseballPrefab, Vector3.MoveTowards(transform.position, lookTarget - new Vector3(0, 20, 0), spawnDistance), transform.rotation);
                break;
            case Weapon.Missile:
                currentWeapon = Instantiate(missilePrefab, Vector3.MoveTowards(transform.position, lookTarget - new Vector3(0, 20, 0), spawnDistance), transform.rotation);
                break;
            case Weapon.BowlingBall:
                currentWeapon = Instantiate(bowlingBallPrefab, Vector3.MoveTowards(transform.position, lookTarget, spawnDistance * 2), transform.rotation);
                break;
            case Weapon.Car:
                currentWeapon = Instantiate(carPrefab, Vector3.MoveTowards(transform.position, lookTarget, spawnDistance), Quaternion.Euler(new Vector3(transform.rotation.x + 180, transform.rotation.y + 90, transform.rotation.z)));
                currentWeapon.transform.LookAt(lookTarget);
                break;
            case Weapon.C4:
                currentWeapon = Instantiate(c4Prefab, Vector3.MoveTowards(transform.position, lookTarget - new Vector3(0, 20, 0), spawnDistance), transform.rotation);
                break;
        }
    }
}
