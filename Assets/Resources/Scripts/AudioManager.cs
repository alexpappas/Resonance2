using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public bool audioON;

	void Awake () {
		//	create singleton instance of AudioManager
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		if (audioON) {
			//	for communication with MaxMSP
			OSCHandler.Instance.Init ();

			OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "onoff 1");
		}
	}

	//	sends player pulse information to Max
	public void PlayerPulse(int x) {
		if (audioON) {
			OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "pulse " + x);
		}
	} 

	//	sends resonator pulse information to Max
	public void ResonatorPulse(int x) {
		if (audioON) {
			OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "pulse " + x);
		}
	}

	public void RotateSound(int x) {
		if (audioON) {
			OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "rotate " + x);
		}
	}

	//	if goal has been achieved sends value of 8
	public void GoalBubble() {
		if (audioON) {
		//	OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "bubble");
		}
	}

	//	stop sound... theoretically
	void OnApplicationQuit() {
		//	message of -1 turns audio off
		if (audioON) {
			OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "onoff 0");
		}
	}
}
