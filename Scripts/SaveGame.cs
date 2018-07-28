using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour {
	public Transform Player;
	public float health = 189.0f;
	public RectTransform healthBar;
	public RectTransform animalBar;
	string createdSavedGame;
	public Transform DeathMenu;

	public GameObject bear;
	public GameObject deer;
	public GameObject wolf;
	public GameObject rabbit;

	public int bearsSpawned;
	public int deersSpawned;
	public int wolvesSpawned;
	public int rabbitsSpawned;

	public GameObject[] spawnedBs;
	public GameObject[] spawnedDs;
	public GameObject[] spawnedWs;
	public GameObject[] spawnedRs;

	public int BsSaved;
	public int DsSaved;
	public int WsSaved;
	public int RsSaved;

	float[,] savedBearPositions;
	float[,] savedDeerPositions;
	float[,] savedWolvesPositions;
	float[,] savedRabbitsPositions;

	float[,] savedBearHealth;
	float[,] savedDeerHealth;
	float[,] savedWolvesHealth;
	float[,] savedRabbitsHealth;

	void Awake(){
	    createdSavedGame = PlayerPrefs.GetString ("savedGame");

		if (createdSavedGame == "true") {
			Debug.Log ("Saved Game");
			//set the saved position
			Player.position = new Vector3 (PlayerPrefs.GetFloat ("x"), 
											PlayerPrefs.GetFloat ("y"), 
											PlayerPrefs.GetFloat ("z"));
			Player.eulerAngles = new Vector3 (0, PlayerPrefs.GetFloat ("RotY"), 0);

			//set the saved health 
			health = PlayerPrefs.GetFloat ("health");

		    //set the saved rectangle
			healthBar = GameObject.Find ("/PlayerHealthBarCanvas/HealthBarForeground").GetComponent<RectTransform>();

			healthBar.sizeDelta = new Vector2 (health, PlayerPrefs.GetFloat ("rectDeltaY"));
			Debug.Log ("saved locX" + PlayerPrefs.GetFloat ("locX"));
			healthBar.position = new Vector3 (PlayerPrefs.GetFloat ("locX"), PlayerPrefs.GetFloat ("locY"), PlayerPrefs.GetFloat ("locZ"));


			//respawn all saved animals	
			spawnedBs = new GameObject[100];
			BsSaved = PlayerPrefs.GetInt ("BearsSaved") - 1;
			Debug.Log ("BsSaved: " + BsSaved);
			spawnedDs = new GameObject[100];
			DsSaved = PlayerPrefs.GetInt ("DeerSaved") - 1;
			spawnedRs = new GameObject[100];
			RsSaved = PlayerPrefs.GetInt ("WolvesSaved") - 1;
			spawnedWs = new GameObject[100];
			WsSaved = PlayerPrefs.GetInt ("RabbitsSaved") - 1;
		

			//add each saved enemy at their position and set their health
			for (int i = 0; i <= BsSaved; i++) {
				//instantiate each bear	
				GameObject clone;
				clone = Instantiate (bear, new Vector3 (PlayerPrefs.GetFloat ("Bear" + i + "X"), 
														PlayerPrefs.GetFloat ("Bear" + i + "Y"), 
														PlayerPrefs.GetFloat ("Bear" + i + "Z")), 
										Quaternion.Euler (0, PlayerPrefs.GetFloat ("Bear" + i + "RotY"), 0));
			
				clone.GetComponent<AnimalHealth> ().currentHealth = PlayerPrefs.GetFloat ("Bear"+  i + "health");
				clone.GetComponent<AnimalHealth> ().objectRectTransform.sizeDelta = new Vector2 (PlayerPrefs.GetFloat ("Bear"+  i + "health"), 
																								PlayerPrefs.GetFloat ("Bear"+  i + "rectDeltaY"));
				
				clone.GetComponent<AnimalHealth> ().objectRectTransform.position = new Vector3 (PlayerPrefs.GetFloat ("Bear"+  i + "locX"), 
																								PlayerPrefs.GetFloat ("Bear"+  i + "locY"), 
																								PlayerPrefs.GetFloat ("Bear"+  i + "locZ"));

				if (clone.GetComponent<AnimalHealth> ().isDead)
					Debug.Log ("dead bear instantiated");


				spawnedBs [i] = clone;
				Debug.Log ("bear created" + clone);
			}
				
			for(int i = 0; i <= DsSaved; i++){		
				//instantiate each deer
				GameObject clone;
				clone = Instantiate (deer, new Vector3 (PlayerPrefs.GetFloat ("Deer"+  i + "X"), 
														PlayerPrefs.GetFloat ("Deer"+  i + "Y"), 
														PlayerPrefs.GetFloat ("Deer"+  i + "Z")), 
											Quaternion.Euler (0, PlayerPrefs.GetFloat ("Deer"+  i + "RotY"), 0));

				clone.GetComponent<AnimalHealth> ().currentHealth = PlayerPrefs.GetFloat ("Deer"+  i + "health");
				clone.GetComponent<AnimalHealth> ().objectRectTransform.sizeDelta = new Vector2 (PlayerPrefs.GetFloat ("Deer"+  i + "health"), 
																								PlayerPrefs.GetFloat ("Deer"+  i + "rectDeltaY"));
				
				clone.GetComponent<AnimalHealth> ().objectRectTransform.position = new Vector3 (PlayerPrefs.GetFloat ("Deer"+  i + "locX"), 
																								PlayerPrefs.GetFloat ("Deer"+  i + "locY"), 
																								PlayerPrefs.GetFloat ("Deer"+  i + "locZ"));

				spawnedDs [i] = clone;
				Debug.Log ("deer created" + clone);
			}

			for(int i = 0; i <= WsSaved; i++){		
				//instantiate each wolf
				GameObject clone;
				clone = Instantiate (wolf, new Vector3 (PlayerPrefs.GetFloat ("Wolves"+  i + "X"), 
														PlayerPrefs.GetFloat ("Wolves"+  i + "Y"), 
														PlayerPrefs.GetFloat ("Wolves"+  i + "Z")), 
										Quaternion.Euler (0, PlayerPrefs.GetFloat ("Wolves"+  i + "RotY"), 0));

				clone.GetComponent<AnimalHealth> ().currentHealth = PlayerPrefs.GetFloat ("Wolves"+  i + "health");
				clone.GetComponent<AnimalHealth> ().objectRectTransform.sizeDelta = new Vector2 (PlayerPrefs.GetFloat ("Wolves"+  i + "health"), 
																								PlayerPrefs.GetFloat ("Wolves"+  i + "rectDeltaY"));
				
				clone.GetComponent<AnimalHealth> ().objectRectTransform.position = new Vector3 (PlayerPrefs.GetFloat ("Wolves"+  i + "locX"), 
																								PlayerPrefs.GetFloat ("Wolves"+  i + "locY"), 
																								PlayerPrefs.GetFloat ("Wolves"+  i + "locZ"));

				spawnedWs [i] = clone;
				Debug.Log ("wolf created" + clone);
			}

			for(int i = 0; i <= RsSaved; i++){			
				//instantiate each rabbit
				GameObject clone;
				clone = Instantiate (rabbit, new Vector3 (PlayerPrefs.GetFloat ("Rabbits"+  i + "X"), 
														 PlayerPrefs.GetFloat ("Rabbits"+  i + "Y"), 
														 PlayerPrefs.GetFloat ("Rabbits"+  i + "Z")), 
											Quaternion.Euler (0, PlayerPrefs.GetFloat ("Rabbits"+  i + "RotY"), 0));

				clone.GetComponent<AnimalHealth> ().currentHealth = PlayerPrefs.GetFloat ("Rabbits"+  i + "health");
				clone.GetComponent<AnimalHealth> ().objectRectTransform.sizeDelta = new Vector2 (PlayerPrefs.GetFloat ("Rabbits"+  i + "health"), 
																								PlayerPrefs.GetFloat ("Rabbits"+  i + "rectDeltaY"));
				
				clone.GetComponent<AnimalHealth> ().objectRectTransform.position = new Vector3 (PlayerPrefs.GetFloat ("Rabbits"+  i + "locX"), 
																								PlayerPrefs.GetFloat ("Rabbits"+  i + "locY"), 
																								PlayerPrefs.GetFloat ("Rabbits"+  i + "locZ"));

				spawnedRs [i] = clone;
				Debug.Log ("rabbit created" + clone);
			}

		
		} else {
			Debug.Log ("New Game Created");
		}
	
	}



	public void SaveTheGame(){
		savePlayer ();
		saveEnemies ();
	}

	public void restart(){
		Debug.Log ("Game Restarted");
		DeathMenu.gameObject.SetActive(false);
		PlayerPrefs.SetString ("savedGame", "false");
		Time.timeScale = 1;
		SceneManager.LoadScene(1);
	}

	public void ExitTheGame(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
		

	void savePlayer(){
		//save the player's position
		PlayerPrefs.SetFloat ("x", Player.position.x);
		PlayerPrefs.SetFloat ("y", Player.position.y);
		PlayerPrefs.SetFloat ("z", Player.position.z);

		//save the direction the player is looking
		PlayerPrefs.SetFloat ("RotY", Player.eulerAngles.y);

		//get the current health of the player
		health = GameObject.Find ("Hunter_01").GetComponent<Health> ().currentHealth;

		//save player's health
		PlayerPrefs.SetFloat ("health", health);

		//get a reference to the current health bar
		healthBar = GameObject.Find ("PlayerHealthBarCanvas/HealthBarBackground").GetComponent<RectTransform>();

		//save the rectangle's location data
		PlayerPrefs.SetFloat ("rectDeltaY", GameObject.Find ("Hunter_01").GetComponent<Health> ().sDelta);
		PlayerPrefs.SetFloat ("locX",  GameObject.Find ("Hunter_01").GetComponent<Health> ().locX);
		PlayerPrefs.SetFloat ("locY", GameObject.Find ("Hunter_01").GetComponent<Health> ().locY);
		PlayerPrefs.SetFloat ("locZ", GameObject.Find ("Hunter_01").GetComponent<Health> ().locZ);
	}


	void saveEnemies(){
		//get the enemypositions and health into an accesible array
		TrackEnemies track = gameObject.GetComponent<TrackEnemies> ();
		track.getLocationAndHealth ();

		//get the total number spawned
		bearsSpawned = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().numBears;
		Debug.Log ("bear bearsSpawned = " + bearsSpawned);
		deersSpawned = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().numDeer;
		wolvesSpawned = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().numWolves;
		rabbitsSpawned = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().numRabbits;

		//save the total number spawned
		PlayerPrefs.SetInt ("BearsSaved", bearsSpawned);
		PlayerPrefs.SetInt ("DeerSaved", deersSpawned);
		PlayerPrefs.SetInt ("WolvesSaved", wolvesSpawned);
		PlayerPrefs.SetInt ("RabbitsSaved", rabbitsSpawned);

		//grab the 2d arrays with the positions
		savedBearPositions = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().bearPositions;
		savedDeerPositions = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().deerPositions;
		savedWolvesPositions = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().wolvesPositions;
		savedRabbitsPositions = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().rabbitsPositions;

		//get 2d arrays with the health
		savedBearHealth = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().bearHealth;
		savedDeerHealth = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().deerHealth;
		savedWolvesHealth = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().wolvesHealth;
		savedRabbitsHealth  = GameObject.Find ("Game Controller").GetComponent<TrackEnemies> ().rabbitsHealth;


	/* -------------------- save enemy positions/health in game -------------------- */
	
		//save the x,y,z positions and euler angle y  of bears in playerPrefs
		for (int i = 0; i <= bearsSpawned; i++) {
			PlayerPrefs.SetFloat ("Bear" + i + "X", savedBearPositions [i, 0]);
			PlayerPrefs.SetFloat ("Bear" + i + "Y", savedBearPositions [i, 1]);
			PlayerPrefs.SetFloat ("Bear" + i + "Z", savedBearPositions [i, 2]);
			PlayerPrefs.SetFloat ("Bear" + i + "RotY", savedBearPositions [i, 3]);

			PlayerPrefs.SetFloat ("Bear"+ i + "health", savedBearHealth[i,0]);
			PlayerPrefs.SetFloat ("Bear"+ i + "rectDeltaY", savedBearHealth[i,1]);
			PlayerPrefs.SetFloat ("Bear"+ i + "locX", savedBearHealth[i,2]);
			PlayerPrefs.SetFloat ("Bear"+ i + "locY", savedBearHealth[i,3]);
			PlayerPrefs.SetFloat ("Bear"+ i + "locZ", savedBearHealth[i,4]);
		}
			
		//save the x,y,z positions and euler angle y of deers in playerPrefs
		for (int i = 0; i <= deersSpawned; i++) {
			PlayerPrefs.SetFloat ("Deer"+ i + "X", savedDeerPositions[i,0]);
			PlayerPrefs.SetFloat ("Deer"+ i + "Y", savedDeerPositions[i,1]);
			PlayerPrefs.SetFloat ("Deer"+ i + "Z", savedDeerPositions[i,2]);
			PlayerPrefs.SetFloat ("Deer"+ i + "RotY", savedDeerPositions[i,3]);

			PlayerPrefs.SetFloat ("Deer"+ i + "health", savedDeerHealth[i,0]);
			PlayerPrefs.SetFloat ("Deer"+ i + "rectDeltaY", savedDeerHealth[i,1]);
			PlayerPrefs.SetFloat ("Deer"+ i + "locX", savedDeerHealth[i,2]);
			PlayerPrefs.SetFloat ("Deer"+ i + "locY", savedDeerHealth[i,3]);
			PlayerPrefs.SetFloat ("Deer"+ i + "locZ", savedDeerHealth[i,4]);
		}
			
		//save the x,y,z positions and euler angle y of deers in playerPrefs
		for (int i = 0; i <= wolvesSpawned; i++) {
			PlayerPrefs.SetFloat ("Wolves"+ i + "X", savedWolvesPositions[i,0]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "Y", savedWolvesPositions[i,1]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "Z", savedWolvesPositions[i,2]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "RotY", savedWolvesPositions[i,3]);

			PlayerPrefs.SetFloat ("Wolves"+ i + "health", savedWolvesHealth[i,0]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "rectDeltaY", savedWolvesHealth[i,1]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "locX", savedWolvesHealth[i,2]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "locY", savedWolvesHealth[i,3]);
			PlayerPrefs.SetFloat ("Wolves"+ i + "locZ", savedWolvesHealth[i,4]);
		}
			
		//save the x,y,z positions and euler angle y of deers in playerPrefs
		for (int i = 0; i <= rabbitsSpawned; i++) {
			PlayerPrefs.SetFloat ("Rabbits"+ i + "X", savedRabbitsPositions[i,0]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "Y", savedRabbitsPositions[i,1]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "Z", savedRabbitsPositions[i,2]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "RotY", savedRabbitsPositions[i,3]);

			PlayerPrefs.SetFloat ("Rabbits"+ i + "health", savedRabbitsHealth[i,0]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "rectDeltaY", savedRabbitsHealth[i,1]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "locX", savedRabbitsHealth[i,2]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "locY", savedRabbitsHealth[i,3]);
			PlayerPrefs.SetFloat ("Rabbits"+ i + "locZ", savedRabbitsHealth[i,4]);
		}


	}
}