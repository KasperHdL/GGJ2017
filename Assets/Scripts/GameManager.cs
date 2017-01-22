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

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] endRoundSounds;
	public AudioClip music;

    public void Awake(){
        if(instance != null)
            throw new UnityException("Multiple game managers found");
        instance = this;
		audioSrc = GetComponent<AudioSource>();
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
					if (endRoundSounds.Length > 0) {
						audioSrc.Stop ();
						audioSrc.loop = false;
						int index = Random.Range (0, endRoundSounds.Length);
						audioSrc.clip = endRoundSounds [index];
						if (!audioSrc.isPlaying) {
							audioSrc.Play ();
						}
					}
                }else{
                    gameRunning = false;
                    roundRunning = false;
                    roundCleared = false;
                    //GAME Lost
					audioSrc.loop = false;
					audioSrc.Stop ();
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
		StartCoroutine (fadeIn ());

        orderManager.clear();
    }

	IEnumerator fadeIn(){
		audioSrc.clip = music;
		audioSrc.loop = true;
		audioSrc.volume = 0;
		audioSrc.Play ();
		while (audioSrc.volume != 1) {
			audioSrc.volume += Time.deltaTime;
			yield return new WaitForSeconds (0.2f);
		}
		yield break;
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
