using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHealth : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;
	public GameObject healthBar;
	public GameObject healthBarBackground;
	public RectTransform objectRectTransform;
	public AudioSource death;
	public static Health instance;
	public float locX, locY, locZ, sDelta;
	float savedHealth;
	RectTransform savedBar;
	Animator animator;


	public bool isDead = false;

	public void Awake () {objectRectTransform = healthBar.GetComponent<RectTransform> ();
		maxHealth = objectRectTransform.rect.width; // max health is the width of the element
	}

	// Use this for initialization
	public void Start () {
		animator = GetComponent<Animator>();

		if (PlayerPrefs.GetString ("savedGame") != "true") {
			objectRectTransform = healthBar.GetComponent<RectTransform> ();
			maxHealth = objectRectTransform.rect.width; // max health is the width of the element

			Debug.Log("Maxhealth:" + maxHealth); 
			currentHealth = maxHealth;
		} 

	}


	// Update is called once per frame
	public void Update () {
		//save values accessed by saveGame function
		sDelta = objectRectTransform.rect.size.y;
		locX = objectRectTransform.transform.position.x;
		locY = objectRectTransform.transform.position.y;
		locZ = objectRectTransform.transform.position.z;

	}


	// damage in this function represents the percent of health lost
	public void TakeDamage(float damage)
	{
		Debug.Log ("Take Damage Function Called");
		float percentDamage = (maxHealth*damage);
		currentHealth -= percentDamage;
		Vector3 v = new Vector3 (percentDamage/2, 0, 0);
		objectRectTransform.sizeDelta = new Vector2 (currentHealth, objectRectTransform.sizeDelta.y);
		objectRectTransform.localPosition += v;

		if (currentHealth <= 0)
		{
			isDead = true;
			currentHealth = 0;
			Debug.Log ("Dead enemy!");

			gameObject.GetComponent<FollowPlayer> ().inAttackZone = false;

			animator.Play ("Death");
			death.Play();
		}
	}
		
}
