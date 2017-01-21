using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bell : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;

	private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] bellSounds;

    public void Start(){
        gameManager = GameManager.getInstance();
		audioSrc = GetComponent<AudioSource>();
    }

    private void HandHoverUpdate(Valve.VR.InteractionSystem.Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            gameManager.startGame();
			if (bellSounds.Length > 0) {
				int index = Random.Range (0, bellSounds.Length);
				audioSrc.clip = bellSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}

        }
    }
}
