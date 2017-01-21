using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public ModelPizza model;
    public Transform ingredientContainer;
    public int[] ingredientCount;

	// Use this for initialization
	void Start () {
        ingredientCount = new int[model.ingredientTypes.Length];
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

            g.transform.SetParent(ingredientContainer, true);
            ingredientCount[i]++;
            Destroy(coll.gameObject);

        }

    }
}
