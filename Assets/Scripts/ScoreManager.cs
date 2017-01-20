using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {


    public int score = 0;

    private static ScoreManager instance;

    public void Awake(){
        if(instance != null)
            throw new UnityException("Muliple score manager found");

        instance = this;
    }

    public static ScoreManager getInstance(){
        return instance;
    }

}
