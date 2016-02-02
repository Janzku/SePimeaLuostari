using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab = null;
    private int spawnTimer = 240;
    private float spawnDistance = 4.8f;
    private float randomDirection;
    private float sceneStartTime;
    private int framesToNextSpawn = 300;

    void Start()
    {
        sceneStartTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            DebugSkip();
        }
        if (spawnTimer >= framesToNextSpawn)
        {
            GameObject _go = Instantiate(EnemyPrefab);
            _go.transform.SetParent(this.gameObject.transform);
            _go.transform.position = new Vector3(0, 0, spawnDistance);
            randomDirection = Random.Range(-180.0f, 180.0f); // can spawn in any direction...but right behind is a problem, sounds just like front
            _go.transform.RotateAround(Vector3.zero, Vector3.up, randomDirection);
            _go.transform.LookAt(new Vector3(0, _go.transform.position.y, 0));

            spawnTimer = 0;
            framesToNextSpawn = framesToNextSpawn - 15;
        }
        else
        {
            spawnTimer++;
        }
        if ((sceneStartTime + 40f) <= Time.realtimeSinceStartup)
        {
            FinishTrial();
        }
}

    void FinishTrial()
    {
        Debug.Log("survived.");
        MySceneManager.NextTrial();
    }

    void DebugSkip()
    {
        FinishTrial();
    }
}
