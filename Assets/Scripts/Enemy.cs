using UnityEngine;
using System.Collections;

public class Enemy : BaseBehaviour
{
    private int consecutiveLookedAtFrames = 0;
    private bool lookedAt = false;

    void Update()
    {
        if (PlayerLooking(10))
        {
            consecutiveLookedAtFrames++;
            lookedAt = true;
        }
        else
        {
            consecutiveLookedAtFrames = 0;
            lookedAt = false;
        }
        DestroyCheck();
        Move();
        KillCheck();
    }

    void KillCheck()
    {
        if (transform.position.x <= 0.5f && transform.position.x >= -0.5f || transform.position.z <= 0.5f && transform.position.z >= -0.5f)
        {
            Debug.Log("DOD");
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
