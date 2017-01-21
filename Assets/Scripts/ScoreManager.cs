using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    
    public int score = 0;
    public Text scoreText;

    private static ScoreManager instance;

    public void Awake(){
        if(instance != null)
            throw new UnityException("Multiple score manager found");

        instance = this;
    }
	
    void Update() {
        scoreText.text = score.ToString();
    }

    public static ScoreManager getInstance(){
        return instance;
    }

}
