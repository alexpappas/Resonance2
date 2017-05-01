using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadStars : MonoBehaviour {

	public Vector3 stars;

	public GameObject blackStar1;
	public GameObject blackStar2;
	public GameObject blackStar3;

	public GameObject newStar1;
	public GameObject newStar2;
	public GameObject newStar3;

	// Use this for initialization
	void Start () {

		stars [0] = 0;
		stars [1] = 0;
		stars [2] = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if (stars [0] == 1) {
			blackStar1.SetActive (false);
			newStar1.SetActive (true);
		} else{
			blackStar1.SetActive (true);
			newStar1.SetActive (false);
		} 

		if (stars [1] == 1) {
			blackStar2.SetActive (false);
			newStar2.SetActive (true);
		} else{
			blackStar2.SetActive (true);
			newStar2.SetActive (false);
		}
		if (stars [2] == 1) {
			blackStar3.SetActive (false);
			newStar3.SetActive (true);
		} else {
			blackStar3.SetActive (true);
			newStar3.SetActive (false);
		}
	}
}
