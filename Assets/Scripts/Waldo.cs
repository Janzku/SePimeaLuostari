using UnityEngine;
using System.Collections;

public class Waldo : BaseBehaviour
{
    public bool waldoFound = false;
    private CardboardAudioSource AS;

    void Start()
    {
        AS = GetComponent<CardboardAudioSource>();
        AS.volume = 0;
    }

    void Update()
    {
        PlayerLookCheck();
    }

    void PlayerLookCheck()
    {
        if (PlayerLooking(60))
        {
            var look = Camera.main.transform.forward;
            var pos = transform.position;
            var ang = Vector3.Angle(look, pos);
            float volumeMultiplier = 60 - ang;
            AS.volume = 1.0f / 60.0f * volumeMultiplier;
            if (PlayerLooking(3))
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
        Debug.Log("you found waldo!");
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {

    }
}
