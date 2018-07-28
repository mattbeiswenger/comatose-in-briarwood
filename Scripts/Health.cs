using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;
	GameObject healthBar;
	public RectTransform objectRectTransform;
	public static Health instance;
	public float locX, locY, locZ, sDelta;
	public float origLocX, origLocY, origLocZ, origSDelta;
	float savedHealth;
	RectTransform savedBar;
    Animator animator;


	// Use this for initialization
	public void Start () {
		animator = GetComponent<Animator>();

		// healthBar = GameObject.FindWithTag("PlayerHealth");
		healthBar = GameObject.Find ("/PlayerHealthBarCanvas/HealthBarForeground");
		objectRectTransform = healthBar.GetComponent<RectTransform> ();

		maxHealth = objectRectTransform.rect.width; // max health is the width of the element

		origSDelta = objectRectTransform.sizeDelta.y;
		origLocX = objectRectTransform.transform.position.x;
		origLocY = objectRectTransform.transform.position.y;
		origLocZ = objectRectTransform.transform.position.z;

		Debug.Log("Maxhealth:" + maxHealth); 
		currentHealth = maxHealth;
		
		if (PlayerPrefs.GetString ("savedGame") == "true") {
			objectRectTransform = setRectangleFromSave (); // set rect transform from save (this is needed to keep the position)
			currentHealth = setHealthFromSave (); // set current health from save
		} 


	}


	// Update is called once per frame
	public void Update () {
		//save values accessed by saveGame function
		sDelta = objectRectTransform.sizeDelta.y;
		locX = objectRectTransform.transform.position.x;
		locY = objectRectTransform.transform.position.y;
		locZ = objectRectTransform.transform.position.z;
	}


	// damage in this function represents the percent of health lost
	public void TakeDamage(float damage)
	{
		float percentDamage = (maxHealth*damage);
		currentHealth -= percentDamage;
		Vector3 v = new Vector3 (percentDamage/2, 0, 0);
		objectRectTransform.sizeDelta = new Vector2 (currentHealth, objectRectTransform.sizeDelta.y);
		objectRectTransform.localPosition -= v;

	

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log ("Dead!");

			animator.Play ("Death");


			EndGame deathPopUp = GameObject.Find("Game Controller").GetComponent<EndGame>();
			deathPopUp.playerDead ();

		}
	}





	public float setHealthFromSave()
	{ 
		savedHealth = GameObject.Find("Game Controller").GetComponent<SaveGame>().health;

		// either retrieve from saved game or set default
		if (float.IsNaN(savedHealth))
			return objectRectTransform.rect.width;
		else //saved game
			return savedHealth;
	}



	public RectTransform setRectangleFromSave()
	{
		savedBar = GameObject.Find("Game Controller").GetComponent<SaveGame>().healthBar;
	
		// either retrieve from saved game or set default
		if (savedBar != null) {
			return savedBar;
		}
		else {
			return healthBar.GetComponent<RectTransform>();
		}
	
	}
}
