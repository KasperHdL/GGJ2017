using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour {

    public Ingredient.Type[] ingredientTypes;
    [Header("Models")]
    public ModelOrder modelOrder;
    public ModelPizza modelPizza;

    public int numTables = 3;
    public int maxNumOrders;
    public Transform orderOrigin;
    public Vector3 orderOffset;

    public GameObject prefabOrder;
    //Current Orders
    public List<Order> orders;
    public bool[] orderSlots;
    public int orderCount = 0;


    private ScoreManager scoreManager;
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
        scoreManager = ScoreManager.getInstance();

        modelOrder.ingredientTypes = ingredientTypes;
        modelPizza.ingredientTypes = ingredientTypes;

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

    public void clear(){
        for(int i = orders.Count - 1;i > -1 ; i--){
            Destroy(orders[i].gameObject,1f);
            StartCoroutine(setSlotFree(1.5f, i));
            orders[i].setFailed();
            orders.RemoveAt(i);
            orderCount = 0;
        }

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

        o.difficulty = difficulty;
        int[] ingredientCount = new int[ingredientTypes.Length];

        for(int i = 0; i < ingredientCount.Length; i++){
            ingredientCount[i] = Random.Range(0, 4);
        }

        o.set(Random.Range(0, numTables), ingredientCount);
        o.slot = orderIndex;
        o.id = orderCount++;

        orders.Add(o);
    }

    public bool delivered(Pizza pizza){
        int difficulty = 1;

        if (!pizza.cooked || pizza.burnt) return false;

        bool found = false;
        int i = 0;
        for(; i < orders.Count; i++){
            bool correctIngredients = true;
            for(int j = 0; j < orders[i].ingredientCount.Length; j++){
                if(pizza.ingredientCount[j] != orders[i].ingredientCount[j]){
                    correctIngredients = false;
                    break;
                }

            }
            if(!correctIngredients)
                continue;

            found = true;

            difficulty = orders[i].difficulty;

            StartCoroutine(setSlotFree(1.5f, orders[i].slot));
            orders[i].setDone();
            Destroy(orders[i].gameObject, 1f);
            orders.RemoveAt(i);
            orderCount--;

            break;
        }

        if(found){
            scoreManager.score += difficulty;
        }else{

        }

        return found;

    }

    IEnumerator setSlotFree(float seconds, int index){
        yield return new WaitForSeconds(seconds);
        orderSlots[index] = false;
        

    }


}
