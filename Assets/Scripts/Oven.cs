using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

	public bool closed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void isClosed(bool closed){
		this.closed = closed;
	}

	void OnTriggerStay(Collider other) {
		if (closed) {
			if (other.tag.Equals ("Pizza")) {
				other.GetComponent<Pizza> ().cook ();
			}
		}
	}
}
