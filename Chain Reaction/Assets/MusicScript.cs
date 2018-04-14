﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicScript : MonoBehaviour {

    //Audio
    [EventRef]
    public string audioMusic;

    EventInstance eventMusic;

	// Use this for initialization
	void Start () {
        eventMusic = RuntimeManager.CreateInstance(audioMusic);
        eventMusic.start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
