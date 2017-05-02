//********************************************************************
//
//	PlayerScript
//
//	attached to the player (shape at the center of the screne)
//	
//	player actions:
//	rotate clockwise
//	rotate counter clockwise
//	pulse (the only real means of interacting with the space)
//
//	THIS ONE IS A MESS!!!
//
//	ADRIEN
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class LevelSelectPlayer : MonoBehaviour {

	public static LevelSelectPlayer instance = null;
	public static float rotated_degrees = 0f;

	//	we can play with these 3 until we are happy!!!
	//	rotation speed
	float speed = 100; //changed the player speed to 100 to slow down rotations.

	//	number of sides
	private int numSides = 5;
	public static bool goal_is_reached = false;
	//	force for pulses
	float pulseForce = 10;

	bool movingRight = false;	//	this should be like... clockwise...
	bool movingLeft = false;	//	this should be like... counterclockwise...

	//	this is just a list of the prefab pulse shapes
	//	adrien is there a better way to access and instantiate these prefabs without having to make them public
	//	mostly a clutter thing
	public GameObject triPulse;
	public GameObject sqrPulse;
	public GameObject pentPulse;
	public GameObject hexPulse;

	//	just a list of its different appearances... (square, pentagon, triangle...)
	//	this too! is there another way to switch between sprites without having to make them
	//	public?
	public GameObject triSelf;
	public GameObject sqrSelf;
	public GameObject pentSelf;
	public GameObject hexSelf;

	public Animator ani;

	Vector3 rotationEuler;

	//	color when player is deactivated (when numSides < 3)
	//	still LOOKS like a triangle but it's out of pulses and inert
	Color noRes = new Vector4(0.133f, 0.2f, 0.7f, 1.0f);


	//this is for the aiming line rendering, prefab objects
	//public GameObject triLines;
	void Start() {
		numSides = 5;
	}
	void Awake()
	{
		numSides = 5;
		LevelSelectPlayer.goal_is_reached = false;
		LevelSelectPlayer.rotated_degrees = 0f;

		//	set the instance value to self.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

	}


	// Update is called once per frame
	void Update () {

		if (ani.GetCurrentAnimatorStateInfo (0).IsName ("LoseSide")) {
			SetSprite ();
		}

		//	gets user inputs, right arrow is clockwise rotation of player, left is counterclockwise...
		if (Input.GetKeyDown (KeyCode.RightArrow))
			movingRight = true;
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			movingLeft = true;

		if (Input.GetKeyUp (KeyCode.RightArrow))
			movingRight = false;
		if (Input.GetKeyUp (KeyCode.LeftArrow))
			movingLeft = false;

		//	converts into rotation information
		if (movingRight && numSides >= 3) {
			transform.Rotate (Vector3.back * speed * Time.deltaTime);
			rotated_degrees += speed * Time.deltaTime;
		}

		if (movingLeft && numSides >= 3) {
			transform.Rotate (Vector3.forward * speed * Time.deltaTime);
			rotated_degrees += speed * Time.deltaTime;
		}

		//	checks if player has hit spacebar... if so pulse...
		if (Input.GetKeyDown (KeyCode.Space) && !goal_is_reached) {
			if (ani.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				ExecutePulse ();
			}
		}

		//	after the pulse has been sent, it grabs its number of sides from the GM 
		//	(this is probably absurd but remember not only does sending a pulse affect the number of sides,
		//	so does collecting the food/pearls)
		//numSides = GameManager.instance.GetNumSides ();

		//	adjusts color to indicate if player is inert (has less than 3 sides and can not send pulses into the space)
		//if(numSides < 3)	//	NO PULSE!!!
		//	GetComponent<Renderer>().material.color = noRes;
		//else //	you are still resonanting and can still pulse
		//	GetComponent<Renderer>().material.color = Color.white;

	}

	//	this is called when the player presses the spacebar
	//	it instantiates a one of the pulse prefabs based on the number of sides of the object
	//	(if numSides is 5 it will instantiate the pentPulse prefab)
	//	(if numSides is 4 it will instantiate the sqrPulse prefab)
	//	(if it has fewer than 3 sides it will not pulse
	void ExecutePulse() {
		//print ("Pulse strength = " + (pulseForce/numSides));
		if (numSides >= 6) {
			GameManager.number_of_shots++;
			GameObject s = Instantiate (hexPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Player's Pulse";
			}
			ani.Play ("Pulse");
			AudioManager.instance.PlayerPulse (numSides);
			//GameManager.instance.DecrementNumSides ();

		}
		else if (numSides == 5) {
			GameManager.number_of_shots++;
			GameObject s = Instantiate (pentPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Player's Pulse";
			}
			ani.Play ("Pulse");
			AudioManager.instance.PlayerPulse (numSides);
			//GameManager.instance.DecrementNumSides ();

		}
		else if (numSides == 4) {
			GameManager.number_of_shots++;
			GameObject s = Instantiate (sqrPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Player's Pulse";
			}
			ani.Play ("Pulse");
			AudioManager.instance.PlayerPulse (numSides);
			//GameManager.instance.DecrementNumSides ();

		}
		else if (numSides == 3) {
			GameManager.number_of_shots++;
			GameObject s = Instantiate (triPulse, transform.position, transform.rotation);
			for(int i = 0; i < s.GetComponentsInChildren<Transform>().Length; i++) {
				s.GetComponentsInChildren<Transform>()[i].name = "Player's Pulse";
			}
			ani.Play ("NoSides");
			AudioManager.instance.PlayerPulse (numSides);
			reset_pop_window.out_of_moves = true;

			//GameManager.instance.DecrementNumSides ();

		}

	}
	// these next three function are for the GM to be able to coordinate with the player for number of sides but the GM is not in charge of anything for numsides and will
	//always go to whatever the number of sides the player says it has.
	public void incrementNumSides()
	{
		numSides = numSides + 1;
		SetSprite ();
	}
	public void decrementNumSides()
	{
		numSides = numSides - 1;
	}

	public int getNumSides()
	{
		return numSides;
	}

	public void setNumSides(int newNumSides)
	{

		numSides = newNumSides;
		SetSprite ();
	}

	//	called anytime a pulse is output
	//	or Game Manager increments the number of sides
	//	picks the appropriate sprite, to match numSides
	void SetSprite() {
		if (numSides == 2) {	//	you have less than 3 sides, but we don't wanna make you a line... so you're still a triangle
			//this.GetComponent<SpriteRenderer> ().sprite = sprite3;
			triSelf.SetActive (true);
			sqrSelf.SetActive (false);
			pentSelf.SetActive (false);
		} else if (numSides == 3) {	//	you have 3 sides, you are a triangle
			//this.GetComponent<SpriteRenderer> ().sprite = sprite3;
			triSelf.SetActive(true);
			sqrSelf.SetActive (false);
		} else if (numSides == 4) { 	//	you have 4 sides, you are a square
			//this.GetComponent<SpriteRenderer> ().sprite = sprite4;
			sqrSelf.SetActive (true);
			triSelf.SetActive(false);
			pentSelf.SetActive (false);
		} else if (numSides == 5)	{	//	pentagon
			pentSelf.SetActive(true);
			sqrSelf.SetActive (false);
			triSelf.SetActive (false);
			//this.GetComponent<SpriteRenderer> ().sprite = sprite5;
		} else if (numSides == 6) {	//	hexagon
			//this.GetComponent<SpriteRenderer> ().sprite = sprite6;
		}
	}

}
