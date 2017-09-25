using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour {
	public float timeBeforeRespawn;
	private Vector3 originPosition;
	private Quaternion originRotation;
	private float floorTimer;
	private bool onFloor;
	private void Start() 
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
	}

	private void Update() 
	{
		if (!onFloor) 
		{
			floorTimer = 0;
			return;
		}
		else 
		{
			floorTimer += Time.deltaTime;
		}
		if (floorTimer > timeBeforeRespawn) 
		{
			transform.position = originPosition;
			transform.rotation = originRotation;
		}
	}

	private void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.tag == "Floor") 
		{
			onFloor = true;
		}
	}

	private void OnCollisionExit(Collision other) 
	{
		if (other.gameObject.tag == "Floor") 
		{
			onFloor = false;
		}
	}
}
