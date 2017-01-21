using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] missSounds;
	//Remember to put ind sounds if you need them
	public AudioClip[] ingrdientSounds;

    public ModelPizza model;
    public Transform ingredientContainer;
    public int[] ingredientCount;
	public bool cooked = false;
    public Renderer renderer;
    public Valve.VR.InteractionSystem.Interactable interactable;

    public List<GameObject> dummyIngredients;
	// Use this for initialization
	void Start () {
        ingredientCount = new int[model.ingredientTypes.Length];
		dummyIngredients = new List<GameObject> ();
		
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

        renderer.material = model.cookedMaterial;
	}

	void OnDestroy() {
		//GameObject soundObject = 
	}

    void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Ingredient") {
			if (ingrdientSounds.Length > 0) {
				int index = Random.Range (0, ingrdientSounds.Length + 1);
				audioSrc.clip = ingrdientSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}
			Ingredient ingredient = coll.gameObject.GetComponent<Ingredient> ();
			if (ingredient.renderer.enabled == false)
				return;

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

		} else if (cooked&&coll.gameObject.name=="Floor") {
			if (missSounds.Length > 0) {
				int index = Random.Range (0, missSounds.Length + 1);
				audioSrc.clip = missSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}
		}

    }
}
