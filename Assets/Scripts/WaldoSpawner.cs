using UnityEngine;
using System.Collections;

public class WaldoSpawner : MonoBehaviour
{
    public GameObject WaldoPrefab = null;

    private GameObject m_waldo = null;
    private int waldoCounter = 0;
    private float sceneStartTime;

    void Start()
    {
        sceneStartTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (m_waldo == null)
        {
            float randomDirection = Random.Range(-180.0f, 180.0f);
            int spawnDistance = 5;
            m_waldo = Instantiate(WaldoPrefab);
            m_waldo.transform.SetParent(this.gameObject.transform);
            m_waldo.transform.position = new Vector3(0, 0, spawnDistance);
            m_waldo.transform.RotateAround(Vector3.zero, Vector3.up, randomDirection);
            m_waldo.transform.LookAt(new Vector3(0, m_waldo.transform.position.y, 0));
            waldoCounter++;
        }
        if ((sceneStartTime + 30f) <= Time.realtimeSinceStartup)
        {
            FinishTrial();
        }
    }



    void FinishTrial()
    {
        if (waldoCounter >= 7)
        {
            // move to next trial
        }
        else
        {
            // game over
        }
    }
}
