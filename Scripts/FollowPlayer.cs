using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour {

	public GameObject Player;
	int MoveSpeed = 4;
	public float AttDist = 1.0f;
	public bool inAttackZone = false;
	bool animalDead = false;

	Animator animator;

	void Start()
	{
		if (gameObject.name == "Wolf(Clone)") {
			AttDist = 1.5f;
		}
		animator = GetComponent<Animator>();
		Player = GameObject.Find ("Hunter_01");
		Debug.Log ("attack dist" + AttDist);
	}

	void Update()
	{
		// Update is called once per frame
		if (!animalDead) {
			transform.LookAt(Player.transform);
		}
			
		if(Vector3.Distance (transform.position, Player.transform.position) <= AttDist && !GetComponent<AnimalHealth> ().isDead) {
			animator.Play ("Attack");
			inAttackZone = true;
		} else if (gameObject.GetComponent<AnimalHealth> ().currentHealth > 0) {
			animator.Play ("Run");
			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
			inAttackZone = false;
		} else {
			animalDead = true;
			animator.Play ("Death");
		}
	}
		
}

