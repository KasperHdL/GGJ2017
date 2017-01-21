using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour {

    public int numTables = 3;
    public int numIngredients = 3;

    public int maxNumOrders;
    public Transform orderOrigin;
    public Vector3 orderOffset;

    public GameObject prefabOrder;
    //Current Orders
    public List<Order> orders;
    public bool[] orderSlots;
    public int orderCount = 0;


    private static OrderManager instance;

    public void Awake(){
        if(instance != null)
            throw new UnityException("Multiple order managers found");
        instance = this;
    }

    public static OrderManager getInstance(){
        return instance;
    }

    public void Start(){
        orders = new List<Order>();
        orderSlots = new bool[maxNumOrders];

    }

    public void Update(){
        //for testing
        if(Input.GetKeyDown(KeyCode.Alpha1))
            newOrder(1);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            newOrder(2);
        if(Input.GetKeyDown(KeyCode.Alpha3))
            newOrder(3);
    }

    //difficulty is how many rows will be generated
    public void newOrder(int difficulty){
        if(difficulty <= 0 || difficulty >= 4){
            Debug.Log("Difficulty must be above 0");
        }

        int orderIndex = -1;
        for(int i = 0; i < orderSlots.Length; i++){
            if(!orderSlots[i]){
                orderIndex = i;
                orderSlots[i] = true;
                break;
            }
        }
        if(orderIndex == -1){
            Debug.LogWarning("Max number of orders reached");
            return;
        }

        GameObject g = Instantiate(prefabOrder, orderOrigin.transform.position + orderOffset * orderIndex, Quaternion.identity) as GameObject;
        Order o = g.GetComponent<Order>();

        int[] ingredientCount = new int[numIngredients];

        for(int i = 0; i < ingredientCount.Length; i++){
            ingredientCount[i] = Random.Range(0, 4);
        }

        o.set(Random.Range(0, numTables), ingredientCount);
        o.slot = orderIndex;
        o.id = orderCount++;

        orders.Add(o);
    }

    public void delivered(Pizza pizza){
        int[] ingredientCount = new int[numIngredients];

        if(pizza.ingredients == null)
            return;

        for(int i = 0; i < pizza.ingredients.Count; i++){
            ingredientCount[pizza.ingredients[i].type]++;
        }

        bool found = false;
        for(int i = 0; i < orders.Count; i++){
            bool b = true;
            for(int j = 0; j < orders[i].ingredientCount.Length; j++){
                if(ingredientCount[j] != orders[i].ingredientCount[j]){
                    b = false;
                    break;
                }

            }
            if(!b)
                continue;

            found = true;
            //correct order!?
            Debug.Log(ingredientCount);
            Debug.Log(orders[i].ingredientCount);
            
            ScoreManager.getInstance().score++;


        }
        if(!found){
            Debug.Log("WRONG!?");

        }




    }


}
