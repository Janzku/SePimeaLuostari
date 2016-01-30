using UnityEngine;
using System.Collections;

public class InitializationCheck : MonoBehaviour
{
    private static bool firstInit = false;

	// Use this for initialization
	void Awake ()
    {
        if(firstInit == false)
        {
            DontDestroyOnLoad(this);
            firstInit = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
	
}
