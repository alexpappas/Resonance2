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
	public static bool booted = false;

	public static Dictionary<string, Vector3> data;

	// Use this for initialization
	void Start () {
		helper_bool = false;
		if (!booted) {
			Sprite blackstar = Resources.Load<Sprite> ("Sprites/blackStar2");
			data = new Dictionary<string, Vector3> ();
			foreach (Button b in GameObject.Find ("tutorial").GetComponentsInChildren<Button> ()) {
//				foreach (Image image in b.GetComponentsInChildren<Image>()) {
//					if (!image.name.Contains ("Button")) {
//						image.sprite = blackstar;
//						image.rectTransform.localScale = new Vector3 (3, 3, 3);
//						image.rectTransform.position = new Vector3 (image.rectTransform.position.x, image.rectTransform.position.y - 30, image.rectTransform.position.z);
//					}
//				}

				Vector3 v = new Vector3 (0, 0, 0);
				string s = b.GetComponentInChildren<Text> ().text;
				data [s] = v;
			}
			foreach (Button b in GameObject.Find ("symmetry").GetComponentsInChildren<Button> ()) {
//				foreach (Image image in b.GetComponentsInChildren<Image>()) {
//					if (!image.name.Contains ("Button")) {
//						image.sprite = blackstar;
//						image.rectTransform.localScale = new Vector3 (3, 3, 3);
//						image.rectTransform.position = new Vector3 (image.rectTransform.position.x, image.rectTransform.position.y - 30, image.rectTransform.position.z);
//					}
//				}
				Vector3 v = new Vector3 (0, 0, 0);
				string s = b.GetComponentInChildren<Text> ().text;
				data [s] = v;
			}
			foreach (Button b in GameObject.Find ("rotation").GetComponentsInChildren<Button> ()) {
//				foreach (Image image in b.GetComponentsInChildren<Image>()) {
//					if (!image.name.Contains ("Button")) {
//						image.sprite = blackstar;
//						image.rectTransform.localScale = new Vector3 (3, 3, 3);
//						image.rectTransform.position = new Vector3 (image.rectTransform.position.x, image.rectTransform.position.y - 30, image.rectTransform.position.z);
//
//					}
//				}
				Vector3 v = new Vector3 (0, 0, 0);
				string s = b.GetComponentInChildren<Text> ().text;
				data [s] = v;
			}
			booted = true;
		}
		update_ui ();

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
	void Awake() {
		update_ui ();

	}
	void update_ui() {
		if (booted) {
			Sprite new_sprite = Resources.Load<Sprite> ("Sprites/newstar");

			foreach (Button b in GameObject.Find ("tutorial").GetComponentsInChildren<Button> ()) {
				if (data [b.GetComponentInChildren<Text> ().text] [0] == 1) {
//					b.GetComponentsInChildren<Image> () [3].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [3].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [3].rectTransform.position.x, b.GetComponentsInChildren<Image> () [3].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [3].rectTransform.position.z);
					b.GetComponentsInChildren<Image> () [3].sprite = new_sprite;
				}
				if (data [b.GetComponentInChildren<Text> ().text] [1] == 1) {
//					b.GetComponentsInChildren<Image> () [1].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [1].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [1].rectTransform.position.x, b.GetComponentsInChildren<Image> () [1].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [1].rectTransform.position.z);
					b.GetComponentsInChildren<Image> () [1].sprite = new_sprite;
				}
				if (data [b.GetComponentInChildren<Text> ().text] [2] == 1) {
//					b.GetComponentsInChildren<Image> () [2].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [2].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [2].rectTransform.position.x, b.GetComponentsInChildren<Image> () [2].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [2].rectTransform.position.z);
					b.GetComponentsInChildren<Image> () [2].sprite = new_sprite;
				}

			}
			foreach (Button b in GameObject.Find ("symmetry").GetComponentsInChildren<Button> ()) {
				if (data [b.GetComponentInChildren<Text> ().text] [0] == 1) {
//					b.GetComponentsInChildren<Image> () [3].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [3].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [3].rectTransform.position.x, b.GetComponentsInChildren<Image> () [3].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [3].rectTransform.position.z);
					b.GetComponentsInChildren<Image> () [3].sprite = new_sprite;
				}
				if (data [b.GetComponentInChildren<Text> ().text] [1] == 1) {
//					b.GetComponentsInChildren<Image> () [1].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [1].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [1].rectTransform.position.x, b.GetComponentsInChildren<Image> () [1].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [1].rectTransform.position.z);

					b.GetComponentsInChildren<Image> () [1].sprite = new_sprite;
				}
				if (data [b.GetComponentInChildren<Text> ().text] [2] == 1) {
//					b.GetComponentsInChildren<Image> () [2].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [2].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [2].rectTransform.position.x, b.GetComponentsInChildren<Image> () [2].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [2].rectTransform.position.z);

					b.GetComponentsInChildren<Image> () [2].sprite = new_sprite;
				}
			}
			foreach (Button b in GameObject.Find ("rotation").GetComponentsInChildren<Button> ()) {
				if (data [b.GetComponentInChildren<Text> ().text] [0] == 1) {
//					b.GetComponentsInChildren<Image> () [3].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [3].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [3].rectTransform.position.x, b.GetComponentsInChildren<Image> () [3].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [3].rectTransform.position.z);

					b.GetComponentsInChildren<Image> () [3].sprite = new_sprite;
				}
				if (data [b.GetComponentInChildren<Text> ().text] [1] == 1) {
//					b.GetComponentsInChildren<Image> () [1].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [1].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [1].rectTransform.position.x, b.GetComponentsInChildren<Image> () [1].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [1].rectTransform.position.z);

					b.GetComponentsInChildren<Image> () [1].sprite = new_sprite;
				}
				if (data [b.GetComponentInChildren<Text> ().text] [2] == 1) {
//					b.GetComponentsInChildren<Image> () [2].rectTransform.localScale = new Vector3 (1, 1, 1);
//					b.GetComponentsInChildren<Image> () [2].rectTransform.position = new Vector3 (b.GetComponentsInChildren<Image> () [2].rectTransform.position.x, b.GetComponentsInChildren<Image> () [2].rectTransform.position.y + 30, b.GetComponentsInChildren<Image> () [2].rectTransform.position.z);

					b.GetComponentsInChildren<Image> () [2].sprite = new_sprite;
				}
			}
		}
	}
}
