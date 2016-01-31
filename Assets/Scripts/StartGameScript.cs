using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour
{
    private GameObject Player = null;

    private float m_timer = 3.0f;
    private bool m_beginGame = false;

	// Use this for initialization
	void Start ()
    {
        Player = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Player.transform.rotation.x <= -0.4f)
        {
            Debug.Log("Beginning game");
            m_beginGame = true;
        }

        if(m_beginGame)
        {
            m_timer -= Time.deltaTime;

            if(m_timer <= 0.0f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
            }
        }
    }
}
