using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (name == "Player's Pulse") {
			this.GetComponent<TrailRenderer> ().startColor = Color.cyan;
			this.GetComponent<TrailRenderer> ().endColor = Color.white;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
