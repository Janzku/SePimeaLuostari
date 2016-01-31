using UnityEngine;
using System.Collections;

public class Waldo : BaseBehaviour
{
        private CardboardAudioSource AS; private bool found = false;

        public AudioClip FoundSound = null;

        void Start()
        {
        AS = GetComponent<CardboardAudioSource>();
        AS.volume = 0;
    }

    void Update()
    {
        if (found)
        {
            if (!AS.isPlaying)
            {
                NextWaldo();
            }
        }
        else
        {
            PlayerLookCheck();
        }
    }

    void PlayerLookCheck()
    {
        if (PlayerLooking(60))
        {
            var look = Camera.main.transform.forward;
            var pos = transform.position;
            look.y = 0;
            pos.y = 0;
            var ang = Vector3.Angle(look, pos);
            float volumeMultiplier = 60 - ang;
            AS.volume = 1.0f / 60.0f * volumeMultiplier;
            if (PlayerLooking(4))
            {
                MakeProgress();
            }
            else
            {
                ResetProgress();
            }
        }
    }

    void MakeProgress()
    {
        AS.pitch = AS.pitch + 0.05f;

        if (AS.pitch >= 5.0f)
        {
            FoundWaldo();
        }
    }

    void ResetProgress()
    {
        AS.pitch = 1.0f;
    }

    void FoundWaldo()
    {
        found = true;
        SwapSound(FoundSound, 1.2f);
        AS.loop = false;
    }

    void NextWaldo()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {

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
