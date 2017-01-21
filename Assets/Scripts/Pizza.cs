using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public ModelPizza model;
    public Transform ingredientContainer;
    public int[] ingredientCount;
	public bool cooked = false;
    public Renderer renderer;

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

    void OnCollisionEnter(Collision coll){
        if (coll.gameObject.tag == "Ingredient"){
            Ingredient ingredient = coll.gameObject.GetComponent<Ingredient>();

            int i = 0;
            for(;i < model.ingredientTypes.Length; i++){
                if(model.ingredientTypes[i] == ingredient.type)
                    break;
            }

            Transform t = coll.transform;

            GameObject g = Instantiate(model.prefabIngredients[i], t.position, t.rotation);
			dummyIngredients.Add (g);

            g.transform.SetParent(ingredientContainer, true);
            ingredientCount[i]++;
            ingredient.renderer.enabled = false;

        }

    }
}
