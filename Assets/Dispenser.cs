using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] dispenseSounds;
    private bool dispensed = false;

    public Transform dispensePoint;
    public Transform resetPoint;
    public Transform pizzaSlot;
    public Transform dispenseDirection;
    public float dispenseSpeed;
    public GameObject pizzaPrefab;

	void Start(){
		audioSrc = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y < dispensePoint.position.y && !dispensed)
        {
            GameObject pizza = Instantiate(pizzaPrefab, pizzaSlot.position, Quaternion.identity);
            Rigidbody rb = pizza.GetComponent<Rigidbody>();

            Vector3 direction = dispenseDirection.position - pizzaSlot.position;
            rb.AddForce(direction * dispenseSpeed, ForceMode.Impulse);
            dispensed = true;

			//Play the sound of the pizza getting despensed!
			if (dispenseSounds.Length > 0) {
				int index = Random.Range (0, dispenseSounds.Length + 1);
				audioSrc.clip = dispenseSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}
        }
        if (transform.position.y > resetPoint.position.y)
        {
            dispensed = false;
        }
	}
}
