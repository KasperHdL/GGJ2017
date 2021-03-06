﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    public bool _doorClosed = false;
	public List<Pizza> pizzasInOven;
	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] bakeSounds;
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		for(int i = 0;i < pizzasInOven.Count; i++)
        {
            pizzasInOven[i].timeInOven += Time.fixedDeltaTime;
            if (!pizzasInOven[i].cooked && pizzasInOven[i].timeInOven > pizzasInOven[i].ovenTimeNeeded)
            {
                pizzasInOven[i].cook();
                if (bakeSounds.Length > 0)
                {
                    int index = Random.Range(0, bakeSounds.Length);
                    audioSrc.clip = bakeSounds[index];
                    if (!audioSrc.isPlaying)
                    {
                        audioSrc.Play();
                    }
                }
            }
            else if (!pizzasInOven[i].burnt && pizzasInOven[i].timeInOven > pizzasInOven[i].burnTimeNeeded)
            { 
                pizzasInOven[i].burn();
                if (bakeSounds.Length > 0)
                {
                    int index = Random.Range(0, bakeSounds.Length);
                    audioSrc.clip = bakeSounds[index];
                    if (!audioSrc.isPlaying)
                    {
                        audioSrc.Play();
                    }
                }
            }


    }
	}

    public void doorOpened()
    {
        _doorClosed = false;
        for (int i = 0; i < pizzasInOven.Count; i++)
        {
            pizzasInOven[i].interactable.disable = false;
        }
    }
	public void doorClosed(){
		//play sounds of the pizza being baked
		if (bakeSounds.Length > 0) {
			int index = Random.Range (0, bakeSounds.Length);
			audioSrc.clip = bakeSounds [index];
			if (!audioSrc.isPlaying) {
				audioSrc.Play ();
			}
		}
		foreach (Pizza pizza in pizzasInOven) {
			pizza.GetComponent<Pizza> ().cook ();
		}
        _doorClosed = true;
		for (int i = pizzasInOven.Count - 1;i > -1;i--) {
            if (pizzasInOven[i] == null)
                pizzasInOven.RemoveAt(i);
            else
            {
                pizzasInOven[i].GetComponent<Pizza>().cook();
                pizzasInOven[i].interactable.disable = true;
            }
        }
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Pizza")) {
            Pizza p = other.gameObject.GetComponent<Pizza>();

            pizzasInOven.Add (p);
            p.smoke.Play();
            
		}
	}

	void OnTriggerExit(Collider other){
        if (other.tag == "Pizza")
        {
            Pizza p = other.gameObject.GetComponent<Pizza>();
            if (pizzasInOven.Contains(p))
            {
                p.smoke.Stop();
                pizzasInOven.Remove(p);
            }
        }
	}
}
