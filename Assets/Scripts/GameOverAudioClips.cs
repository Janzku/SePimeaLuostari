using UnityEngine;
using System.Collections.Generic;

public class GameOverAudioClips : MonoBehaviour
{
    public List<AudioClip> AudioClips = null;

    CardboardAudioSource CAS = null;

	// Use this for initialization
	void Start ()
    {
        CAS = GetComponent<CardboardAudioSource>();

        CAS.Stop();
        CAS.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        CAS.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(CAS.isPlaying == false)
        {
            Debug.Log("Audio listened, quit game!");
            Application.Quit();
        }
	}
}
