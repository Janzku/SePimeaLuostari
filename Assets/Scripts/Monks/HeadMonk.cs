using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HeadMonk : BaseBehaviour
{
    private float m_notLookingTimer = 1.5f;

    private int m_moveDirection = 1;

    public GameObject Player = null;

    public List<AudioClip> IntroClips = null;
    private int m_curClipIndex = 0;
    private bool m_introIsOver = false;

    public List<AudioClip> EarlyActionClips = null;
    public List<AudioClip> InteruptionClips = null;
    public List<AudioClip> AskToListenClips = null;
    public List<AudioClip> ContinueClips = null;
    public AudioClip Credits = null;

    private CardboardAudioSource CAS = null;


    bool m_playerActionTriggered = false;
    bool m_playCredits = false;
    bool m_resumePreach = false;

    // Use this for initialization
    void Start ()
    {
        CAS = GetComponent<CardboardAudioSource>();

        Player = Camera.main.gameObject;
        
        CAS.Stop();
        CAS.clip = IntroClips[m_curClipIndex];
        CAS.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_playCredits)
        {
            PlayCredits();
            return;
        }
        if (m_playerActionTriggered)
        {
            LoadSceneAfterClip();
            return;
        }

        PlayerAction();

        if (PreachToPlayer())
        {
            MoveHeadPriest();

            NextIntroClip();
        }
        else
        {
            if (m_introIsOver == false)
                m_resumePreach = true;

            HeadPriestSpeeches();
        }

        
	}

    bool PreachToPlayer()
    {
        if (PlayerLooking(25))
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

    void MoveHeadPriest()
    {
        if (m_introIsOver == false)
        {
            if (transform.position.x >= 6.0f)
            {
                m_moveDirection = -1;
            }
            if (transform.position.x <= -6.0f)
            {
                m_moveDirection = 1;
            }
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 10.0f * m_moveDirection);
        }
    }

    int _helper = 0;
    void HeadPriestSpeeches()
    {
        if(CAS.isPlaying == false)
        {
            switch(_helper)
            {
                case 0:
                    CAS.clip = InteruptionClips[Random.Range(0, InteruptionClips.Count)];
                    CAS.Play();
                    break;
                case 1:
                    CAS.clip = AskToListenClips[Random.Range(0, AskToListenClips.Count)];
                    CAS.Play();
                    break;
                default: // Continue to ask player to follow.
                    CAS.clip = AskToListenClips[Random.Range(0, AskToListenClips.Count)];
                    CAS.Play();
                    break;
            }

            _helper++;
        }
    }

    void NextIntroClip()
    {

        if (CAS.isPlaying == false)
        {
            if(m_resumePreach)
            {
                CAS.clip = ContinueClips[Random.Range(0, ContinueClips.Count)];
                CAS.Play();

                m_resumePreach = false;
                m_curClipIndex--;
                _helper = 0;
                return;
            }


            m_curClipIndex++;
            if (m_curClipIndex >= IntroClips.Count)
            {
                //Debug.Log("Played all. Wait for player interaction");
                m_introIsOver = true;
                return;
            }

            CAS.Stop();
            CAS.clip = IntroClips[m_curClipIndex];
            CAS.Play();
        }
    }

    
    void PlayerAction()
    {
        if(Player.transform.rotation.x >= 0.4f && m_playerActionTriggered == false)
        {
            m_playerActionTriggered = true;
            Debug.Log("Bow");
            if(m_introIsOver == false)
            {
                CAS.Stop();
                CAS.clip = EarlyActionClips[0];
                CAS.Play();
            }
        }
        else if(Player.transform.rotation.x <= -0.4f)
        {
            m_playCredits = true;
            Debug.Log("Praise the heavens");
            if(m_introIsOver == false)
            {
                m_playerActionTriggered = true;
                CAS.Stop();
                CAS.clip = EarlyActionClips[1];
                CAS.Play();
            }
        }

        if(m_introIsOver)
        {
            if(Player.transform.rotation.y <= -0.3f || Player.transform.rotation.y >= 0.3f)
            {
                m_playerActionTriggered = true;
                // quit sound?
                Application.Quit();
                Debug.Log("Player quits"); // for editor
            }
        }
    }

    void LoadSceneAfterClip()
    {
        if(CAS.isPlaying == false)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(this.gameObject); // Load trial scene here instead.
        }
    }

    void PlayCredits()
    {
        if(CAS.isPlaying == false)
        {
            if (m_playerActionTriggered)
            {
                CAS.Stop();
                CAS.clip = Credits;
                CAS.Play();
                m_playerActionTriggered = false;
                m_resumePreach = true;
            }
            else
            {
                m_playCredits = false;
            }
        }
    }
}
