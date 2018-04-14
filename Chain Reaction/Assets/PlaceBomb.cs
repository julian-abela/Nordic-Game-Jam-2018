using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour {

    public Camera camera;
    public GameObject bomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.Mouse0)) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Instantiate(bomb,hit.point,objectHit.rotation);

            }
        }
	}
}
