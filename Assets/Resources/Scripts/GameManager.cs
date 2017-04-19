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


	public static int count_of_resonators = 0;
	public static int count_of_activated_resonators = 0;

	public Text goal1;
	public Text goal2;
	public Text goal3;

	public Image image1;
	public Image image2;
	public Image image3;

	public Image star1;
	public Image star2;
	public Image star3;

	public Image check1;
	public Image check2;
	public Image check3;

	public Image overlay;

	public Button next_button;

	public static int number_of_shots = 0;

	private int score_or_go_next = 0;

	public bool resultsDisplaying;


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
				if (gameObj.name.Contains ("resonator")) {
					count_of_resonators++;
				}
			}

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
			check1.enabled = false;
			check2.enabled = false;
			check3.enabled = false;
			overlay.enabled = false;
		}
	}

	private Vector3 determine_results() {
		Vector3 result = new Vector3(1, 0, 0);
		if (number_of_shots <= 3) {
			result [2] = 1;
		}

		if (count_of_activated_resonators == count_of_resonators) {
			result [1] = 1;
		}

		return result;
	}



	private void display_score() {

		resultsDisplaying = true;

		// Game Scoring Stuff
		Vector3 results = determine_results();
		//print (results);
		if (results [0] == 1.0f) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/fancystar");
			star1.sprite = new_sprite;
			check1.enabled = true;
		}
		if (results [1] == 1.0f) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/fancystar");
			star2.sprite = new_sprite;
			check2.enabled = true;
		}
		if (results [2] == 1.0f) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/fancystar");
			star3.sprite = new_sprite;
			check3.enabled = true;
		}
		goal1.text = "Level Completed";
		goal2.text = "All Resonators Activated";
		goal3.text = "3 shots or less";
		image1.enabled = true;
		image2.enabled = true;
		image3.enabled = true;
		goal1.enabled = true;
		goal2.enabled = true;
		goal3.enabled = true;
		star1.enabled = true;
		star2.enabled = true;
		star3.enabled = true;
		overlay.enabled = true;
	}

	public void restart_button_click(){
		Application.LoadLevel(Application.loadedLevel);
		GameManager.number_of_shots = 0;
		reset_pop_window.finished = false;
		reset_pop_window.out_of_moves = false;
	}

	public void menu_button_click(){
		SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
		GameManager.number_of_shots = 0;
		reset_pop_window.finished = false;
		reset_pop_window.out_of_moves = false;
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
			SceneManager.LoadScene(stages[next], LoadSceneMode.Single);

			levelComplete = false;
			score_or_go_next = 0;
			GameManager.number_of_shots = 0;

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

		//DontDestroyOnLoad (this.transform);

		//	player's number of sides 
		//	start game with 5 sides
		numSides = 5;

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

		print ("Goal has been reached");
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
						print ("Space has been pressed ");
						print ("going to next level");
						timeBeforeScore = maxTimeBeforeScore;
					}
					//print (timeBeforeScore);
				} else {
					print ("Score will be displayed now");
					next_stage_button_click ();
					//now that we have displayed the ui can set the timebeforescore to a negative value to wait for spacebar press.
					timeBeforeScore = -10.0f;
				}
			}
			else{

				if (Input.GetKeyDown (KeyCode.Space)) {
					print ("Space has been pressed ");
					print ("going to next level");
					next_stage_button_click ();
					goalReached = false;
					timeBeforeScore = 0;
				}

			}

		}

	}
}
