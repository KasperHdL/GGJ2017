using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public Transform ingredientContainer;
    public List<Ingredient> ingredients;
    public Transform attachPoint;

	// Use this for initialization
	void Start () {
        ingredients = new List<Ingredient>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision coll){
        if (coll.gameObject.tag == "Ingredient"){
            for(int i = 0; i < ingredients.Count; i++){
                if(ingredients[i] == coll.gameObject.GetComponent<Ingredient>()){
                    return;
                }
            }

            ingredients.Add(coll.gameObject.GetComponent<Ingredient>());

            Rigidbody body = coll.transform.GetComponent<Rigidbody>();
            body.isKinematic = true;


            coll.transform.SetParent(ingredientContainer, true);
            coll.transform.position = attachPoint.position;
            coll.collider.enabled = false;

        }

    }
}
