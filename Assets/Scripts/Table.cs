using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public float minPizzaTime = 2f;
    private OrderManager orderManager;

    private List<Pizza> activePizzas;
    private List<float> acceptedTime;

	void Start () {
        orderManager = OrderManager.getInstance();

        activePizzas = new List<Pizza>();
        acceptedTime = new List<float>();
		
	}
	
	void FixedUpdate () {
		
        for(int i = acceptedTime.Count - 1;i > -1; i--){
            if(Time.time >= acceptedTime[i]){
                //TODO GIVE POINT REMOVE PIZZA N STUFF

                acceptedTime.RemoveAt(i);

                orderManager.delivered(activePizzas[i]);

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
