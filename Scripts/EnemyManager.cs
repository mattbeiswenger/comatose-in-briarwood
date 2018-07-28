using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public GameObject bear;
	public GameObject deer;
	public GameObject wolf;
	public GameObject rabbit;

	public GameObject healthBar;

	public Health playerHealth;       // Reference to the player's heatlh.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;
	public int Max;
	public int count = 0;
	public GameObject[] spawnedEnemies;
	public int i = 0;

	// Use this for initialization
	void Start () {

		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		spawnedEnemies = new GameObject[Max];

		if (PlayerPrefs.GetString ("savedGame") == "true") {

			if (this.name == "EnemyManagerB") {
				spawnedEnemies =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().spawnedBs;
				count =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().BsSaved;
				count++;
				i = count;
				Debug.Log ("bear count = " + count);
			}

			if (this.name == "EnemyManagerD") {
				spawnedEnemies =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().spawnedDs;
				count =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().DsSaved;
				count++;
				i = count;
			}

			if (this.name == "EnemyManagerR") {
				spawnedEnemies =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().spawnedRs;
				count =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().RsSaved;
				count++;
				i = count;
			}

			if (this.name == "EnemyManagerW") {
				spawnedEnemies =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().spawnedWs;
				count =  GameObject.Find ("Game Controller").GetComponent<SaveGame> ().WsSaved;
				count++;
				i = count;
			}

		} 
		
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
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);


		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		GameObject clone;
		clone = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;

		clone.GetComponent<AnimalHealth> ().currentHealth = clone.GetComponent<AnimalHealth> ().maxHealth; //health is the width of the element

		spawnedEnemies [i] = clone; 

		i++;
		Debug.Log ("count: " + count);
		count++;
		
	}
}
