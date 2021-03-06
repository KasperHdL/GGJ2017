﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public bool gameRunning = false;
    public bool roundRunning = false;
    public bool roundCleared = false;

    public AnimationCurve numOrderPerLevelCurve;
    public AnimationCurve timeBetweenOrderCurve;
    public AnimationCurve roundBufferTimeCurve;
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
    public Text clockText;
    public Text clockText2;

    private static GameManager instance;
    private OrderManager orderManager;
    private ScoreManager scoreManager;

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] endRoundSounds;
	public AudioClip music;
    public AudioClip ambient;

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
        scoreManager = ScoreManager.getInstance();

        audioSrc.clip = ambient;
        audioSrc.loop = true;
        audioSrc.Play();
    }

    public void Update() {
	    if (level == 0) {
	        levelText.text = "Hit bell to begin";
	    }
	    else {
                levelText.text = "Wave " + level.ToString();
        }
    }

    public void FixedUpdate(){
        if(gameRunning && roundRunning && !roundCleared)
        {
            clockText.text = Mathf.Round(roundEndTime - Time.time) + "";
            clockText2.text = clockText.text;

            if ((orderManager.orders.Count == 0 && numRoundOrdersLeft > 0) || (numRoundOrdersLeft > 0 && nextOrder < Time.time)){
                //make order

                int index = orderManager.newOrder(1);
                if(index != -1){
                    nextOrder += timeBetweenOrders;
                    numRoundOrdersLeft--;
                }
            }

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
            }
            if(roundEndTime < Time.time){
                gameRunning = false;
                roundRunning = false;
                roundCleared = false;
                //GAME Lost
                audioSrc.loop = false;
                audioSrc.Stop ();
                orderManager.clear();
            }
        }
        if(!audioSrc.isPlaying && !roundRunning) {
            audioSrc.clip = ambient;
            audioSrc.loop = true;
            audioSrc.Play();
        } 
    }

    public void startGame(){
        audioSrc.Stop();
        StartCoroutine(fadeIn(music));

        if (gameRunning && roundCleared) {
            newRound();
            return;
        }

        gameRunning = true;
        roundRunning = true;
        roundCleared = false;
        scoreManager.score = 0;
        level = 0;

        newRound();
        orderManager.clear();
    }

	IEnumerator fadeIn(AudioClip clip){
		audioSrc.clip = clip;
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
        level++;
        roundRunning = true;
        roundCleared = false;

        numRoundOrdersLeft = Mathf.RoundToInt(numOrderPerLevelCurve.Evaluate(level));
        timeBetweenOrders = timeBetweenOrderCurve.Evaluate(level); 
        roundStartTime = Time.time;
        roundBufferTime = roundBufferTimeCurve.Evaluate(level);
        roundEndTime = roundStartTime + (numRoundOrdersLeft * timeBetweenOrders) + roundBufferTime + startDelay;
        nextOrder = roundStartTime + startDelay;

    }
}

