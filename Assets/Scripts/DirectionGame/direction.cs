using UnityEngine;
using System.Collections;

public class direction : BaseBehaviour
{
    DirectionGame Dmanager;
    private bool inside;

    
    // Use this for initialization
    void Start()
    {
        Dmanager = GameObject.Find("gameManager").GetComponent<DirectionGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLooking(50, false))
        {
            if (inside != true)
            {
                if (Dmanager.directionList[Dmanager.completed] != name)
                {
                    Debug.Log("Wrong!" + name);
                    Dmanager.completed = 0;
                    inside = true;
                }
                else
                {
                    Dmanager.completed++;
                    GetComponent<CardboardAudioSource>().mute = false;
                    Debug.Log("Correct!" + name);
                    inside = true;

                }
            }

            
        }
        else
        {

            GetComponent<CardboardAudioSource>().mute = true;
            inside = false;
        }
 
        
    }
}
