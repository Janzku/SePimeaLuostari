using UnityEngine;
using System.Collections;

public class TrialInstructor : MonoBehaviour {

    public MonoBehaviour Trial = null;
    private CardboardAudioSource AS;
    public AudioClip IntroSpeech = null;
    private bool introStarted = false;
    //public bool introFinished = true;

	// Use this for initialization
	void Start () {
        AS = GetComponent<CardboardAudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (!AS.isPlaying)
        {
            Debug.Log("asd");
            if (introStarted)
            {
                // start trial
                //introFinished = true;
                Trial.enabled = true;
                Destroy(this.gameObject);
            }
            else
            {
                // "lets get started" finished, start trial intro
                SwapSound(IntroSpeech);
                introStarted = true;
            }

        }
	}
    void SwapSound(AudioClip sound, float pitch = 1.0f)
    {
        AS.Stop();
        AS.clip = sound;
        AS.volume = 1;
        AS.pitch = pitch;
        AS.Play();
    }
}
