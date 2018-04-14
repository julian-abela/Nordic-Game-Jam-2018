using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector: MonoBehaviour {

    public GameObject baseballPrefab;
    public GameObject missilePrefab;
    public GameObject bowlingBallPrefab;
    public GameObject carPrefab;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(baseballPrefab, new Vector3(-1.5f, 2, -14), Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(missilePrefab, new Vector3(-1.5f, 2, -14), Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(bowlingBallPrefab, new Vector3(-1.5f, 10, 0), Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(carPrefab, new Vector3(-1.5f, 2, -14), Quaternion.identity);
        }
    }
}
