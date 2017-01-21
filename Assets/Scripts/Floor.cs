using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameObject prefabWrongParticleSystem;
    public float minPizzaTime = 2f;

    private List<Pizza> activePizzas;
    private List<float> acceptedTime;

	// Use this for initialization
	void Start () {
		
        activePizzas = new List<Pizza>();
        acceptedTime = new List<float>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        for(int i = acceptedTime.Count - 1;i > -1; i--){
            if(Time.time >= acceptedTime[i]){

                acceptedTime.RemoveAt(i);


                Instantiate(prefabWrongParticleSystem, activePizzas[i].transform.position, Quaternion.identity);
                

                Destroy(activePizzas[i].gameObject);
                activePizzas.RemoveAt(i);

            }
        }
	}

    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Pizza"){
            Pizza p = coll.gameObject.GetComponent<Pizza>();

            for(int i = 0;i < activePizzas.Count; i++){
                if(p == activePizzas[i])
                    return;
            }
            activePizzas.Add(p);
            acceptedTime.Add(Time.time + minPizzaTime);
        }
    }

    void OnCollisionExit(Collision coll){
        if(coll.gameObject.tag == "Pizza"){
            Pizza p = coll.gameObject.GetComponent<Pizza>();

            for(int i = 0;i < activePizzas.Count; i++){
                if(p == activePizzas[i]){
                    activePizzas.RemoveAt(i);
                    acceptedTime.RemoveAt(i);
                }
            }
        }

    }
}
