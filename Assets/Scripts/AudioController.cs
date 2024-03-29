﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
	public AudioClip explosion;
	public AudioClip pickup;
	public AudioClip thrustMove;
	public AudioClip thrustUp;
	public AudioClip buildMusic;
	public AudioClip launchMusic;
	public AudioClip fuelAlarm;
	public AudioClip starport;
	public AudioClip purchase;

	public AudioSource audioSource;
	// Use this for initialization

	void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.2f;
	}
	
	public void playExplosion(){
		audioSource.PlayOneShot(explosion, 0.3f);
	}

	public void playPickup(){
		audioSource.PlayOneShot(pickup, 0.3f);
	}

	public void playThrustMove(){
		audioSource.PlayOneShot(thrustMove, 0.3f);
	}

	public void playStarport(){
		audioSource.PlayOneShot(starport, 0.3f);
	}

	public void playPurchase(){
		audioSource.PlayOneShot(purchase, 0.3f);
	}

	public void playBuildMusic(){
		audioSource.clip = buildMusic;
		audioSource.loop = true;
		audioSource.Play();
	}

	public void playLaunchMusic(){
		audioSource.clip = launchMusic;
		audioSource.loop = true;
		audioSource.Play();
	}
}
