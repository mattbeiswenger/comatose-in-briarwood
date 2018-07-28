using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackEnemies : MonoBehaviour {
	public GameObject bear;
	public GameObject deer;
	public GameObject wolf;
	public GameObject rabbit;

	public GameObject[] spawnedBears;
	public GameObject[] spawnedDeer;
	public GameObject[] spawnedWolves;
	public GameObject[] spawnedRabbits;

	public float[,] bearPositions;
	public float[,] deerPositions;
	public float[,] wolvesPositions;
	public float[,] rabbitsPositions;

	public float[,] bearHealth;
	public float[,] deerHealth;
	public float[,] wolvesHealth;
	public float[,] rabbitsHealth;

	public bool[] bearDeaths;
	public bool[] deerDeaths;
	public bool[] wolfDeaths;
	public bool[] rabbitDeaths;

	public int numBears;
	public int numDeer;
	public int numWolves;
	public int numRabbits;

	public int maxBears;
	public int maxDeer;
	public int maxRabbits;
	public int maxWolves;

	bool everyoneDead = false;
	public Transform canvas;


	// Use this for initialization
	void Start () {
		bearPositions = new float[100,4];
		deerPositions = new float[100,4];
		wolvesPositions = new float[100,4];
		rabbitsPositions = new float[100,4];

		bearHealth = new float[100,5];
		deerHealth = new float[100,5];
		wolvesHealth = new float[100,5];
		rabbitsHealth = new float[100,5];

		bearDeaths = new bool[100];
		deerDeaths = new bool[100];
		wolfDeaths = new bool[100];
		rabbitDeaths = new bool[100];

		if (PlayerPrefs.GetString ("savedGame") == "true") {
			//grab all the saved data from the SaveGame script
			numBears = GameObject.Find ("Game Controller").GetComponent<SaveGame> ().BsSaved;
			spawnedBears =  GameObject.Find ("EnemyManagerB").GetComponent<EnemyManager> ().spawnedEnemies;

			numDeer = GameObject.Find ("Game Controller").GetComponent<SaveGame> ().DsSaved;
			numWolves = GameObject.Find ("Game Controller").GetComponent<SaveGame> ().WsSaved;
			numRabbits = GameObject.Find ("Game Controller").GetComponent<SaveGame> ().RsSaved;
			spawnedDeer = GameObject.Find ("EnemyManagerD").GetComponent<EnemyManager> ().spawnedEnemies;
			spawnedWolves = GameObject.Find ("EnemyManagerW").GetComponent<EnemyManager> ().spawnedEnemies;
			spawnedRabbits = GameObject.Find ("EnemyManagerR").GetComponent<EnemyManager> ().spawnedEnemies;

		} else { 
			//if no saved game set the number to EnemyManager's count (which should be 0)
			numBears = GameObject.Find ("EnemyManagerB").GetComponent<EnemyManager> ().count;
			numDeer = GameObject.Find ("EnemyManagerD").GetComponent<EnemyManager> ().count;
			numRabbits = GameObject.Find ("EnemyManagerR").GetComponent<EnemyManager> ().count;
			numWolves = GameObject.Find ("EnemyManagerW").GetComponent<EnemyManager> ().count;
		}
	}


	// Update is called once per frame
	void Update () {
		everyoneDead = true;
		//get all of the spawned enamies from each manager and the total number that will be spawned
		spawnedBears = GameObject.Find ("EnemyManagerB").GetComponent<EnemyManager> ().spawnedEnemies;
		spawnedDeer = GameObject.Find ("EnemyManagerD").GetComponent<EnemyManager> ().spawnedEnemies;
		spawnedRabbits = GameObject.Find ("EnemyManagerR").GetComponent<EnemyManager> ().spawnedEnemies;
		spawnedWolves = GameObject.Find ("EnemyManagerW").GetComponent<EnemyManager> ().spawnedEnemies;

		maxBears = GameObject.Find ("EnemyManagerB").GetComponent<EnemyManager> ().Max;
		maxDeer = GameObject.Find ("EnemyManagerD").GetComponent<EnemyManager> ().Max;
		maxRabbits = GameObject.Find ("EnemyManagerR").GetComponent<EnemyManager> ().Max;
		maxWolves = GameObject.Find ("EnemyManagerW").GetComponent<EnemyManager> ().Max;


		if ((spawnedBears.Length == maxBears) && (spawnedDeer.Length == maxDeer) && (spawnedRabbits.Length == maxRabbits) && (spawnedWolves.Length == maxWolves)) {
			//if any enemy is alive set the everyoneDead to false
			for (int i = 0; i < spawnedBears.Length; i++) {
				if (spawnedBears[i] == null || !spawnedBears[i].GetComponent<AnimalHealth> ().isDead) {
					everyoneDead = false;
				}
			}
			for (int i = 0; i < spawnedDeer.Length; i++) {
				if (spawnedDeer[i] == null || !spawnedDeer[i].GetComponent<AnimalHealth> ().isDead) {
					everyoneDead = false;
				}
			}
			for (int i = 0; i < spawnedRabbits.Length; i++) {
				if (spawnedRabbits[i] == null || !spawnedRabbits[i].GetComponent<AnimalHealth> ().isDead) {
					everyoneDead = false;
				}
			}
			for (int i = 0; i < spawnedWolves.Length; i++) {
				if (spawnedWolves[i] == null || !spawnedWolves[i].GetComponent<AnimalHealth> ().isDead) {
					everyoneDead = false;
				}
			}

			//if everyoneDead is true then no animal is left alive, so the player wins
			if (everyoneDead) {
			
				Debug.Log ("Everyone dead");

				if(canvas.gameObject.activeInHierarchy == false)
				{
					Cursor.visible = true;
					//display the canvas
					canvas.gameObject.SetActive(true);

					//freeze the game
					Time.timeScale = 0;

					//disable shooting
					GameObject.Find ("Hunter_01").GetComponent<HunterScript>().enabled = false;  

					//disable mouselook
					GameObject.Find ("Hunter_01").GetComponent<MouseLook>().enabled = false; 
				}

			}
		}
	}



	//called when player presses save button
	public void getLocationAndHealth()
	{

		 numBears = GameObject.Find ("EnemyManagerB").GetComponent<EnemyManager> ().count;
		 numDeer = GameObject.Find ("EnemyManagerD").GetComponent<EnemyManager> ().count;
		 numRabbits = GameObject.Find ("EnemyManagerR").GetComponent<EnemyManager> ().count;
		 numWolves = GameObject.Find ("EnemyManagerW").GetComponent<EnemyManager> ().count;

   /* --------- get the location and direction of each animal and save is to an array --------- */
		//get health and location of all spawned bears
		for(int i = 0; i < numBears; i++) {
			if (spawnedBears [i] == null)
				break;
			
			bearPositions[i,0] = spawnedBears [i].transform.position.x;
			bearPositions[i,1] = spawnedBears [i].transform.position.y;
			bearPositions[i,2] = spawnedBears [i].transform.position.z;
			bearPositions[i,3] = spawnedBears [i].transform.eulerAngles.y;

			bearHealth[i,0] = spawnedBears [i].GetComponent<AnimalHealth> ().currentHealth;
			bearHealth[i,1] = spawnedBears [i].GetComponent<AnimalHealth> ().sDelta;
			bearHealth[i,2] = spawnedBears [i].GetComponent<AnimalHealth> ().locX;
			bearHealth[i,3] = spawnedBears [i].GetComponent<AnimalHealth> ().locY;
			bearHealth[i,4] = spawnedBears [i].GetComponent<AnimalHealth> ().locZ;

			bearDeaths[i] = spawnedBears [i].GetComponent<AnimalHealth> ().isDead;
		}


		//get health and location of all spawned deer
		for (int d = 0; d < numDeer; d++) {
			if (spawnedDeer [d] == null)
				break;

			deerPositions[d,0] = spawnedDeer [d].transform.position.x;
			deerPositions[d,1] = spawnedDeer [d].transform.position.y;
			deerPositions[d,2] = spawnedDeer [d].transform.position.z;
			deerPositions[d,3] = spawnedDeer [d].transform.eulerAngles.y;

			deerHealth[d,0] = spawnedDeer [d].GetComponent<AnimalHealth> ().currentHealth;
			deerHealth[d,1] = spawnedDeer [d].GetComponent<AnimalHealth> ().sDelta;
			deerHealth[d,2] = spawnedDeer [d].GetComponent<AnimalHealth> ().locX;
			deerHealth[d,3] = spawnedDeer [d].GetComponent<AnimalHealth> ().locY;
			deerHealth[d,4] = spawnedDeer [d].GetComponent<AnimalHealth> ().locZ;

			deerDeaths[d] = spawnedDeer [d].GetComponent<AnimalHealth> ().isDead;

		}



		//get health and location of all spawned wolves
		for (int w = 0; w < numDeer; w++) {
			if (spawnedWolves [w] == null)
				break;
			
			wolvesPositions[w,0] = spawnedWolves [w].transform.position.x;
			wolvesPositions[w,1] = spawnedWolves [w].transform.position.y;
			wolvesPositions[w,2] = spawnedWolves [w].transform.position.z;
			wolvesPositions[w,3] = spawnedWolves [w].transform.eulerAngles.y;

		    wolvesHealth[w,0] = spawnedWolves [w].GetComponent<AnimalHealth> ().currentHealth;
			wolvesHealth[w,1] = spawnedWolves [w].GetComponent<AnimalHealth> ().sDelta;
			wolvesHealth[w,2] = spawnedWolves [w].GetComponent<AnimalHealth> ().locX;
			wolvesHealth[w,3] = spawnedWolves [w].GetComponent<AnimalHealth> ().locY;
			wolvesHealth[w,4] = spawnedWolves [w].GetComponent<AnimalHealth> ().locZ;

			wolfDeaths[w] = spawnedWolves [w].GetComponent<AnimalHealth> ().isDead;

		}


		//get health and location of all spawned rabbits
		for (int r = 0; r < numDeer; r++) {
			if (spawnedRabbits [r] == null)
				break;
			
			rabbitsPositions[r,0] = spawnedRabbits [r].transform.position.x;
			rabbitsPositions[r,1] = spawnedRabbits [r].transform.position.y;
			rabbitsPositions[r,2] = spawnedRabbits [r].transform.position.z;
			rabbitsPositions[r,3] = spawnedRabbits [r].transform.eulerAngles.y;

			rabbitsHealth[r,0] = spawnedRabbits [r].GetComponent<AnimalHealth> ().currentHealth;
			rabbitsHealth[r,1] = spawnedRabbits [r].GetComponent<AnimalHealth> ().sDelta;
			rabbitsHealth[r,2] = spawnedRabbits [r].GetComponent<AnimalHealth> ().locX;
			rabbitsHealth[r,3] = spawnedRabbits [r].GetComponent<AnimalHealth> ().locY;
			rabbitsHealth[r,4] = spawnedRabbits [r].GetComponent<AnimalHealth> ().locZ;

			rabbitDeaths[r] = spawnedRabbits [r].GetComponent<AnimalHealth> ().isDead;

		}

	}

}
