using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bell : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;

    public void Start(){
        gameManager = GameManager.getInstance();
    }

    private void HandHoverUpdate(Valve.VR.InteractionSystem.Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            gameManager.startGame();

        }
    }
}
