using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name.EndsWith ("Pulse")) {
			GetComponent<Animator>().Play ("Pulse");
			Destroy (coll.gameObject);
			AudioManager.instance.SoundBarrier ();
		}
	}
}
