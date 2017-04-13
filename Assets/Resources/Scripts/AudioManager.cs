using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	public bool audioON;

	AudioClip[] triangleSounds;
	AudioClip[] squareSounds;
	AudioClip[] pentagonSounds;

	public AudioSource[] resonatorPolyphony;
	int resonatorVoiceCounter;

	AudioClip[] triangleRotationSounds;
	AudioClip[] squareRotationSounds;
	AudioClip[] pentagonRotationSounds;

	public AudioSource[] rotationPolyphony;
	int rotationVoiceCounter;

	public AudioSource playerVoice;
	int currentClip;

	public AudioSource goalVoice;

	void Awake () {
		//	create singleton instance of AudioManager
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		if (audioON) {
			//	for communication with MaxMSP
		//	OSCHandler.Instance.Init ();

		//	OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "onoff 1");
		
			InitializeSounds ();
		}
	}
		
	//	sends player pulse information to Max
	public void PlayerPulse(int x) {
		if (audioON) {
			//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "pulse " + x);

			if (x == 3) {
				currentClip = 1 + Random.Range (0, (triangleSounds.Length - 1));
				playerVoice.clip = triangleSounds [currentClip];
			} else if (x == 4) {
				currentClip = 1 + Random.Range (0, (squareSounds.Length - 1));
				playerVoice.clip = squareSounds [currentClip];			
			} else if (x == 5) {
				currentClip = 1 + Random.Range (0, (pentagonSounds.Length - 1));
				print (currentClip);
				playerVoice.clip = pentagonSounds [currentClip];
			}

			playerVoice.Play();

		}
	} 

	//	sends resonator pulse information to Max
	public void ResonatorPulse(int x) {
		if (audioON) {
			//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "pulse " + x);

			if (x == 3) {
				currentClip = Random.Range (0, (triangleSounds.Length - 1));
				resonatorPolyphony[resonatorVoiceCounter].clip = triangleSounds [currentClip];
				resonatorPolyphony [resonatorVoiceCounter].Play ();
			} else if (x == 4) {
				currentClip = Random.Range (0, (squareSounds.Length - 1));
				resonatorPolyphony[resonatorVoiceCounter].clip = squareSounds [currentClip];
				resonatorPolyphony [resonatorVoiceCounter].Play ();			
			} else if (x == 5) {
				currentClip = Random.Range (0, (pentagonSounds.Length - 1));
				resonatorPolyphony[resonatorVoiceCounter].clip = pentagonSounds [currentClip];
				resonatorPolyphony [resonatorVoiceCounter].Play ();
			}

			resonatorVoiceCounter++;
			resonatorVoiceCounter %= 4;
		}
	}

	public void RotateSound(int x) {
		if (audioON) {
			//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "rotate " + x);
			if (x == 3) {
				currentClip = Random.Range (0, (triangleRotationSounds.Length - 1));
				rotationPolyphony[rotationVoiceCounter].clip = triangleRotationSounds [currentClip];
				rotationPolyphony [rotationVoiceCounter].Play ();
			} else if (x == 4) {
				currentClip = Random.Range (0, (squareRotationSounds.Length - 1));
				print ("CC " + currentClip);
				rotationPolyphony[rotationVoiceCounter].clip = squareRotationSounds [currentClip];
				rotationPolyphony [rotationVoiceCounter].Play ();			
			} else if (x == 5) {
				currentClip = Random.Range (0, (pentagonRotationSounds.Length - 1));
				rotationPolyphony[rotationVoiceCounter].clip = pentagonRotationSounds [currentClip];
				rotationPolyphony [rotationVoiceCounter].Play ();
			}

			rotationPolyphony [rotationVoiceCounter].Play ();

			rotationVoiceCounter++;
			rotationVoiceCounter %= 4;
		}
	}

	//	if goal has been achieved sends value of 8
	public void GoalBubble() {
		if (audioON) {
		//	OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "bubble");
			goalVoice.Play();
		}
	}

	//	stop sound... theoretically
	void OnApplicationQuit() {
		//	message of -1 turns audio off
		if (audioON) {
			//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "onoff 0");
		}
	}
		

	void InitializeSounds() {
		triangleSounds = Resources.LoadAll<AudioClip> ("Sounds/Triangle");
		squareSounds = Resources.LoadAll<AudioClip> ("Sounds/Square");
		pentagonSounds = Resources.LoadAll<AudioClip> ("Sounds/Pentagon");

		resonatorVoiceCounter = 0;

		triangleRotationSounds = Resources.LoadAll<AudioClip> ("Sounds/RotationTri");
		squareRotationSounds = Resources.LoadAll<AudioClip> ("Sounds/RotationSqr");
		pentagonRotationSounds = Resources.LoadAll<AudioClip> ("Sounds/RotationPent");

		print (triangleRotationSounds.Length);

		rotationVoiceCounter = 0;
	}

}
