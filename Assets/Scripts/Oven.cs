using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

	public List<GameObject> pizzasInOven;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void doorClosed(){
		foreach (GameObject pizza in pizzasInOven) {
			pizza.GetComponent<Pizza> ().cook ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Pizza")) {
			pizzasInOven.Add (other.gameObject);
		}
	}

	void onTriggerExit(Collider other){
		if (pizzasInOven.Contains (other.gameObject)) {
			pizzasInOven.Remove (other.gameObject);
		}
	}
}
