using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public float minPizzaTime = 2f;
    private List<GameObject> activePizzas;
    private List<float> acceptedTime;

	void Start () {

        activePizzas = new List<GameObject>();
        acceptedTime = new List<float>();
		
	}
	
	void FixedUpdate () {
		
        for(int i = acceptedTime.Count - 1;i > -1; i--){
            if(Time.time >= acceptedTime[i]){
                //TODO GIVE POINT REMOVE PIZZA N STUFF

                Destroy(activePizzas[i]);
                activePizzas.RemoveAt(i);
                acceptedTime.RemoveAt(i);

            }
        }
	}

    void OnCollisionEnter(Collision coll){
        for(int i = 0;i < activePizzas.Count; i++){
            if(coll.gameObject == activePizzas[i])
                return;
        }
        activePizzas.Add(coll.gameObject);
        acceptedTime.Add(Time.time + minPizzaTime);
    }

    void OnCollisionExit(Collision coll){
        for(int i = 0;i < activePizzas.Count; i++){
            if(coll.gameObject == activePizzas[i])
                activePizzas.RemoveAt(i);
                acceptedTime.RemoveAt(i);
        }

    }
}
