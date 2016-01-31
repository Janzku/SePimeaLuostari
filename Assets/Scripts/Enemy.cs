using UnityEngine;
using System.Collections;

public class Enemy : BaseBehaviour
{
    private int damage = 0;
    private bool lookedAt = false;
    private bool almostLookedAt = false;
    private bool volumeFadeCompleted = false;
    private bool dying = false;
    private bool attacking = false;
    private bool stunned = false;

    private CardboardAudioSource AS;

    public AudioClip MoveSound = null;
    public AudioClip DeathSound = null;
    public AudioClip AttackSound = null;
    public AudioClip StunnedSound = null;

    void Start()
    {
        AS = GetComponent<CardboardAudioSource>();
        AS.volume = 0;
    }

    void Update()
    {
        if (dying)
        {
            CheckIfDead();
        }
        else if (attacking)
        {
            CheckAttackEnd();
        }
        else
        {
            FadeInVolume();
            PlayerLookCheck();
            DestroyCheck();
            Move();
            PlayerKillCheck();
        }
    }

    void PlayerLookCheck()
    {
        if (PlayerLooking(20))
        {
            if (PlayerLooking(5))
            {
                damage = damage + 2;
                lookedAt = true;
                //AS.pitch = Random.Range(0.5f, 2.5f);
                //AS.volume = Random.Range(0f, 1.0f);
            }
            else
            {
                damage++;
                almostLookedAt = true;
                lookedAt = false;
                //AS.pitch = Random.Range(0.5f, 2.5f);
                //AS.volume = Random.Range(0f, 1.0f);
            }
            if (!stunned)
            {
                SwapSound(StunnedSound);
            }
            stunned = true;
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
            if (stunned) // AS.clip != MoveSound caused stackoverflow
            {
                SwapSound(MoveSound);
            }
            stunned = false;
        }
    }

    void PlayerKillCheck()
    {
        if (transform.position.x <= 0.5f && transform.position.x >= -0.5f)
        {
            if (transform.position.z <= 0.5f && transform.position.z >= -0.5f)
            {
                PlayerCaught();
            }
        }
    }

    void DestroyCheck()
    {
        if (damage >= 180)
        {
            StartDying();
        }
    }

    void Move()
    {
        if (lookedAt)
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        else if (stunned)
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

    void PlayerCaught()
    {
        attacking = true;
        AS.Stop();
        AS.clip = AttackSound;
        AS.volume = 1;
        AS.pitch = 1;
        AS.Play();
        AS.loop = false;
    }

    void StartDying()
    {
        dying = true;
        AS.Stop();
        AS.clip = DeathSound;
        AS.volume = 1;
        AS.pitch = 1;
        AS.Play();
        AS.loop = false;
    }

    void CheckIfDead()
    {
        if (!AS.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    void CheckAttackEnd()
    {
        if (!AS.isPlaying)
        {
            FailTrial();
        }
    }

    void FailTrial()
    {
        Debug.Log("Player is dead.");
        Destroy(this.gameObject);
        // go to game over scene
    }

    void SwapSound(AudioClip sound)
    {
        AS.Stop();
        AS.clip = sound;
        AS.volume = 1;
        AS.pitch = 1;
        AS.Play();
    }
}
