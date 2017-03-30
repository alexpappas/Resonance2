using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReached : MonoBehaviour {

	public GameObject gPart;
	public Sprite activeGoal;

	void OnTriggerEnter2D(Collider2D coll) {		
		this.GetComponent<SpriteRenderer> ().sprite = activeGoal;
		Instantiate (gPart, transform.position, transform.rotation);
		GameManager.instance.GoalReached();
	}
}
