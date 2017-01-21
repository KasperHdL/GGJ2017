using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool isRunning = false;

    public int numOrderPerLevelMultiplier = 2;
    public float timeBetweenOrderMultiplier = 60f;
    public float roundBufferTimeMultiplier = 30f;
    public float startDelay = 2f;

    [Header("Running variables")]
    public int level;
    public float nextOrder;
    public int numRoundOrdersLeft;
    public float timeBetweenOrders;
    public float roundBufferTime;
    public float roundStartTime;
    public float roundEndTime;


    private static GameManager instance;
    private OrderManager orderManager;

    public void Awake(){
        if(instance != null)
            throw new UnityException("Multiple game managers found");
        instance = this;
    }

    public static GameManager getInstance(){
        return instance;
    }

    public void Start(){
        orderManager = OrderManager.getInstance();

    }

    public void FixedUpdate(){
        if(isRunning){
            if(numRoundOrdersLeft > 0 && nextOrder < Time.time){
            //make order

                orderManager.newOrder(1);
                nextOrder += timeBetweenOrders;

                numRoundOrdersLeft--;
            }

            if((orderManager.orders.Count == 0 && numRoundOrdersLeft == 0) || roundEndTime < Time.time){
                if(orderManager.orders.Count == 0 && numRoundOrdersLeft == 0){
                    newRound();
                }else{
                    isRunning = false;
                    //GAME Lost
                }
            }
        }
    }

    public void startGame(){
        if(isRunning){
            if(Time.time - roundStartTime < 5f)
                return;
        }
        isRunning = true;
        level = 1;
        newRound();
        orderManager.clear();
        level++;
    }

    public void newRound(){
        if(!isRunning) startGame();

        numRoundOrdersLeft = level * numOrderPerLevelMultiplier;
        timeBetweenOrders = (1f/level) * timeBetweenOrderMultiplier; 
        roundStartTime = Time.time;
        roundBufferTime = (1f/level) * roundBufferTimeMultiplier;
        roundEndTime = roundStartTime + (numRoundOrdersLeft * timeBetweenOrders) + roundBufferTime + startDelay;
        nextOrder = roundStartTime + startDelay;

    }
}
