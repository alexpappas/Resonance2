using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour {

	public string sceneName;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D coll) {

		print ("Loading Scene with name " + sceneName);
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

	}
}
