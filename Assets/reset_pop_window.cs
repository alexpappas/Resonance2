using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class reset_pop_window : MonoBehaviour {

	public GameObject tomove;
	public static bool out_of_moves = false;
	public static bool finished = false;
	public Text stage_name;
	private bool move = false;
	// Use this for initialization
	void Start () {
		//tomove.GetComponent<RectTransform> ().localPosition = new Vector3 (432, -92, 0);
		time_started = 0f;
		tomove.transform.localScale = new Vector3 (1, 1, 1);
		stage_name.text = SceneManager.GetActiveScene ().name;
	}

	private float time_started = 0f;
	// Update is called once per frame
	void Update () {
		if (out_of_moves && Input.GetKeyDown (KeyCode.Space) && !finished) {
			move = true;
		}

		if (move && !finished) {
			Vector3 current_position = tomove.transform.position;
			current_position.x = current_position.x - Time.deltaTime * 82f;
			tomove.transform.position = current_position;
			time_started += Time.deltaTime;
		}

		if (time_started > 2f) {
			finished = true;
			time_started = 0f;
		}

	}




}
