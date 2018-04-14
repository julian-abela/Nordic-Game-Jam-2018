using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour {

    public Camera camera;
    public GameObject bomb;

	void Update () {
        if(Input.GetKeyUp(KeyCode.Mouse0)) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                var toPlace = Instantiate(bomb, hit.point, Quaternion.Euler(new Vector3(objectHit.rotation.eulerAngles.x,objectHit.rotation.eulerAngles.y,objectHit.rotation.eulerAngles.z+90)));
                toPlace.transform.parent = objectHit;

            }
        }
	}
}
