//********************************************************************
//
//	PulseScript
//
//	this is a TERRIBLE SCRIPT
//	this script is on the prefabs of pulses
//	(pentpulse, tripulse, sqr pulse)
//	this just kinda destroys the pulse so we don't have a ton of
//	invisible game objects floating around the scene
//	in retrospect we could probably just destroy it after a certain
//	duration of time... cause we should be able to calculate how
//	fast and far each pulse is traveling and about when it disappears
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class PulseScript : MonoBehaviour {

	//	speed of pulse
	//	this is constant for all pulses
	int speed = 500;

	int i = 0;

	Rigidbody2D rb;

	public Color colorStart = Color.white;

	void Start () {

		//	sets appearance of pulse based off if it was created by player or not
		if (name == "Player's Pulse") {
			GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Sprites/pulse", typeof(Sprite)) as Sprite;
		} else {
			GetComponent<SpriteRenderer> ().sprite = Resources.Load ("Sprites/pulse2", typeof(Sprite)) as Sprite;
		}

		rb = GetComponent<Rigidbody2D> ();

		//	when it is instantiated (as a part of a pulse group) by the PlayerScript it starts moving outward
		Pulse ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//	the color starts white but as the pulse slows down it becomes more transparent
		colorStart.a = Mathf.Abs (rb.velocity.magnitude);
		GetComponent<Renderer>().material.color = colorStart;

		//	this is buggy... give it enough time to stabilize before it destroys itself
		//	it was trying to destroy itself like RIGHT when it was created
		//	now it waits for 20 frames before it tries to destroy itself
		//	needs to be a better way to do this
		if (i < 20) {
			i++;
		}
		//	it SHOULD destroy itself once it is fully transparent (a = 0)
		else if (colorStart.a <= 0.05) {
			Destroy (this.gameObject);
		}

	}

	//	applies a force to the rigid body on the pulse to propel it outward
	void Pulse() {
		//	this pulseMult is dependent on shape
		//	pulse groups with fewer sides travel farther than those with more
		float multiplier = transform.parent.GetComponent<PulseGroupScript> ().pulseMult;
		//transform.localScale = new Vector3 (multiplier/10, .4f, .1f);
		rb.AddForce(transform.right * speed / multiplier);
	}

//	void OnTriggerEnter2D(Collider2D coll) {		
//		if (coll.gameObject.name == "Player's Pulse" && this.gameObject.name == "Resonator's Pulse") {
//			print (coll.gameObject.transform.right);
//			print (this.gameObject.transform.right);
//			float first_angle = Mathf.Atan2(coll.gameObject.transform.right.y, coll.gameObject.transform.right.x);
//			float second_angle = Mathf.Atan2 (this.gameObject.transform.right.y, this.gameObject.transform.right.x);
//			
//			if (Mathf.Abs (first_angle - second_angle) < 1.2) { // That's about 68 degrees, whatever..
//				float multiplier = transform.parent.GetComponent<PulseGroupScript> ().pulseMult;
//				coll.attachedRigidbody.AddForce (transform.right * speed / multiplier);
//				coll.gameObject.transform.localScale = new Vector3(0.2f, 0.8f, 1);
//				coll.gameObject.name = "Boosted";
//			}
//		} else if (this.gameObject.name == "Player's Pulse" && coll.gameObject.name == "Resonator's Pulse") {
//			
//		}
//	}
		
}
