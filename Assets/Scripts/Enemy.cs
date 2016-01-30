using UnityEngine;
using System.Collections;

public class Enemy : BaseBehaviour
{
    private int damage = 0;
    private bool lookedAt = false;
    private bool almostLookedAt = false;
    private bool volumeFadeCompleted = false;

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
                damage = damage + 2;
                lookedAt = true;
                AS.pitch = Random.Range(0.5f, 2.5f);
                AS.volume = Random.Range(0f, 1.0f);
            }
            else
            {
                damage++;
                almostLookedAt = true;
                lookedAt = false;
                //AS.pitch = Random.Range(0.5f, 2.5f);
                AS.volume = Random.Range(0f, 1.0f);
            }
        }
        else
        {
            damage = 0;
            lookedAt = false;
            almostLookedAt = false;
            AS.pitch = 1;
            if (volumeFadeCompleted)
            {
                AS.volume = 1;
            }
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
        if (damage >= 180)
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
            if (newVolume >= 1)
            {
                newVolume = 1.0f;
                volumeFadeCompleted = true;
            }
            AS.volume = newVolume;
        }
    }
}
