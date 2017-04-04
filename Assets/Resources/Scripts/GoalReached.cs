using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalReached : MonoBehaviour {

	public static int count_of_resonators = 0;
	public static int count_of_activated_resonators = 0;

	public GameObject gPart;
	public Sprite activeGoal;

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

	bool isActive = false;

	public static int number_of_shots = 0;


	void Start() {

		// Count how many resonators for game goal
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
	
	}

	void OnTriggerEnter2D(Collider2D coll) {		

		if (!isActive) {

			isActive = true;

			//	changes appearance
			this.GetComponent<SpriteRenderer> ().sprite = activeGoal;

			//	instantiates particles
			Instantiate (gPart, transform.position, transform.rotation);

			//	notifies GM
			GameManager.instance.GoalReached ();

			//	notifies AM
			AudioManager.instance.GoalBubble ();
			}

	
			// Game Scoring Stuff
			Vector3 results = determine_results ();
			print (results);
			if (results [0] == 1.0f) {
				Sprite new_sprite = Resources.Load<Sprite> ("Sprites/gold_star.png");
				star1.sprite = new_sprite;
			}
			goal1.text = "Reach the food";
			goal1.color = Color.black;
			goal2.text = "Activate all resonators";
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

}
