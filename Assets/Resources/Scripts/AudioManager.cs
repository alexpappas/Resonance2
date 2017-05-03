using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

	AudioClip[] goalSounds;
	AudioClip[] goalTailSounds;

	public AudioSource[] rotationPolyphony;
	int rotationVoiceCounter;

	public AudioSource playerVoice;
	int currentClip;

	public AudioSource goalVoice;
	public AudioSource goalTailVoice;

	public AudioSource wallVoice;

	AudioClip[] ambiVoice1SoundsPent;
	AudioClip[] ambiVoice1SoundsSqr;
	AudioClip[] ambiVoice1SoundsTri;

	AudioClip[] ambiVoice2SoundsPent;
	AudioClip[] ambiVoice2SoundsSqr;
	AudioClip[] ambiVoice2SoundsTri;
	AudioClip[] ambiVoiceSpice;

	public AudioSource ambientVoice1;
	public AudioSource ambientVoice2;
	public AudioSource ambientVoice3;

	int playerSides = 5;

	public AudioMixerSnapshot normal;
	public AudioMixerSnapshot goalReached;

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

		//	allows for smoother transitions between scenes
		DontDestroyOnLoad (this.transform);

		//print ("AM init");
	}

	void Start () {
		//print ("AM start");
		InvokeRepeating("LowAmbient", 5, 7);
		InvokeRepeating ("HighAmbient", 9, 13);
		InvokeRepeating ("SpiceAmbient", 5, 17);
	}
		
	//	sends player pulse information to Max
	public void PlayerPulse(int x) {
		if (audioON) {

			playerSides = x - 1;

			if (x == 3) {
				currentClip = 1 + Random.Range (0, (triangleSounds.Length - 1));
				playerVoice.clip = triangleSounds [currentClip];
			} else if (x == 4) {
				currentClip = 1 + Random.Range (0, (squareSounds.Length - 1));
				playerVoice.clip = squareSounds [currentClip];			
			} else if (x == 5) {
				currentClip = 1 + Random.Range (0, (pentagonSounds.Length - 1));
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
			//currentClip = Random.Range (0, (goalSounds.Length - 1));
			//goalVoice.clip = goalSounds [currentClip];
			//goalVoice.Play ();

			//currentClip = Random.Range (0, (goalSounds.Length - 1));
			//goalTailVoice.clip = goalTailSounds[0];
			if (playerSides == 5) {
				goalTailVoice.clip = goalTailSounds[0];
			} else if (playerSides == 4) {
				goalTailVoice.clip = goalTailSounds[1];
			} else {
				goalTailVoice.clip = goalTailSounds[2];
			}

			goalReached.TransitionTo (0.1f);
			goalTailVoice.Play ();

		}
	}
		
	public void SoundBarrier() {
		if (audioON) {
			//	OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", "bubble");
			currentClip = Random.Range (0, (goalSounds.Length - 1));
			wallVoice.clip = goalSounds [currentClip];
			wallVoice.Play ();

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
		//	initialize pulse sounds
		triangleSounds = Resources.LoadAll<AudioClip> ("Sounds/Triangle");
		squareSounds = Resources.LoadAll<AudioClip> ("Sounds/Square");
		pentagonSounds = Resources.LoadAll<AudioClip> ("Sounds/Pentagon");

		resonatorVoiceCounter = 0;

		//	initialize rotation sounds
		triangleRotationSounds = Resources.LoadAll<AudioClip> ("Sounds/RotationTri");
		squareRotationSounds = Resources.LoadAll<AudioClip> ("Sounds/RotationSqr");
		pentagonRotationSounds = Resources.LoadAll<AudioClip> ("Sounds/RotationPent");

		rotationVoiceCounter = 0;

		//	initialize goal sounds
		goalSounds = Resources.LoadAll<AudioClip> ("Sounds/Goal");
		goalTailSounds = Resources.LoadAll<AudioClip> ("Sounds/GoalChord");

		//	initialize ambient sounds
		ambiVoice1SoundsPent = Resources.LoadAll<AudioClip> ("Sounds/Ambient/V1Pent");
		ambiVoice1SoundsSqr = Resources.LoadAll<AudioClip> ("Sounds/Ambient/V1Sqr");
		ambiVoice1SoundsTri = Resources.LoadAll<AudioClip> ("Sounds/Ambient/V1Tri");

		//	initialize ambient sounds
		ambiVoice2SoundsPent = Resources.LoadAll<AudioClip> ("Sounds/Ambient/V2Pent");
		ambiVoice2SoundsSqr = Resources.LoadAll<AudioClip> ("Sounds/Ambient/V2Sqr");
		ambiVoice2SoundsTri = Resources.LoadAll<AudioClip> ("Sounds/Ambient/V2Tri");

		ambiVoiceSpice = Resources.LoadAll<AudioClip> ("Sounds/Ambient/Spice");

	}

	void LowAmbient() {

		if (playerSides == 5) {
			currentClip = Random.Range (0, (ambiVoice1SoundsPent.Length - 1));
			ambientVoice1.clip = ambiVoice1SoundsPent [currentClip];
		} else if (playerSides == 4) {
			currentClip = Random.Range (0, (ambiVoice1SoundsSqr.Length - 1));
			ambientVoice1.clip = ambiVoice1SoundsSqr [currentClip];
		} else {
			currentClip = Random.Range (0, (ambiVoice1SoundsTri.Length - 1));
			ambientVoice1.clip = ambiVoice1SoundsTri [currentClip];
		}

		ambientVoice1.Play ();

	}

	void HighAmbient() {
		
		if (playerSides == 5) {
			currentClip = Random.Range (0, (ambiVoice2SoundsPent.Length - 1));
			ambientVoice2.clip = ambiVoice2SoundsPent [currentClip];
		} else if (playerSides == 4) {
			currentClip = Random.Range (0, (ambiVoice2SoundsSqr.Length - 1));
			ambientVoice2.clip = ambiVoice2SoundsSqr [currentClip];
		} else {
			currentClip = Random.Range (0, (ambiVoice2SoundsTri.Length - 1));
			ambientVoice2.clip = ambiVoice2SoundsTri [currentClip];
		}

		ambientVoice2.Play ();
	}

	void SpiceAmbient() {

		currentClip = Random.Range (0, (ambiVoiceSpice.Length - 1));
		ambientVoice3.clip = ambiVoiceSpice [currentClip];

		ambientVoice3.Play ();

	}

	public void NextScene() {
		playerSides = 5;
		normal.TransitionTo (0.1f);
		print("next");
	}
}
