using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public List<Ingredient> ingredients;

	// Use this for initialization
	void Start () {
        ingredients = new List<Ingredient>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision coll){
        Debug.Log("ASdAS");
        if (coll.gameObject.tag == "Ingredient"){
            for(int i = 0; i < ingredients.Count; i++){
                if(ingredients[i] == coll.gameObject.GetComponent<Ingredient>()){
                    return;
                }
            }

            ingredients.Add(coll.gameObject.GetComponent<Ingredient>());

            coll.transform.localScale = Vector3.one;
            coll.transform.position = coll.contacts[0].point;
            coll.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
            coll.transform.SetParent(transform, true);

        }

    }
}
