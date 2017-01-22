using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] missSounds;
	//Remember to put ind sounds if you need them
	public AudioClip[] ingrdientSounds;
	//Remember to put ind sounds if you need them
	public AudioClip[] pizzaPickupSounds;
	//Remember to put ind sounds if you need them
	public AudioClip[] throwSounds;

    public ModelPizza model;
    public Transform ingredientContainer;
    public int[] ingredientCount;
	public bool cooked = false;
    public bool burnt = false;
    public bool fired = false;

    public ParticleSystem smoke;

    public float timeInOven = 0f;
    public float ovenTimeNeeded = 5f;

    public Renderer renderer;
    public Valve.VR.InteractionSystem.Interactable interactable;

    public List<GameObject> dummyIngredients;
	// Use this for initialization
	void Start () {
        ingredientCount = new int[model.ingredientTypes.Length];
		dummyIngredients = new List<GameObject> ();
        audioSrc = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void cook(){
		cooked = true;
		foreach (GameObject child in dummyIngredients) {
            Ingredient i = child.GetComponent<Ingredient>();
            i.renderer.material = i.model.cookedMaterial;
		}
        smoke.emissionRate = 25;

        renderer.material = model.cookedMaterial;
	}

    public void burn()
    {
        burnt = true;
        foreach (GameObject child in dummyIngredients)
        {
            Ingredient i = child.GetComponent<Ingredient>();
            i.renderer.material = i.model.burntMaterial;
        }
        smoke.emissionRate = 100f;

        renderer.material = model.burntMaterial;
    }

    void OnDestroy() {
		//GameObject soundObject = 
	}

	public void pickedUp(){
		if (pizzaPickupSounds.Length > 0) {
			int index = Random.Range (0, pizzaPickupSounds.Length);
			audioSrc.clip = pizzaPickupSounds [index];
			if (!audioSrc.isPlaying) {
				audioSrc.Play ();
			}
		}
	}

	public void thrown(){
        if (throwSounds.Length > 0) {
			int index = Random.Range (0, throwSounds.Length);
			audioSrc.clip = throwSounds [index];
			if (!audioSrc.isPlaying) {
				audioSrc.Play ();
			}
		}
	}

    void OnCollisionEnter(Collision coll){
        if (cooked) return;

        if (coll.gameObject.tag == "Ingredient")
        {
            if (ingrdientSounds.Length > 0) {
				int index = Random.Range (0, ingrdientSounds.Length);
				audioSrc.clip = ingrdientSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}

			Ingredient ingredient = coll.gameObject.GetComponent<Ingredient> ();

			int i = 0;
			for (; i < model.ingredientTypes.Length; i++) {
				if (model.ingredientTypes [i] == ingredient.type)
					break;
			}

			Transform t = coll.transform;

			GameObject g = Instantiate (model.prefabIngredients [i], t.position, t.rotation);
			dummyIngredients.Add (g);


			g.transform.SetParent (ingredientContainer, true);
			ingredientCount [i]++;
			ingredient.renderer.enabled = false;

		} else if (cooked&&coll.gameObject.tag=="Floor") {
			if (missSounds.Length > 0) {
				int index = Random.Range (0, missSounds.Length);
				audioSrc.clip = missSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}
		}

    }
}
