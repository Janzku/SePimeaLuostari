using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab = null;
    private int spawnTimer = 240;
    private int spawnDistance = 4;
    private float randomDirection;

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.S))
        if (spawnTimer >= 300)
        {
            GameObject _go = Instantiate(EnemyPrefab);
            _go.transform.SetParent(this.gameObject.transform);
            _go.transform.position = new Vector3(0 , 0, spawnDistance);
            randomDirection = Random.Range(-90.0f, 90.0f);
            _go.transform.RotateAround(Vector3.zero, Vector3.up, randomDirection);
            _go.transform.LookAt(new Vector3(0, _go.transform.position.y, 0));

            spawnTimer = 0;
        }
        else
        {
            spawnTimer++;
        }
    }
}
