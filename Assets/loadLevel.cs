using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour {

	public string sceneName;

	void OnTriggerEnter2D(Collider2D coll) {

		print ("Loading Scene with name " + sceneName);
		//SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

		Invoke ("LevelSelected", 2.5f);

	}

	void LevelSelected() {
	
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

	}

}
