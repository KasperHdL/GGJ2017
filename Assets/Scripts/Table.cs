using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public GameObject prefabCorrectParticleSystem;
    public GameObject prefabWrongParticleSystem;

    public float minPizzaTime = 2f;
    private OrderManager orderManager;

    private List<Pizza> activePizzas;
    private List<float> acceptedTime;

    public Transform[] legs;
    public float legOffset = 0.9f;

	void Start () {
        orderManager = OrderManager.getInstance();

        activePizzas = new List<Pizza>();
        acceptedTime = new List<float>();

        Vector3 scale = transform.localScale / 2;

        float x = scale.x * legOffset;
        float y = -0.5f;
        float z = scale.z * legOffset;
        
        legs[0].transform.localPosition = new Vector3(x  , y , z);
        legs[1].transform.localPosition = new Vector3(-x , y , z);
        legs[2].transform.localPosition = new Vector3(x  , y , -z);
        legs[3].transform.localPosition = new Vector3(-x , y , -z);
		
	}
	
	void FixedUpdate () {
		
        for(int i = acceptedTime.Count - 1;i > -1; i--){
            if(Time.time >= acceptedTime[i]){
                //TODO GIVE POINT REMOVE PIZZA N STUFF

                acceptedTime.RemoveAt(i);


                bool correct = orderManager.delivered(activePizzas[i]);
                if(correct){
                    Instantiate(prefabCorrectParticleSystem, activePizzas[i].transform.position, Quaternion.identity);
                }else{
                    Instantiate(prefabWrongParticleSystem, activePizzas[i].transform.position, Quaternion.identity);
                }
                

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
