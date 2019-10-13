using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
	public AudioClip explosion;
	public AudioClip pickup;
	public AudioClip thrustMove;
	public AudioClip thrustUp;
	public AudioClip backgroundMusic;

	public AudioSource audioSource;
	// Use this for initialization

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}
	
	public void playExplosion(){
		audioSource.PlayOneShot(explosion, 0.7f);
	}

	public void playPickup(){
		audioSource.PlayOneShot(pickup, 0.7f);
	}
}
