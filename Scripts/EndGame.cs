using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public Transform DeathCanvas;
	public Transform Hunter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void playerDead(){
	
		if(DeathCanvas.gameObject.activeInHierarchy == false)
		{

			//freeze the game
			Cursor.visible = true;
			Time.timeScale = 0;

			//disable shooting
			Hunter.GetComponent<HunterScript>().enabled = false;  

			//disable mouselook
			Hunter.GetComponent<MouseLook>().enabled = false; 

			//show the canvas
			DeathCanvas.gameObject.SetActive (true);
		}
		else
			Debug.Log("ERROR");

	}
}
