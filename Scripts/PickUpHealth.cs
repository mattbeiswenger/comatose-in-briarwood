using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour {

	public Health playerHealth;
	public float health;
	public RectTransform healthBar;
	float maxHealth;
	// Use this for initialization
	void Start () {
		maxHealth = GameObject.Find ("Hunter_01").GetComponent<Health> ().maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col)
	{

		// I don't know why this code isn't working
		if (col.gameObject.name == "Hunter_01") 
		{
			//Find the health of the player object
			health = GameObject.Find ("Hunter_01").GetComponent<Health> ().currentHealth;

			//Should check to see if player has less than max health
			//If so, give max health
			if ( health < maxHealth) {
				GameObject.Find ("Hunter_01").GetComponent<Health> ().currentHealth = maxHealth;

				//Set the players health bar
				healthBar = GameObject.Find ("/PlayerHealthBarCanvas/HealthBarForeground").GetComponent<RectTransform>();
				healthBar.sizeDelta = new Vector2 (maxHealth, GameObject.Find ("Hunter_01").GetComponent<Health> ().origSDelta);
				healthBar.position = new Vector3 ( GameObject.Find ("Hunter_01").GetComponent<Health> ().origLocX,  GameObject.Find ("Hunter_01").GetComponent<Health> ().origLocY, GameObject.Find ("Hunter_01").GetComponent<Health> ().origLocZ);

				//destroys health
				Destroy (gameObject);
			}

			//If the player has max health, don't do anything.
		}

	}
}
