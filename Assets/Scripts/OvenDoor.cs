using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenDoor : MonoBehaviour {

	public float forceAmount;
	public Vector3 theForce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		theForce = new Vector3 (0, forceAmount, 0);
		this.transform.gameObject.GetComponent<Rigidbody> ().AddTorque (theForce);
	}
}
