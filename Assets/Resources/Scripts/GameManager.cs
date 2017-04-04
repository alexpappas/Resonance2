//********************************************************************
//
//	GameManager!!!
//
//	I control a lot of stuff
//	attached to game object GM
//	there is only one instance of me
//	I currently also handle communication with the music program
//
//	ADRIEN
//
//********************************************************************

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private int numSides;

	private string[] stages;

	private bool levelComplete;

	void Start () {
		stages = new string[] { "TitleScreen", "Tutorial01", "Tutorial02", "Tutorial02a", "Tutorial01c", "Tutorial01d", "Tutorial04", "Tutorial05", "Level01", "Level02" };
	}

	public void restart_button_click(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void pause_button_click(){
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		}
		else{
			Time.timeScale = 1;
		}
	}

	public void next_stage_button_click(){
		Scene scene = SceneManager.GetActiveScene();
		int next = 0;
		for (int i = 0; i < stages.Length; i++)
		{
			if (scene.name == stages[i]) {
				next = i + 1;
			}
		}
		SceneManager.LoadScene(stages[next], LoadSceneMode.Single);
		levelComplete = false;
	}


	void Awake () {	
		//	create singleton instance of GameManager
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
			
		//	instantiates AudioManager
		Instantiate(Resources.Load("prefabs/AudioManager"));

		//	player's number of sides 
		//	start game with 5 sides
		numSides = 5;

	}

	//	decreases the number of sides the player has by 1 (but lets the player do it and then gets the value back)
	public void DecrementNumSides() {
		PlayerScript.instance.decrementNumSides();

		numSides = PlayerScript.instance.getNumSides ();
		AudioManager.instance.PlayerPulse (numSides);

	}

	//	increases the number of sides the player has by 1 (but lets the player do it and then gets the value back)
	public void IncrementNumSides() {
		
		PlayerScript.instance.incrementNumSides();
		numSides = PlayerScript.instance.getNumSides ();
		AudioManager.instance.PlayerPulse (numSides);

	}

	//	returns the number of sides the player has // shouldn't be necessary anymore but i'm still keeping it in just in case.
	public int GetNumSides() {
		return numSides;
	}

	//	sends the number of sides the player has to the music program
	void SendNumSides(int x) {
		AudioManager.instance.PlayerPulse (numSides);
		PlayerScript.instance.setNumSides(x);

		numSides = PlayerScript.instance.getNumSides ();


	}

	public void GoalReached() {
		levelComplete = true;
		GameObject nextButton = GameObject.Find ("Next");
		nextButton.SetActive (true);
		nextButton.GetComponent<Button> ().interactable = true;
	}
}
