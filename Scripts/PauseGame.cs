using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public Transform canvas;
	public Transform Hunter;

	public float timeSpeed = 1;

	// Update is called once per frame
	void Update () {

		//escape key bring up pause menu 
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pause ();
		}
			
	}

	public void pause (){
		
		//if the menu is not displayed it will display 
		if(canvas.gameObject.activeInHierarchy == false)
		{

			Cursor.visible = true;
			//display the canvas
			canvas.gameObject.SetActive(true);

			//freeze the game
			Time.timeScale = 0;

			//disable shooting
			Hunter.GetComponent<HunterScript>().enabled = false;  

			//disable mouselook
			Hunter.GetComponent<MouseLook>().enabled = false; 

		}
		//if it is displayed it resumes the game
		else{

			Cursor.visible = false;
			//do not display the canvas
			canvas.gameObject.SetActive(false);

			//resume the game
			Time.timeScale = timeSpeed;

			//disable shooting
			Hunter.GetComponent<HunterScript>().enabled = true; 
			//enable mouse look
			Hunter.GetComponent<MouseLook>().enabled = true; 

		}
	
	}


}
