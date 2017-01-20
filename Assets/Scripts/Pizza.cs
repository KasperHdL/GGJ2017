using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public List<Ingredient> ingredients;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Ingredient"){
            coll.transform.SetParent(transform, true);
            coll.transform.localScale = Vector3.one;
            coll.transform.position = coll.contacts[0].point;
            coll.transform.GetComponent<Rigidbody>().isKinematic = true;

        }

    }
}
