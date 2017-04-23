using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseDetection : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider coll) {
		print (coll.name);
//		GetComponentInParent<Resonator3D> ().PulseTriggered ();
	}

	//void OnTriggerEnter2D (Collider2D coll) {
	//	print(coll.name);
	//}

}
