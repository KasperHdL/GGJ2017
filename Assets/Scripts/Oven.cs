using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

	public List<GameObject> pizzasInOven;
	private AudioSource audioSrc;
	public AudioClip[] bakeSounds;
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void doorClosed(){
		int index = Random.Range (0, bakeSounds.Length + 1);
		audioSrc.clip = bakeSounds [index];
		if (!audioSrc.isPlaying) {
			audioSrc.Play ();
		}
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
