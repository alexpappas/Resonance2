using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalReached : MonoBehaviour {

	public GameObject gPart;
	public Sprite activeGoal;
	bool isActive = false;

	void OnTriggerEnter2D(Collider2D coll) {


		if (!isActive) {
			print ("goal Reached");
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



	}



}
