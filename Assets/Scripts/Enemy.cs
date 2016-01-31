using UnityEngine;
using System.Collections;

public class Enemy : BaseBehaviour
{
    private int damage = 0;
    private bool lookedAt = false;
    private bool volumeFadeCompleted = false;
    private bool dying = false;
    private bool attacking = false;
    private bool stunned = false;
    private int stunPitchDirection = 1;
    private float lookingMargin = 30;
    private float preciseLookingMargin = 10;

    private CardboardAudioSource AS;

    public AudioClip MoveSound1 = null;
    public AudioClip MoveSound2 = null;
    public AudioClip DeathSound = null;
    public AudioClip AttackSound = null;
    public AudioClip StunnedSound = null;

    void Start()
    {
        AS = GetComponent<CardboardAudioSource>();
        AS.volume = 0;
        SwapSound(GetRandomMoveSound());
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
        if (PlayerLooking(lookingMargin))
        {
            if (PlayerLooking(preciseLookingMargin))
            {
                damage = damage + 2;
                lookedAt = true;
            }
            else
            {
                damage++;
                lookedAt = false;
            }
            if (!stunned)
            {
                SwapSound(StunnedSound);
            }
            stunned = true;
            AdjustStunPitch();
        }
        else
        {
            damage = 0;
            lookedAt = false;
            AS.pitch = 1;
            if (volumeFadeCompleted)
            {
                AS.volume = 1;
            }
            if (stunned) // AS.clip != MoveSound caused stackoverflow
            {
                SwapSound(GetRandomMoveSound());
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
        if (damage >= 120)
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
        MySceneManager.LostGameScene();
    }

    void SwapSound(AudioClip sound, float pitch = 1)
    {
        AS.Stop();
        AS.clip = sound;
        AS.volume = 1;
        AS.pitch = pitch;
        AS.Play();
    }

    AudioClip GetRandomMoveSound()
    {
        int rand = Random.Range(1, 3);
        if (rand == 1)
        {
            return MoveSound1;
        }
        else
        {
            return MoveSound2;
        }
    }

    void AdjustStunPitch()
    {
        //Bounces pitch up and down, speed depends on accuracy
        if (AS.pitch <= 1.5f)
        {
            stunPitchDirection = 1;
        }
        else if (AS.pitch >= 2.5f)
        {
            stunPitchDirection = -1;
        }
        var look = Camera.main.transform.forward;
        var pos = transform.position;
        look.y = 0;
        pos.y = 0;
        var ang = Vector3.Angle(look, pos);
        float extraPitchMultiplier = lookingMargin - ang;
        float extraPitch = extraPitchMultiplier * 0.01f;
        //Debug.Log(extraPitch);
        AS.pitch = AS.pitch + extraPitch * stunPitchDirection;

        //initial version: changes pitch depending on accuracy
        //var look = Camera.main.transform.forward;
        //var pos = transform.position;
        //look.y = 0;
        //pos.y = 0;
        //var ang = Vector3.Angle(look, pos);
        //float extraPitchMultiplier = 20 - ang;
        //AS.pitch = 1.0f + 0.10f * extraPitchMultiplier;
    }
}
