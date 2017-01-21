using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public ModelPizza model;
    public Transform ingredientContainer;
    public int[] ingredientCount;
	public bool cooked = false;

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
		foreach (GameObject thing in dummyIngredients) {
			switch (thing.GetComponent<Ingredient> ().type) {

			case Ingredient.Type.Cheese:
				Material cheeseCooked = (Material)Resources.Load ("cheeseTexCooked", typeof(Material));
				thing.GetComponent<Renderer> ().material = cheeseCooked;
				break;

			case Ingredient.Type.Mushroom:
				Material mushroomCooked = (Material)Resources.Load ("mushroomCooked", typeof(Material));
				thing.GetComponent<Renderer> ().material = mushroomCooked;
				break;

			case Ingredient.Type.Salami:
				Material salamiCooked = (Material)Resources.Load ("salamiTexCooked", typeof(Material));
				thing.GetComponent<Renderer> ().material = salamiCooked;
				break;

			default:
				Debug.Log ("FUCK, The ingredient do not exist");
				break;
			}
		}

	}

    void OnCollisionEnter(Collision coll){
        if (coll.gameObject.tag == "Ingredient"){
            Ingredient.Type type = coll.gameObject.GetComponent<Ingredient>().type;

            int i = 0;
            for(;i < model.ingredientTypes.Length; i++){
                if(model.ingredientTypes[i] == type)
                    break;
            }

            Transform t = coll.transform;

            GameObject g = Instantiate(model.prefabIngredients[i], t.position, t.rotation);
			dummyIngredients.Add (g);

            g.transform.SetParent(ingredientContainer, true);
            ingredientCount[i]++;
            Destroy(coll.gameObject);

        }

    }
}
