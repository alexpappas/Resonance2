using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReached : MonoBehaviour {

	public GameObject gPart;
	public Sprite activeGoal;

	bool isActive = false;

	//	when goal is hit with a pulse
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
	}
}
