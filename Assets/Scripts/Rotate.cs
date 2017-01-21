using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Vector3 vel;


	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(vel.x,vel.y,vel.z);
		
	}
}
