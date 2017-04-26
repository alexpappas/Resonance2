using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (name == "Player's Pulse") {
			this.GetComponent<TrailRenderer> ().startColor = new Vector4 (0.18f, 1, 1, 0.686f);
		}
	}

}
