using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bell : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public ScoreManager scoreManager;

    private AudioSource audioSrc;
	//Remember to put ind sounds if you need them
	public AudioClip[] bellSounds;

	public AudioClip[] startSounds;
	public AudioClip[] carlRoundSounds;
	public AudioClip[] jonasRoundSounds;
	public AudioClip[] endSounds;
	public AudioClip[] altSounds;


    public void Start(){
        gameManager = GameManager.getInstance();
        scoreManager = ScoreManager.getInstance();
		audioSrc = GetComponent<AudioSource>();
    }

	IEnumerator roundSound(){
		yield return new WaitForSeconds (audioSrc.clip.length);
		if (gameManager.level < 8) {
			if (startSounds.Length > 0) {
				int index = Random.Range (0, startSounds.Length);
				audioSrc.clip = startSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
					yield return new WaitForSeconds (audioSrc.clip.length);
				}
			}
			if (Random.Range (0, 2) == 0) {
				if (carlRoundSounds.Length > 0) {
					audioSrc.clip = carlRoundSounds [gameManager.level - 1];
					if (!audioSrc.isPlaying) {
						audioSrc.Play ();
						yield return new WaitForSeconds (audioSrc.clip.length);
					}
				}
			} else {
				if (jonasRoundSounds.Length > 0) {
					audioSrc.clip = jonasRoundSounds [gameManager.level - 1];
					if (!audioSrc.isPlaying) {
						audioSrc.Play ();
						yield return new WaitForSeconds (audioSrc.clip.length);
					}
				}
			}

			if (endSounds.Length > 0) {
				int index = Random.Range (0, endSounds.Length);
				audioSrc.clip = endSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}

		} else {
			if (altSounds.Length > 0) {
				int index = Random.Range (0, altSounds.Length);
				audioSrc.clip = altSounds [index];
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
			}
		}
		yield break;
	}

    private void HandHoverUpdate(Valve.VR.InteractionSystem.Hand hand)
    {

    }

    private void OnHandHoverBegin(Valve.VR.InteractionSystem.Hand hand)
    {
        gameManager.startGame();
        scoreManager.score = 0;
        if (bellSounds.Length > 0)
        {
            int index = Random.Range(0, bellSounds.Length);
            audioSrc.clip = bellSounds[index];
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
                StartCoroutine(roundSound());
            }
        }

    }
}
