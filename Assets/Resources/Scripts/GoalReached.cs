using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalReached : MonoBehaviour {

	public GameObject gPart;
	//public Sprite activeGoal;
	bool isActive = false;

	public Animator ani;
	public Renderer myRenderer;
	public Renderer myInRenderer;
	public Renderer myCenterRenderer;

	Material resMaterial;
	Material whiteMaterial;
	Material blueMaterial;

	void Awake() {
		resMaterial = Resources.Load ("celShading/ActiveToon") as Material;
		whiteMaterial = Resources.Load ("celShading/GoalCenterToon") as Material;
		blueMaterial = Resources.Load ("celShading/PlayerCenter") as Material;

	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (!isActive) {
			print ("goal Reached");
			isActive = true;
			reset_pop_window.out_of_moves = false;

			//	changes appearance
			//this.GetComponent<SpriteRenderer> ().sprite = activeGoal;
			ani.Play("GoalPulse");

			myRenderer.GetComponent<Renderer>().material = resMaterial;
			myInRenderer.GetComponent<Renderer>().material = whiteMaterial;
			myCenterRenderer.GetComponent<Renderer>().material = blueMaterial;


			//	instantiates particles
			Instantiate (gPart, transform.position, transform.rotation);

			//	notifies GM
			GameManager.instance.GoalReached ();

			//	notifies AM
			AudioManager.instance.GoalBubble ();
		}



	}



}
