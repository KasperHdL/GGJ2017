using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSound : MonoBehaviour {

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] correctSounds;
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		//Play the sound of the pizza getting despensed!
		if (correctSounds.Length > 0) {
			int index = Random.Range (0, correctSounds.Length + 1);
			audioSrc.clip = correctSounds [index];
			if (!audioSrc.isPlaying) {
				audioSrc.Play ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
