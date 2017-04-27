using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLight : MonoBehaviour {

	public GameObject lightPrefab;

	// Use this for initialization
	void Start () {
		ParticleSystem ps = this.GetComponent<ParticleSystem> ();

		Light partLight = ps.lights.light;

		partLight = GameObject.FindGameObjectWithTag ("partLight").GetComponent<Light>();

	}
}
