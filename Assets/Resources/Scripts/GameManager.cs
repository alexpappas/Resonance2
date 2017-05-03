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
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private int numSides;

	private string[] stages;

	private bool levelComplete;


	public static int count_of_resonators = 0;
	public static int count_of_activated_resonators = 0;

	public Text goal1;
	public Text goal2;
	public Text goal3;

	public Image image1;
	public Image image2;
	public Image image3;

	public Image nextButtonLevelComp;
	public Image resetButtonLevelComp;
	public Image menuButtonLevelComp;

	public Image star1;
	public Image star2;
	public Image star3;

	//public Image check1;
	//public Image check2;
	//public Image check3;

	public Image overlay;
	public Image overlay2;

	public Button next_button;

	public static int number_of_shots = 0;

	private int score_or_go_next = 0;

	public bool resultsDisplaying;
	private Dictionary<string, float> fewest_shots;


	//score display
	bool goalReached = false;

	float timeBeforeScore = 0.0f;
	float maxTimeBeforeScore = 3.0f;



	void Start () {
		timeBeforeScore = 0;
		Instantiate(Resources.Load("prefabs/AudioManager"));

		stages = new string[] {
			"TitleScreen",
			"0 - 1",
			"0 - 2",
			"0 - 3",
			"0 - 4",
			"0 - 5",
			"0 - 6",
			"1 - 1",
			"1 - 2",
			"1 - 3",
			"1 - 4",
			"1 - 5",
			"2 - 1",
			"2 - 2", 
			"2 - 3",
			"2 - 4",
			"2 - 5",
			"2 - 6"
		};

		fewest_shots = new Dictionary<string, float> ();
		fewest_shots ["0 - 1"] = 360f;
		fewest_shots ["0 - 2"] = 270f;
		fewest_shots ["0 - 3"] = 180f;
		fewest_shots ["0 - 4"] = 90f;
		fewest_shots ["0 - 5"] = 100f;
		fewest_shots ["0 - 6"] = 60f;
		fewest_shots ["1 - 1"] = 90f;	//	could be lower like 60
		fewest_shots ["1 - 2"] = 45f;
		fewest_shots ["1 - 3"] = 30f;
		fewest_shots ["1 - 4"] = 30f;
		fewest_shots ["1 - 5"] = 30f;
		fewest_shots ["2 - 1"] = 150f;
		fewest_shots ["2 - 2"] = 100f; // can be 0
		fewest_shots ["2 - 3"] = 180f;
		fewest_shots ["2 - 4"] = 120f;  // can be 19
		fewest_shots ["2 - 5"] = 100f;  // can be 0
		fewest_shots ["2 - 6"] = 150f;  // can be 45




			
		levelComplete = false;
		score_or_go_next = 0;
		GameManager.number_of_shots = 0;

		resultsDisplaying = false;

		if (SceneManager.GetActiveScene ().name == "TitleScreen") {
			score_or_go_next = 1;

			// Count how many resonators for game goal
		} else {

			count_of_resonators = 0;
			count_of_activated_resonators = 0;
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.Contains ("Resonator3D")) {
					count_of_resonators++;
				}
			}
			count_of_resonators--;


			// Lol code
			image1.enabled = false;
			image2.enabled = false;
			image3.enabled = false;
			goal1.enabled = false;
			goal2.enabled = false;
			goal3.enabled = false;
			star1.enabled = false;
			star2.enabled = false;
			star3.enabled = false;
			//check1.enabled = false;
			//check2.enabled = false;
			//check3.enabled = false;
			nextButtonLevelComp.enabled = false;
			resetButtonLevelComp.enabled = false;
			menuButtonLevelComp.enabled = false;
			overlay.enabled = false;
			overlay2.enabled = false;
		}
	}

	private Vector3 determine_results() {
		Vector3 result = new Vector3(1, 0, 0);
		if (PlayerScript3D.rotated_degrees <= fewest_shots[SceneManager.GetActiveScene().name]) {
			result [2] = 1;
		}
		if (count_of_activated_resonators == count_of_resonators) {
			result [1] = 1;
		}
		if (buttons_control.data == null) {
			buttons_control.data = new Dictionary<string, Vector3> ();
			for (int i = 1; i < stages.Length; i++) {
				buttons_control.data [stages [i]] = new Vector3 (0, 0, 0);
			}
			buttons_control.booted = true;
			buttons_control.data[SceneManager.GetActiveScene ().name] = result;
		}
		buttons_control.data[SceneManager.GetActiveScene ().name] = result;


		if (routeStarsToLevel.data == null) {
			print ("Route stars to level is null, populating with zeros"); 
			routeStarsToLevel.data = new Dictionary<string, Vector3> ();
			for (int i = 1; i < stages.Length; i++) {
				routeStarsToLevel.data [stages [i]] = new Vector3 (0, 0, 0);
			}
			routeStarsToLevel.data[SceneManager.GetActiveScene ().name] = result;
		}
		print ("Sending stars to level selector");
		routeStarsToLevel.data [SceneManager.GetActiveScene ().name] = result;
		print ("result = ");
		print (result);
		return result;
	}



	private void display_score() {

		resultsDisplaying = true;

		// Game Scoring Stuff
		Vector3 results = determine_results();
		//print (results);
		if (results [0] == 1.0f) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/newstar");
			star1.sprite = new_sprite;
			//check1.enabled = true;
		}
		if (results [1] == 1.0f) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/newstar");
			star2.sprite = new_sprite;
			//check2.enabled = true;
		}
		if (results [2] == 1.0f) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/newstar");
			star3.sprite = new_sprite;
			//check3.enabled = true;
		}
		goal1.text = "Level Completed";
		goal2.text = "All Resonators Activated";
		float degrees = fewest_shots [SceneManager.GetActiveScene ().name];
		goal3.text = "Rotated less than " + degrees.ToString () + " degrees!\nYou moved: " + ((int)PlayerScript3D.rotated_degrees).ToString() + " degrees";

		image1.enabled = true;
		image2.enabled = true;
		image3.enabled = true;
		goal1.enabled = true;
		goal2.enabled = true;
		goal3.enabled = true;
		star1.enabled = true;
		star1.GetComponent<Animator> ().Play ("Start");
		star2.enabled = true;
		star2.GetComponent<Animator> ().Play ("Start");
		star3.enabled = true;
		star3.GetComponent<Animator> ().Play ("Start");
		overlay.enabled = true;
		overlay2.enabled = true;

		nextButtonLevelComp.enabled = true;
		resetButtonLevelComp.enabled = true;
		menuButtonLevelComp.enabled = true;

	}

	public void restart_button_click(){
		GameManager.number_of_shots = 0;
		PlayerScript3D.rotated_degrees = 0f;
		reset_pop_window.finished = false;
		reset_pop_window.out_of_moves = false;
		reset_pop_window.player_has_won = false;
		PlayerScript3D.goal_is_reached = false;
		Application.LoadLevel(Application.loadedLevel);

	}

	public void menu_button_click(){
		//SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
		SceneManager.LoadScene("Rotating_Level_Select", LoadSceneMode.Single);
		GameManager.number_of_shots = 0;
		reset_pop_window.finished = false;
		reset_pop_window.out_of_moves = false;
		reset_pop_window.player_has_won = false;
		PlayerScript3D.goal_is_reached = false;

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
		if (score_or_go_next == 0) {
			display_score ();
			score_or_go_next = 1;
		} else {
			Scene scene = SceneManager.GetActiveScene();
			int next = 0;
			for (int i = 0; i < stages.Length; i++)
			{
				if (scene.name == stages[i]) {
					next = i + 1;
				}
			}
			GameManager.number_of_shots = 0;
			PlayerScript3D.rotated_degrees = 0f;
			reset_pop_window.finished = false;
			reset_pop_window.out_of_moves = false;
			reset_pop_window.player_has_won = false;

			SceneManager.LoadScene(stages[next], LoadSceneMode.Single);

			AudioManager.instance.NextScene ();

			levelComplete = false;
			score_or_go_next = 0;


			resultsDisplaying = false;
		}
	}


	void Awake () {	
		//	create singleton instance of GameManager
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
			
		//	player's number of sides 
		//	start game with 5 sides
		numSides = 5;

		//	setting all cameras to orthographic so we don't have to do it manually
		GameObject mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		if (!mainCam.GetComponent<Camera> ().orthographic) {
			mainCam.GetComponent<Camera> ().orthographic = true;
			mainCam.GetComponent<Camera> ().orthographicSize = 4;
		}

		GameObject lights = Resources.Load ("prefabs/lights") as GameObject;
		Instantiate (lights);

	}

	//	decreases the number of sides the player has by 1 (but lets the player do it and then gets the value back)
	public void DecrementNumSides() {
		//PlayerScript.instance.decrementNumSides();

		numSides = PlayerScript.instance.getNumSides ();


	}

	//	increases the number of sides the player has by 1 (but lets the player do it and then gets the value back)
	public void IncrementNumSides() {
		
		//PlayerScript.instance.incrementNumSides();
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
		//nextButton.SetActive (true);

		goalReached = true;

		//get stuck in this routing for 5 seconds;
		//nextButton.GetComponent<Button> ().interactable = true;
	}
		
	void Update()
	{
		//print ("updating");
		if (goalReached == true) {
			//print("goal reached = true");
			//timeBeforeScore >= 0 if the goal is reached and the ui hasn't been displayed yet.
			//timeBeforeScore < 0 if the goal ui has been displayed and we are waiting for the space bar to be pressed.
			if (timeBeforeScore >= 0){ 
				
				if (timeBeforeScore < maxTimeBeforeScore) {
					timeBeforeScore = timeBeforeScore + Time.deltaTime;
					//if the player wants to exit, they can also just hit space.
					if (Input.GetKeyDown (KeyCode.Space)) {
				//		print ("Space has been pressed ");
			//			print ("going to next level");
						timeBeforeScore = maxTimeBeforeScore;
					}
					//print (timeBeforeScore);
				} else {
		//			print ("Score will be displayed now");
					next_stage_button_click ();
					//now that we have displayed the ui can set the timebeforescore to a negative value to wait for spacebar press.
					timeBeforeScore = -10.0f;
				}
			}
			else{

				if (Input.GetKeyDown (KeyCode.Space)) {
				//	print ("Space has been pressed ");
			//		print ("going to next level");
					next_stage_button_click ();
					goalReached = false;
					timeBeforeScore = 0;
				}

			}

		}

	}
}
