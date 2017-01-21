using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public bool gameRunning = false;
    public bool roundRunning = false;
    public bool roundCleared = false;

    public AnimationCurve numOrderPerLevelMultiplier;
    public AnimationCurve timeBetweenOrderMultiplier;
    public AnimationCurve roundBufferTimeMultiplier;
    public float startDelay = 2f;

    [Header("Running variables")]
    public int level;
    public float nextOrder;
    public int numRoundOrdersLeft;
    public float timeBetweenOrders;
    public float roundBufferTime;
    public float roundStartTime;
    public float roundEndTime;

    public Text levelText;

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

    public void Update() {
	if (level == 0) {
	    levelText.text = "Press bell to begin";
	}
	else {
             levelText.text = "Wave " + level.ToString();
	}
    }

    public void FixedUpdate(){
        if(roundRunning && !roundCleared)
        {
            if((orderManager.orders.Count == 0 && numRoundOrdersLeft > 0) || (numRoundOrdersLeft > 0 && nextOrder < Time.time)){
            //make order

                orderManager.newOrder(1);
                nextOrder += timeBetweenOrders;

                numRoundOrdersLeft--;
            }

            if((orderManager.orders.Count == 0 && numRoundOrdersLeft == 0) || roundEndTime < Time.time){
                if(orderManager.orders.Count == 0 && numRoundOrdersLeft == 0){
                    roundRunning = false;
                    roundCleared = true;
                }else{
                    gameRunning = false;
                    roundRunning = false;
                    roundCleared = false;
                    //GAME Lost
                    orderManager.clear();
                }
            }
        }
    }

    public void startGame(){
        if (roundCleared && ((orderManager.orders.Count == 0 && numRoundOrdersLeft > 0) || (numRoundOrdersLeft > 0 && nextOrder < Time.time))) {
            newRound();
            return;
        }
        gameRunning = true;
        roundRunning = true;
        roundCleared = false;
         level = 0;

        newRound();
        orderManager.clear();
    }

    public void newRound(){
        if(!gameRunning || !roundRunning) startGame();
        level++;
        roundRunning = true;
        roundCleared = false;

        numRoundOrdersLeft = Mathf.RoundToInt(level * numOrderPerLevelMultiplier.Evaluate(level));
        timeBetweenOrders = (1f/level) * timeBetweenOrderMultiplier.Evaluate(level); 
        roundStartTime = Time.time;
        roundBufferTime = (1f/level) * roundBufferTimeMultiplier.Evaluate(level);
        roundEndTime = roundStartTime + (numRoundOrdersLeft * timeBetweenOrders) + roundBufferTime + startDelay;
        nextOrder = roundStartTime + startDelay;

    }
}
