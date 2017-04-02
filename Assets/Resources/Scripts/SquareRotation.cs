using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRotation : MonoBehaviour {

	private Vector3 currentAngle;

	private Vector3 targetAngle;

	// Use this for initialization
	void Start () {
		currentAngle = transform.eulerAngles;
		targetAngle = new Vector3 (0, 0, currentAngle.z + 72);
	}
	
	// Update is called once per frame
	void Update () {
		currentAngle = new Vector3 (0, 0, Mathf.LerpAngle (currentAngle.z, targetAngle.z, Time.deltaTime * 0.7f));
			transform.eulerAngles = currentAngle;
	}
}
