using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttons_control : MonoBehaviour {

	public EventSystem eventsystem;
	private string[] stages;
	private bool helper_bool = false;

	// Use this for initialization
	void Start () {
		helper_bool = false;
		stages = new string[] {
			"TitleScreen",
			"Tutorial01",
			"Tutorial02",
			"Tutorial02a",
			"Tutorial01c",
			"Tutorial01d",
			"Tutorial04",
			"Tutorial05",
			"Tutorial05a",
			"Level02",
			"Level03",
			"Level04",
			"Level05",
			"Level06",
			"Level24", 
			"Level25",
			"Level26",
			"Level27"
		};
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) && !helper_bool) {
			try {
				GameObject a = EventSystem.current.currentSelectedGameObject;
				Button b = a.GetComponent<Button>();
				helper_bool = true;
				SceneManager.LoadScene(b.GetComponentInChildren<Text>().text, LoadSceneMode.Single);

			} finally {
			}
				
		}
		
	}
}
