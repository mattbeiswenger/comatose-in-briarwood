using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HunterScript : MonoBehaviour {

	public Transform Player;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	Animator animator;

	//var for double tap 
	private int tapCount = 0;
	private float tapCooler = 0.5f;
	bool runFlag = false;
	bool walkBack = false;
	bool animalAttacking = false;

	EnemyManager em;
	Health health;

	public float elapsedTime;


	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		animator = GetComponent<Animator>();
		elapsedTime = 0f;
		health = GameObject.Find("Hunter_01").GetComponent<Health>();
	}


	// Update is called once per frame
	void Update ()
	{

		animalAttacking = false;

		em = GameObject.Find ("EnemyManagerD").GetComponent<EnemyManager>();
		GameObject[] deers = em.spawnedEnemies;
		foreach (GameObject deer in deers) {
			if (deer != null && !deer.GetComponent<AnimalHealth> ().isDead) {
				affectHealth (deer.GetComponent<FollowPlayer> ().inAttackZone);
				if (!animalAttacking) {
					animalAttacking = deer.GetComponent<FollowPlayer> ().inAttackZone;
				}
			}

		}

		em = GameObject.Find ("EnemyManagerR").GetComponent<EnemyManager>();
		GameObject[] rabbits = em.spawnedEnemies;
		foreach (GameObject rabbit in rabbits) {
			if (rabbit != null && !rabbit.GetComponent<AnimalHealth> ().isDead) {
				affectHealth (rabbit.GetComponent<FollowPlayer> ().inAttackZone);
				// if we have already found an animal that is attacking then animal attacking
				// is set to true and we continue the loop. if we haven't, then we keep searching
				// to see if there is an animal that is attacking
				if (!animalAttacking) {
					animalAttacking = rabbit.GetComponent<FollowPlayer> ().inAttackZone;
				}	
			}
		}

		em = GameObject.Find ("EnemyManagerB").GetComponent<EnemyManager>();
		GameObject[] bears = em.spawnedEnemies;
		foreach (GameObject bear in bears) {
			if (bear != null && !bear.GetComponent<AnimalHealth> ().isDead) {
				affectHealth (bear.GetComponent<FollowPlayer> ().inAttackZone);
				if (!animalAttacking) {
					animalAttacking = bear.GetComponent<FollowPlayer> ().inAttackZone;
				}
			}
		}

		em = GameObject.Find ("EnemyManagerW").GetComponent<EnemyManager>();
		GameObject[] wolves = em.spawnedEnemies;
		foreach (GameObject wolf in wolves) {
			if (wolf != null && !wolf.GetComponent<AnimalHealth> ().isDead) {
				affectHealth (wolf.GetComponent<FollowPlayer> ().inAttackZone);
				if (!animalAttacking) {
					animalAttacking = wolf.GetComponent<FollowPlayer> ().inAttackZone;
				}
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			playAttacks(animalAttacking);
		}

		
		// -- player can only be walking forwards, walking backwards, or running forwards
		/*No key is being pressed so the animation idle is played*/
		if (Input.anyKey == false) {

			animator.Play ("HumanoidIdle");
			runFlag = false;
		

		} else if (Input.GetKeyDown (KeyCode.W)) {

			//W keys is pressed so walking animation plays
			//if it is double tapped he runs
			if (tapCount == 1) {
				animator.Play ("HumanoidRun");
				runFlag = true;
			} else {
				animator.Play ("HumanoidWalk");
				tapCount += 1;
				tapCooler = 0.5f;
				walkBack = false;
			}

		} else if (Input.GetKeyDown (KeyCode.S) && !runFlag) {

			//S keys is pressed so walking backwards animation plays
			animator.Play ("HumanoidWalkBack");
			walkBack = true;

		} 


		//control doubleTap
		if (tapCooler > 0) {
			tapCooler -= 1 * Time.deltaTime;
		} else {
			tapCount = 0;
		}


		// ---- turn while running ---- //
		//If the animator is playing the Humanoid Run animation and the D key is pressed, play the Run Right animation
		if (runFlag && Input.GetKey (KeyCode.D) )
			animator.Play ("HumanoidRunRight");

		//If the animator is playing the Humanoid Run animation and the A key is pressed, play the Run Left animation
		if (runFlag && Input.GetKey (KeyCode.A) )
			animator.Play ("HumanoidRunLeft");



		// ---- turn while walking backwards ---- //
		//If the animator is playing the Humanoid Walk animation and the A key is pressed, play the Walk Backwards Left animation
		if (Input.GetKeyDown (KeyCode.A) && !runFlag && walkBack)
			animator.Play ("HumanoidWalkLeftBack");

		//If the animator is playing the Humanoid Walk animation and the D key is pressed, play the Walk Backwards Right animation
		if (Input.GetKeyDown (KeyCode.D) && !runFlag && walkBack)
			animator.Play ("HumanoidWalkRightBack");



		// ---- turn while walking forwards ---- //
		//A keys is pressed so walking left animation plays
		if (Input.GetKeyDown (KeyCode.A) && !runFlag && !walkBack) {
			animator.Play ("HumanoidWalkLeft");	
		}

		//D keys is pressed so walking right animation plays
		if (Input.GetKey (KeyCode.D) && !runFlag && !walkBack) {
			animator.Play ("HumanoidWalkRight");
		}



		//  -- allow character to continue running/walking forward when left/right keys are unpressed  --  //

		// ---- running ---- //
		if (Input.GetKeyUp (KeyCode.D) && runFlag)
			animator.Play ("HumanoidRun");
		
		if (Input.GetKeyUp (KeyCode.A) && runFlag)
			animator.Play ("HumanoidRun");


		// ---- walking backwards ---- //
		if (Input.GetKeyUp (KeyCode.A) && !runFlag && walkBack)
			animator.Play ("HumanoidWalkBack");
		
		if (Input.GetKeyUp (KeyCode.D) && !runFlag && walkBack)
			animator.Play ("HumanoidWalkBack");


		// ---- walking forwards ---- //
		if (Input.GetKeyUp (KeyCode.A) && !runFlag && !walkBack) 
			animator.Play ("HumanoidWalk");	
			
		if (Input.GetKeyUp (KeyCode.D) && !runFlag && !walkBack) 
			animator.Play ("HumanoidWalk");

	}

	public void affectHealth(bool animalAttack) {
		if (animalAttack) {
			// starting on initial attack, the player will lose health every one second
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= 3.0f) {
				// deal 10 percent damage
				health.TakeDamage (.10f);
				elapsedTime = 0.0f;
			}
		}
	}



	public void playAttacks(bool animalAttack) {
		if (Input.GetMouseButtonDown (0)) {
			animator.Play ("shoot");
			Fire ();
		}
	}


	public void Fire(){
		//create the bullet from the bullet prefab
		var bullet = (GameObject)Instantiate (
			             bulletPrefab,
			             bulletSpawn.position,
			             bulletSpawn.rotation);

		//add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;

		//destory the bullet after 5 seconds
		Destroy(bullet, 5.0f);
	}






}