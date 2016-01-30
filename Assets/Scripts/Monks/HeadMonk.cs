using UnityEngine;
using System.Collections;

public class HeadMonk : BaseBehaviour
{
    private float m_notLookingTimer = 1.5f;

    private bool m_continuePreach = true;

    private int m_moveDirection = 1;

	// Use this for initialization
	void Start ()
    {
	   
	}
	
	// Update is called once per frame
	void Update ()
    {
        // TODO: Audio control to play throught list of clips.

        if (PreachToPlayer())
        {
            if(transform.position.x >= 6.0f)
            {
                m_moveDirection = -1;
            }
            if(transform.position.x <= -6.0f)
            {
                m_moveDirection = 1;
            }
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 10.0f * m_moveDirection);
        }
        else
        {
            // Asking player to follow clips.

            //CardboardAudio Caudio = GetComponent<CardboardAudio>();
            //CardboardAudio.
        }
	}

    bool PreachToPlayer()
    {
        if (PlayerLooking(20))
        {
            m_notLookingTimer = 0.5f;
            return true;
        }
        else
        {
            if ((m_notLookingTimer -= Time.deltaTime) <= 0.0f)
            {
                return false;
            }
        }

        return true;
    }
}
