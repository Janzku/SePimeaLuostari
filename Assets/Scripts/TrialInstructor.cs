using UnityEngine;
using System.Collections;

public class TrialInstructor : MonoBehaviour {

    public MonoBehaviour Trial = null;
    private CardboardAudioSource AS;
    public AudioClip IntroSpeech = null;
    public AudioClip LetsStart = null;
    private bool introStarted = false;
    private bool letsStartStarted = false;
    private bool trialIntroFinished = false;
    //public bool introFinished = true;

	// Use this for initialization
	void Start () {
        AS = GetComponent<CardboardAudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            DebugSkip();
        }
	    if (!AS.isPlaying && !trialIntroFinished)
        {
            if (letsStartStarted)
            {
                StartTrial();
            }
            else if (introStarted)
            {
                StartLetsStart();
            }
            else
            {
                // "psst" finished, start trial intro
                SwapSound(IntroSpeech);
                introStarted = true;
            }
        }
	}
    void SwapSound(AudioClip sound, float pitch = 1.0f)
    {
        Debug.Log("asd");
        AS.Stop();
        AS.clip = sound;
        AS.volume = 1;
        AS.pitch = pitch;
        AS.loop = false;
        AS.Play();
    }

    void StartTrial()
    {
        Trial.enabled = true;
        //Destroy(this.gameObject);
        trialIntroFinished = true;
    }

    void StartLetsStart() {
        SwapSound(LetsStart);
        letsStartStarted = true;
    }

    void DebugSkip()
    {
        StartLetsStart();
    }
}
