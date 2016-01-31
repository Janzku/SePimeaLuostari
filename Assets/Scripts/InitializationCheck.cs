using UnityEngine;
using System.Collections;

public class InitializationCheck : MonoBehaviour
{
    public static bool firstInit = false;

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
            DestroyImmediate(this.gameObject);
        }
	}
	
}
