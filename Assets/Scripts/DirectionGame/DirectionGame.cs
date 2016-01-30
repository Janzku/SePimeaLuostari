using UnityEngine;
using System.Collections;

public class DirectionGame : MonoBehaviour 
{
    public int Directions;
    public int completed;
    public string[] directionList;

	// Use this for initialization
	void Start () 
    {
        newDirectionList();
	
	}

    void newDirectionList()
    {
        directionList = new string[Directions];

        for (int i = 0; i < directionList.Length; i++)
        {
           int direction = Random.Range(0, 4);

           switch (direction)
           {
               case 0:
                   directionList[i] = "Down";
                   break;
               case 1:
                   directionList[i] = "Up";
                   break;
               case 2:
                   directionList[i] = "Left";
                   break;
               case 3:
                   directionList[i] = "Right";
                   break;
           }

           if (i > 0)
           {
               if (directionList[i] == directionList[i - 1]) // if direction is same as previous , reroll
               {
                   direction++;
                   if (direction >= 4) // goes to 4 -> 0 to start over.
                   {
                       direction = 0;
                   }

                      switch (direction)
                       {
                           case 0:
                               directionList[i] = "Down";
                               break;
                           case 1:
                               directionList[i] = "Up";
                               break;
                           case 2:
                               directionList[i] = "Left";
                               break;
                           case 3:
                               directionList[i] = "Right";
                               break;
                       }

                   
               }
 
           }
        }


 
    }

	// Update is called once per frame
	void Update () 
    {
        if (completed == Directions)
        {
            Debug.Log("Voitit Pelin");
        }
	
	}
}
