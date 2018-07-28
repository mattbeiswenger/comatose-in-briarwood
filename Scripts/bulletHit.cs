using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHit : MonoBehaviour {
	Health health;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("Bullet hit " + col.gameObject.name);

		// I don't know why this code isn't working
		if (col.gameObject.name == "Bear(Clone)") {
			col.gameObject.GetComponent<AnimalHealth>().TakeDamage(.10f);
		}

		if (col.gameObject.name == "Wolf(Clone)") {
			col.gameObject.GetComponent<AnimalHealth>().TakeDamage(.10f);
		}

		if (col.gameObject.name == "Rabbit(Clone)") {
			col.gameObject.GetComponent<AnimalHealth>().TakeDamage(.10f);
		}

		if (col.gameObject.name == "Deer(Clone)") {
			col.gameObject.GetComponent<AnimalHealth>().TakeDamage(.10f);
		}

		Destroy (gameObject); // destroys bullet
	}

}
