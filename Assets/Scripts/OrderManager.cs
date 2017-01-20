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

        int[] ingredients = new int[Random.Range(1,4) + (difficulty - 1) * 3];

        for(int i = 0; i < ingredients.Length; i++){
            ingredients[i] = Random.Range(1, numIngredients + 1);
        }

        o.set(Random.Range(0,numTables), ingredients);
        o.slot = orderIndex;
        o.id = orderCount++;

        orders.Add(o);
    }
}
