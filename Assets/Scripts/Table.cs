using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public float minPizzaTime = 2f;
    public OrderManager orderManager;

    private List<GameObject> activePizzas;
    private List<float> acceptedTime;

	void Start () {
        orderManager = GameObject.Find("Systems").GetComponent<OrderManager>();

        activePizzas = new List<GameObject>();
        acceptedTime = new List<float>();
		
	}
	
	void FixedUpdate () {
		
        for(int i = acceptedTime.Count - 1;i > -1; i--){
            if(Time.time >= acceptedTime[i]){
                //TODO GIVE POINT REMOVE PIZZA N STUFF

                ScoreManager.getInstance().score++;
                Destroy(activePizzas[i]);
                activePizzas.RemoveAt(i);
                acceptedTime.RemoveAt(i);

            }
        }
	}

    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Pizza"){
            for(int i = 0;i < activePizzas.Count; i++){
                if(coll.gameObject == activePizzas[i])
                    return;
            }
            activePizzas.Add(coll.gameObject);
            acceptedTime.Add(Time.time + minPizzaTime);
        }
    }

    void OnCollisionExit(Collision coll){
        if(coll.gameObject.tag == "Pizza"){
            for(int i = 0;i < activePizzas.Count; i++){
                if(coll.gameObject == activePizzas[i]){
                    activePizzas.RemoveAt(i);
                    acceptedTime.RemoveAt(i);
                }
            }
        }

    }
}
