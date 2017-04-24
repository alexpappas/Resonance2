﻿//********************************************************************
//
//	ResonatorScript
//
//	attached to the resonating shapes scattered around the level
//	
//	Resonator Logic!
//
//	a resonator is similar to the player
//	it is stationary
//	and will send out pulses when activated
//	resonators are activated (isResonating = true) when it is triggered by another object's pulse

//	when activated, a resonator will pulse a certain number of times before turning itself OFF
//	triangle will pulse 3 times
//	squares will pulse 4 times
//	etc
//
//	a resonator that is OFF will turn ON when triggered
//	a resonator that is ON will turn OFF when triggered
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class Resonator3D : MonoBehaviour {

	public bool shouldPulse;
	public Animator ani;

	//	what shape the resonator is
	public int numSides;

	//	these are the pulse prefabs
	public GameObject triPulse;
	public GameObject sqrPulse;
	public GameObject pentPulse;

	//	if the object is ON (pulsing) = true
	//	of the object is OFF (not pulsing) = false
	public bool isResonating;

	//	has it been turned on at least once?
	public bool hasBeenActivated;
	private bool should_increment_counter;

	//	color when it is not resonating
	Color noRes = new Vector4(0.133f, 0.2f, 0.7f, 1.0f);

	Material noResMaterial;
	Material resMaterial;

	//	equal to the number of sides
	//	for now
	//	please have fun with this!
	public int MAXPULSES;

	public Renderer myRenderer;

	//	MAXPULSES - how many times it has pulses since being turned ON
	int pulsesRemaining;

	void Start () {

		//	nothing is resonating on start
		isResonating = false;
		hasBeenActivated = false;
		should_increment_counter = true;

		noResMaterial = Resources.Load ("celShading/InactiveToon") as Material;
		resMaterial = Resources.Load ("celShading/ActiveToon") as Material;
		myRenderer.GetComponent<Renderer>().material = noResMaterial;

		MAXPULSES = numSides;

	}

	void Update() {
		//	leave this one alone for now
		//	basically checks if it is in the rotating pentagon
		//	and if that pentagon is rotating it turns off
		//	if people like it this week this is our next big project
		//	that i'm really gonna need you're help with!
		if (this.GetComponentInParent<BigRotation> () == null) {
			//print ("AH");
		} else if (this.GetComponentInParent<BigRotation> ().isRotating) {
			isResonating = false;
			hasBeenActivated = false;
			should_increment_counter = false;
			SetMyColor ();
			CancelInvoke ("ExecutePulse");
		}

		if (shouldPulse) {
			InvokeRepeating ("ExecutePulse", 1, 4);
			shouldPulse = false;
		}

	}

	//	I HAVE BEEN ACTIVATED BY ANOTHER PULSE!
	void OnTriggerEnter2D(Collider2D coll) {

	//void OnTriggerEnter() {
	//public void PulseTriggered() {
		
		if (!hasBeenActivated && should_increment_counter) {
			GameManager.count_of_activated_resonators++;				
		}

		hasBeenActivated = true;
		//	I was OFF and now I am ON
		if (!isResonating) {
			ani.Play ("Idle");
			//	waits for a second, before pulsing once every 4 seconds	
			InvokeRepeating ("ExecutePulse", 1, 4);
			isResonating = true;
			//	I was ON and now I am OFF
		} else if (isResonating) {
			isResonating = false;
			CancelInvoke ("ExecutePulse");
		}

		SetMyColor ();

		pulsesRemaining = MAXPULSES;

	}

	//	resonators pulsate at regular intervals
	//	the shape of this resonator will determine which pulse prefab to instantiate
	void ExecutePulse() {

		if (numSides == 5) {
			GameObject s = Instantiate (pentPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Resonator's Pulse";
			}
		} if (numSides == 4) {
			GameObject s = Instantiate (sqrPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Resonator's Pulse";
			}
		} if (numSides == 3) {
			GameObject s = Instantiate (triPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Resonator's Pulse";
			}
		}

		ani.Play ("Pulse");

		SendNumSides (numSides);

		pulsesRemaining--;

		//	resonArea is a child of this resonator
		//	each time the resonator sends out a pulse
		//	the area of the sprite behind it increases
		//	these are the growing colored areas that radiate out from the resonators
		//	this is the thing that i may not include in the future
		//resonArea.transform.localScale *= 2;

		//print ("I have " + numSides + " sides : " + pulsesRemaining);

		//	turns the resonator off if that was its last pulse
		if (pulsesRemaining < 1) {
			isResonating = false;
			SetMyColor ();
			CancelInvoke ("ExecutePulse");
		}

	}

	//	if it is resonating its color is white
	//	if it is not resonating its a darking kinda blackish color
	void SetMyColor () {
		if (isResonating) {
			myRenderer.GetComponent<Renderer>().material = resMaterial;
		} else if (!isResonating && hasBeenActivated) {
			myRenderer.GetComponent<Renderer>().material = noResMaterial;
		} else if (!isResonating && !hasBeenActivated) {
			myRenderer.GetComponent<Renderer>().material = noResMaterial;
		}
	}

	//	sends its identity to the music program
	//	each resonator makes a different sound when it pulses depending on its shape
	void SendNumSides(int x) {
		AudioManager.instance.ResonatorPulse(x);
	}
}
