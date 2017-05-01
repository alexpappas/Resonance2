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
			print(data);

		}
		
	}
}
