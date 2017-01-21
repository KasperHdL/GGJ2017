using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongSounds : MonoBehaviour {

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] wrongSounds;
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		//Play the sound of the pizza getting despensed!
		if (wrongSounds.Length > 0) {
			int index = Random.Range (0, wrongSounds.Length + 1);
			audioSrc.clip = wrongSounds [index];
			if (!audioSrc.isPlaying) {
				audioSrc.Play ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
