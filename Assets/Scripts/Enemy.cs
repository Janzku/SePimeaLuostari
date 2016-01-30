using UnityEngine;
using System.Collections;

public class Enemy : BaseBehaviour
{
    private int consecutiveLookedAtFrames = 0;
    private bool lookedAt = false;

    private CardboardAudioSource AS;

    void Start()
    {
        AS = GetComponent<CardboardAudioSource>();
    }

    void Update()
    {
        if (PlayerLooking(10))
        {
            consecutiveLookedAtFrames++;
            lookedAt = true;
            // AS.volume -= Time.deltaTime;
            //if (AS.pitch >= 2.8)
            //{
                AS.pitch = 0.5f;
            //}
        }
        else
        {
            consecutiveLookedAtFrames = 0;
            lookedAt = false;
            // AS.volume = 1;
            AS.pitch = 1;
        }
        DestroyCheck();
        Move();
        KillCheck();
    }

    void KillCheck()
    {
        if (transform.position.x <= 0.5f && transform.position.x >= -0.5f || transform.position.z <= 0.5f && transform.position.z >= -0.5f)
        {
            Debug.LogError("DOD");
            Destroy(this.gameObject);
        }
    }

    void DestroyCheck()
    {
        if (consecutiveLookedAtFrames >= 60)
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
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
