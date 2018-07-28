using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	public Transform MainMenu;
	public Transform HTPPopUp;

	//creates a new game
	public void PlayGame(){
		SceneManager.LoadScene (1);

		//tell the SaveGame script whether or not to load the last save
		PlayerPrefs.SetString ("savedGame", "false");
	}

	//resumes the last save
	public void ContinueGame(){
		SceneManager.LoadScene (1);
		//tell the SaveGame script whether or not to load the last save
		PlayerPrefs.SetString ("savedGame", "true");
	}

	//closes the entire application
	public void ExitGame(){
		//tell the SaveGame script whether or not to load the last save
		PlayerPrefs.SetString ("savedGame", "false");
		Debug.Log ("Quit");
		Application.Quit ();
	}

	//shows the how to play canvas
	public void HowToPlayTheGame(){
		HTPPopUp.gameObject.SetActive(true);
		MainMenu.gameObject.SetActive(false);
	}

	//closes the how to play canvas
	public void BackToMain(){
		HTPPopUp.gameObject.SetActive(false);
		MainMenu.gameObject.SetActive(true);
	}
}
