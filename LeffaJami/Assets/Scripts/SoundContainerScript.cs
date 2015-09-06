﻿using UnityEngine;
using System.Collections;

public class SoundContainerScript : MonoBehaviour {

	public AudioClip[] clips = new AudioClip[2];

	public enum audioClips {
		hit,
		miss
	};

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

	public void PlayAudio(audioClips state)
	{	
		switch (state) 
		{
		case audioClips.hit:
			GetComponent<AudioSource>().clip = clips[0];
			break;
		case audioClips.miss:
			GetComponent<AudioSource>().clip = clips[1];
			break;
		}

		GetComponent<AudioSource> ().Play ();
	}
}