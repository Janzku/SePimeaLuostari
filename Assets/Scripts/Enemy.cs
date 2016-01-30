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
        if (PlayerLooking(20))
        {
            consecutiveLookedAtFrames++;
            lookedAt = true;
            AS.pitch = Random.Range(0.5f, 2.5f);
        }
        else
        {
            consecutiveLookedAtFrames = 0;
            lookedAt = false;
            AS.pitch = 1;
        }
        DestroyCheck();
        Move();
        PlayerKillCheck();
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
