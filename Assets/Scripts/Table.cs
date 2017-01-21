using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public GameObject prefabCorrectParticleSystem;
    public GameObject prefabWrongParticleSystem;

    public float maxPizzaTime = 2f;
    public float maxIngredientTime = 5f;
    private OrderManager orderManager;

    private List<Pizza> activePizzas;
    private List<float> acceptedTime;

    private List<Ingredient> activeIngredients;
    private List<float> destroyIngredientTime;

    public Transform[] legs;
    public float legOffset = 0.9f;

	void Start () {
        orderManager = OrderManager.getInstance();

        activePizzas = new List<Pizza>();
        acceptedTime = new List<float>();

        activeIngredients = new List<Ingredient>();
        destroyIngredientTime = new List<float>();


        //hack table
        Vector3 scale = transform.parent.localScale / 2;

        transform.localScale = transform.parent.localScale;
        transform.parent.localScale = Vector3.one;

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
                    Instantiate(prefabWrongParticleSystem, activePizzas[i].transform.position, prefabWrongParticleSystem.transform.rotation);
                }
                

                Destroy(activePizzas[i].gameObject);
                activePizzas.RemoveAt(i);
            }
        }

        for(int i = activeIngredients.Count - 1;i > -1; i--){
            if(Time.time >= destroyIngredientTime[i]){
                destroyIngredientTime.RemoveAt(i);

                Instantiate(prefabWrongParticleSystem, activeIngredients[i].transform.position, Quaternion.identity);
                activeIngredients[i].destroy();
                activeIngredients.RemoveAt(i);

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
            acceptedTime.Add(Time.time + maxPizzaTime);
        }else if(coll.gameObject.tag == "Ingredient"){
            Ingredient ing = coll.gameObject.GetComponent<Ingredient>();

            for(int i = 0;i < activeIngredients.Count; i++){
                if(ing == activeIngredients[i])
                    return;
            }
            activeIngredients.Add(ing);
            destroyIngredientTime.Add(Time.time + maxIngredientTime);

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
        else if(coll.gameObject.tag == "Ingredient"){
            Ingredient ing = coll.gameObject.GetComponent<Ingredient>();

            for(int i = 0;i < activeIngredients.Count; i++){
                if(ing == activeIngredients[i]){
                    activeIngredients.RemoveAt(i);
                    destroyIngredientTime.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
