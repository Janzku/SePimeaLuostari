using UnityEngine;
using System.Collections;

public class TrialInstructor : MonoBehaviour {

    public MonoBehaviour Trial = null;
    private CardboardAudioSource AS;
    public AudioClip IntroSpeech = null;
    public AudioClip LetsStart = null;
    private bool introStarted = false;
    private bool letsStartStarted = false;
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
            if (letsStartStarted)
            {
                StartTrial();
            }
            if (introStarted)
            {
                SwapSound(LetsStart);
                letsStartStarted = true;
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

    void StartTrial()
    {
        Trial.enabled = true;
        Destroy(this.gameObject);
    }
}
