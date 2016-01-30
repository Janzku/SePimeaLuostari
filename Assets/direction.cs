using UnityEngine;
using System.Collections;

public class direction : BaseBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLooking(50, false))
        {
            GetComponent<CardboardAudioSource>().mute = false;
        }
        else
        {
            GetComponent<CardboardAudioSource>().mute = true;
        }
 
        
    }
}
