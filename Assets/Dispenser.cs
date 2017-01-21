using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {
    private bool dispensed = false;

    public Transform dispensePoint;
    public Transform resetPoint;
    public Transform pizzaSlot;

    public GameObject pizzaPrefab;


	// Update is called once per frame
	void Update () {
		if (transform.position.y < dispensePoint.position.y && !dispensed)
        {
            GameObject pizza = Instantiate(pizzaPrefab, pizzaSlot.position, Quaternion.identity);
            dispensed = true;
        }
        if (transform.position.y > resetPoint.position.y)
        {
            dispensed = false;
        }
	}
}
