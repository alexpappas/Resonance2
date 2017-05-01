using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class routeStarsToLevel : MonoBehaviour {


	public static Dictionary<string, Vector3> data = new Dictionary<string, Vector3> ();

	public 

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		//send the gameobject its own data.
		if (data != null) {
			print ("Data = ");
			foreach(KeyValuePair<string,Vector3> levelScore in data)
			{
				//Now you can access the key and value both separately from this attachStat as:
				print(levelScore.Key);
				print(levelScore.Value);
				// try and find the gameobject with the name GOAL(level_name);

				GameObject curGameObject = GameObject.Find ("GOAL(" + levelScore.Key + ")");

				if (curGameObject != null) {
					
					print (curGameObject);
					loadStars loadStarScript = curGameObject.GetComponent (typeof(loadStars)) as loadStars;

					loadStarScript.stars = levelScore.Value;
				}

			}




		} else {
			print (data);
		}

		
	}
}
