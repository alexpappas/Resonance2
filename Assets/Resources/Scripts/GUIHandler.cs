//********************************************************************
//
//	GUIHandler
//
//	this script allows me to make a single instance of the GUI buttons
//	as a prefab that i can edit easily across multiple levels
//	for coherency
//	don't have to manually reset the listener in the unity editor
//	each time i make an edit
//
//********************************************************************

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {

	public GameObject rButton;	//	reset button object
	public GameObject pButton;	//	pause button object
	public GameObject nButton;	//	next button object

	void Start () {			
		rButton.GetComponent<Button>().onClick.AddListener (GameManager.instance.restart_button_click);
		pButton.GetComponent<Button>().onClick.AddListener (GameManager.instance.pause_button_click);
		nButton.GetComponent<Button>().onClick.AddListener (GameManager.instance.next_stage_button_click);
	}

}
