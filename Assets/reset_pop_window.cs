using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class reset_pop_window : MonoBehaviour {

	public GameObject tomove;
	public static bool out_of_moves = false;
	public static bool finished = false;
	public Text stage_name;
	private bool move = false;
	public static bool player_has_won = false;
	private bool fade_away = false;
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
		
		if (out_of_moves && !finished) {
			move = true;
		}

		if (move && !finished) {
			Vector3 current_position = tomove.transform.position;
			current_position.x = current_position.x - Time.deltaTime * 82f;
			tomove.transform.position = current_position;
			time_started += Time.deltaTime;
		}

		if (time_started > 2f && !finished) {
			finished = true;
			time_started = 0f;
		}

		if (finished && player_has_won) {
			time_started += Time.deltaTime;
			if (time_started > 2.5f) {
				fade_away = true;
			}

		}

		if (fade_away) {
			Vector3 current_position = tomove.transform.position;
			current_position.x = current_position.x + Time.deltaTime * 82f;
			tomove.transform.position = current_position;
		}

		if (Input.GetMouseButton (0)) {
			try {
				GameObject a = EventSystem.current.currentSelectedGameObject;
				Button b = a.GetComponent<Button>();
				if (b.name == "Button (1)") {
					GameManager.number_of_shots = 0;
					reset_pop_window.finished = false;
					reset_pop_window.out_of_moves = false;
					SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
				}

			} finally {
			}

		}


	}






}
