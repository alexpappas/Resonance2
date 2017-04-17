using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class reset_pop_window : MonoBehaviour {

	public GameObject tomove;
	public static bool out_of_moves = false;
	public static bool won = false;
	public Text stage_name;
	// Use this for initialization
	void Start () {
		Vector3 position = new Vector3 (900, 100, 0);
		tomove.transform.position = position;
		stage_name.text = SceneManager.GetActiveScene ().name;
	}

	private float time_after_fail = 0f;
	// Update is called once per frame
	void Update () {
		if (out_of_moves) {
			time_after_fail += Time.deltaTime;
		}
		if (time_after_fail > 5f && time_after_fail < 7f) {
			Vector3 current_position = tomove.transform.position;
			float current_x = current_position.x;
			current_position.x = current_position.x - Time.deltaTime * 82f;
			tomove.transform.position = current_position;
		}
	}




}
