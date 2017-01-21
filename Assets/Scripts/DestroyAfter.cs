using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
    public float seconds;

	void Start () {
        StartCoroutine(destroy(seconds));
	}

    IEnumerator destroy(float seconds){
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

