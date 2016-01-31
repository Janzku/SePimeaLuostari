using UnityEngine;
using System.Collections;

public class DirectionGame : MonoBehaviour 
{
    public int Directions;
    public int completed;
    public int progress;
    public string[] directionList;
    public string[] activatedDirections;
    public GameObject UpDirection;
    public GameObject DownDirection;
    public GameObject LeftDirection;
    public GameObject RightDirection;
    public GameObject CenterObject;
    direction downD;
    direction upD;
    direction leftD;
    direction rightD;
    direction centerD;
    private bool fail = false;
	// Use this for initialization
	void Start () 
    {
        newDirectionList();
        downD = DownDirection.GetComponent<direction>();
        upD =UpDirection.GetComponent<direction>();
        leftD = LeftDirection.GetComponent<direction>();
        rightD = RightDirection.GetComponent<direction>();
        centerD = CenterObject.GetComponent<direction>();

        activatedDirections = new string[completed+1];
       
       

	
	}

    void Update()
    {
        theDirectionGame();
 
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

   void directionCheck(string dName)
   {
      
            var corObj = GameObject.Find(dName);
            var corD = corObj.GetComponent<direction>();

            if (corD.inside == false)
            {
                
                if (corD.PlayerLooking(10, false))
                {
                    if (progress >= completed + 1)
                    {
                        Debug.Log("failed. Return to center");
                        fail = true;
                    }
                    if (fail != true)
                    {
                        Debug.Log(dName + " true");
                        corD.inside = true;
                        Debug.Log("progress: " + progress);
                        activatedDirections[progress] = dName;
                        if (corObj.name != "Up")
                        {
                            upD.inside = false;
                        }

                        if (corObj.name != "Down")
                        {
                            downD.inside = false;
                        }

                        if (corObj.name != "Left")
                        {
                            leftD.inside = false;
                        }

                        if (corObj.name != "Right")
                        {
                            rightD.inside = false;
                        }
                    }

                    
                    progress++;
                   
                    
                    
                   
                }
            }
      
   }

   void returnToCenter()
   {
       if (centerD.PlayerLooking(25, false))
       {
           if (fail)
           {
               progress = 0;
               activatedDirections = new string[completed + 1];
               upD.inside = false;
               downD.inside = false;
               leftD.inside = false;
               rightD.inside = false;
               Debug.Log("Returned to the center.Now try again");

           }
 
       }
       


       if (progress == completed+1)
           if (centerD.PlayerLooking(25, false))
           {
               

               if (isPathCorrect())
               {
                   
                   completed++;
                   progress = 0;
                   activatedDirections = new string[completed + 1];
                   upD.inside = false;
                   downD.inside = false;
                   leftD.inside = false;
                   rightD.inside = false;

               }
               else
               {
                   progress = 0;
                   activatedDirections = new string[completed + 1];
                   upD.inside = false;
                   downD.inside = false;
                   leftD.inside = false;
                   rightD.inside = false;
 
               }
               
           }
       
       }
   bool isPathCorrect()
   {
       for (int j = 0; j < activatedDirections.Length; j++)
			{
                if(activatedDirections[j] != directionList[j])
                {
                    Debug.Log("failed. Wrong pattern");
                    return false;
                }
                
			 
			}
       Debug.Log("pattern was correct!");
       return true;
   }

   void theDirectionGame()
   {
       directionCheck("Up");
       directionCheck("Down");
       directionCheck("Left");
       directionCheck("Right");
       returnToCenter();
       if (completed == directionList.Length)
       {
           Debug.Log("Voitit Pelin");
       }
 
   }
 
 
    

	
}
