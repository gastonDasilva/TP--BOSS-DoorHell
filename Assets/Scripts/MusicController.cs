using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public AudioClip musicClip;

    private AudioSource audioController;

	// Use this for initialization
	void Start () {
        audioController = GetComponent<AudioSource>();
        audioController.clip = musicClip;
        audioController.Play();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
