using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab = null;
    private int spawnTimer = 240;

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.S))
        if (spawnTimer == 300)
        {
            GameObject _go = Instantiate(EnemyPrefab);
            _go.transform.SetParent(this.gameObject.transform);
            _go.transform.position = new Vector3(Random.Range(-15, 15), 0, 5);
            _go.transform.LookAt(new Vector3(0, _go.transform.position.y, 0));

            spawnTimer = 0;
        }
        else
        {
            spawnTimer++;
        }
    }
}
