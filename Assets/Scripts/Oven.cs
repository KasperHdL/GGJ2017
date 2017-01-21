using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    public bool _doorClosed = false;
	public List<Pizza> pizzasInOven;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void doorOpened()
    {
        _doorClosed = false;
        for (int i = 0; i < pizzasInOven.Count; i++)
        {
            pizzasInOven[i].interactable.disable = false;
        }
    }
	public void doorClosed(){
        _doorClosed = true;
		for (int i = pizzasInOven.Count - 1;i > -1;i--) {
            if (pizzasInOven[i] == null)
                pizzasInOven.RemoveAt(i);
            else
            {
                pizzasInOven[i].GetComponent<Pizza>().cook();
                pizzasInOven[i].interactable.disable = true;
            }
        }
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Pizza")) {
			pizzasInOven.Add (other.gameObject.GetComponent<Pizza>());
		}
	}

	void OnTriggerExit(Collider other){
        if (other.tag == "Pizza")
        {
            Pizza p = other.gameObject.GetComponent<Pizza>();
            if (pizzasInOven.Contains(p))
            {
                pizzasInOven.Remove(p);
            }
        }
	}
}
