using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpSpawner : MonoBehaviour {

	public Health playerHealth;       // Reference to the player's heatlh.
	public GameObject HealthPickUp;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;
	public int Max;
	private int count = 0;
	public int i = 0;

	// Use this for initialization
	void Start () {

		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);

	}

	// Update is called once per frame
	void Spawn () {

		if(playerHealth.currentHealth <= 0f)
		{
			// ... exit the function.
			return;
		}

		if (count >= Max) {
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = count;


		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (HealthPickUp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

		count++;

	}
}

