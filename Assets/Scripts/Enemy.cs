using UnityEngine;
using System.Collections;

public class Enemy : BaseBehaviour
{
    private int consecutiveLookedAtFrames = 0;
    private bool lookedAt = false;
    private bool almostLookedAt = false;

    private CardboardAudioSource AS;

    void Start()
    {
        AS = GetComponent<CardboardAudioSource>();
        AS.volume = 0;
    }

    void Update()
    {
        PlayerLookCheck();
        FadeInVolume();
        DestroyCheck();
        Move();
        PlayerKillCheck();
    }

    void PlayerLookCheck()
    {
        if (PlayerLooking(20))
        {
            if (PlayerLooking(5))
            {
                consecutiveLookedAtFrames = consecutiveLookedAtFrames + 2;
                lookedAt = true;
                AS.pitch = Random.Range(0.5f, 2.5f);
                AS.volume = Random.Range(0f, 1.0f);
            }
            else
            {
                consecutiveLookedAtFrames++;
                almostLookedAt = true;
                lookedAt = false;
                //AS.pitch = Random.Range(0.5f, 2.5f);
                AS.volume = Random.Range(0f, 1.0f);
            }
        }
        else
        {
            consecutiveLookedAtFrames = 0;
            lookedAt = false;
            almostLookedAt = false;
            AS.pitch = 1;
            AS.volume = 1; // should maybe be ignored if volume ramp up is still going...
        }
    }

    void PlayerKillCheck()
    {
        if (transform.position.x <= 0.5f && transform.position.x >= -0.5f)
        {
            if (transform.position.z <= 0.5f && transform.position.z >= -0.5f)
            {
                Debug.Log(transform.position.x);
                Debug.Log(transform.position.z);
                Debug.LogError("DOD");
                Destroy(this.gameObject);
            }
        }
    }

    void DestroyCheck()
    {
        if (consecutiveLookedAtFrames >= 180)
        {
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        if (lookedAt)
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        else if (almostLookedAt)
        {
            // doesn't move
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    void FadeInVolume()
    { 
        if (AS.volume < 1)
        {
            float volumeStep = 0.0125f;
            float newVolume = AS.volume + volumeStep;
            if (newVolume > 1)
            {
                newVolume = 1.0f;
            }
            AS.volume = newVolume;
        }
    }
}
