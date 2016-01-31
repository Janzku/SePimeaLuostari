using UnityEngine;
using System.Collections;

public class InitializationPrefab : MonoBehaviour
{

    public GameObject Prefab = null;

	void Awake ()
    {
	    if(InitializationCheck.firstInit == false)
        {
            GameObject _go = Instantiate(Prefab);
            _go.transform.SetParent(transform);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
	}
}
