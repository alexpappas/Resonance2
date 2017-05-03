using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRotation : MonoBehaviour {

	public int rotateShape;	//	number of sides/corner resonators
	//public float radius;	//	distance between each corner resonator and the center

	private Vector3 currentAngle;
	private Vector3 targetAngle;
	private int angleDelta;

	bool shouldRotate = false;
	public bool isRotating = false;

	//rotating half or full angles
	public bool shouldRotateHalf = false; // shouldRotateHalf = true only rotates half, else rotates full

    public bool clockwise = false; //default is counter-clockwise operation. set this to true if want clockwise

	//	public GameObject sqrRes;	//	for variable radius and instantiation of resonators

	public int rotateSpeed;
	public GameObject[] lines;

	GameObject[] members;

	// Use this for initialization
	void Start () {
		if (rotateShape == 5) {
			angleDelta = 72;
		} else if (rotateShape == 4) {
			angleDelta = 90;
			//CreateChildrenSqr ();
		} else if (rotateShape == 3) {
			angleDelta = 60;
		}

		//angle has been determined and we check if it needs to be cut in half
		if (shouldRotateHalf == true) {
			angleDelta = angleDelta / 2;
		}

        if (clockwise == true)
        {
            angleDelta = -angleDelta;
        }
	}
	
	// Update is called once per frame
	void Update () {

		CheckShouldRotate ();

		if (shouldRotate) {
			shouldRotate = false;
			currentAngle = transform.eulerAngles;
            targetAngle = new Vector3(0, 0, currentAngle.z + angleDelta);
            StartCoroutine ("Rotate");
			AudioManager.instance.RotateSound (rotateShape);
		}

	}

	void CheckShouldRotate() {
		bool childrenActivated = true;
		foreach (Transform child in this.transform) {
			if (child.CompareTag ("resonator")) {
				//childrenActivated = childrenActivated && child.GetComponent<ResonatorScript> ().hasBeenActivated;
				childrenActivated = childrenActivated && child.GetComponent<Resonator3D> ().hasBeenActivated;
			}
		}

		if (childrenActivated) {
			shouldRotate = true;

			foreach (Transform child in this.transform) {
				if (child.CompareTag ("resonator")) {
					//child.GetComponent<ResonatorScript> ().hasBeenActivated = false;
					child.GetComponent<Resonator3D> ().hasBeenActivated = false;
				}
			}
		}

	}

	//	performs rotation
	IEnumerator Rotate() {
		
		if (!isRotating) {
			isRotating = true;

			Color old_color = lines [0].GetComponent<Renderer> ().material.color;
			for (int i = 0; i < lines.Length; i++) {
				lines [i].GetComponent<Renderer> ().material.color = Color.yellow;
			}

            if (clockwise == true)
            {

                while (targetAngle.z < currentAngle.z)
                {
					
                    currentAngle = new Vector3(0, 0, Mathf.MoveTowardsAngle(currentAngle.z, targetAngle.z, Time.deltaTime * rotateSpeed));
                    transform.eulerAngles = currentAngle;
                    yield return null;
                }
            }
            else
            {
                while (currentAngle.z < targetAngle.z)
                {
                    currentAngle = new Vector3(0, 0, Mathf.MoveTowardsAngle(currentAngle.z, targetAngle.z, Time.deltaTime * rotateSpeed));
                    transform.eulerAngles = currentAngle;
                    yield return null;
                }
            }
			for (int i = 0; i < lines.Length; i++) {
				lines [i].GetComponent<Renderer> ().material.color = old_color;
			}
            isRotating = false;
		}
	}

//	void CreateChildrenSqr() {
//		
//		members = new GameObject[4];
//
//		int[] mult = {1, 1, -1, -1, 1};
//
//		for (int i = 0; i < 4; i++) {
//			members [i] = (GameObject)Instantiate (sqrRes, transform.position, transform.rotation);
//			members [i].transform.SetParent (this.transform);
//			members [i].transform.localPosition = new Vector3 (radius * mult[i], radius * mult[i + 1], 0);
//		}
//	}

}
