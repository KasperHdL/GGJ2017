using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    public bool _doorClosed = false;
	public List<Pizza> pizzasInOven;
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

    public void doorOpened()
    {
        _doorClosed = false;
        for (int i = 0; i < pizzasInOven.Count; i++)
        {
            pizzasInOven[i].interactable.disable = false;
        }
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
			pizzasInOven.Add (other.gameObject.GetComponent<Pizza>());
		}
	}

	void OnTriggerExit(Collider other){
        if (other.tag == "Pizza")
        {
            Pizza p = other.gameObject.GetComponent<Pizza>();
            if (pizzasInOven.Contains(p))
            {
                pizzasInOven.Remove(p);
            }
        }
	}
}
