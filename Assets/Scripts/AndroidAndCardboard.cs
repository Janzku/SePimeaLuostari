using UnityEngine;
using System.Collections;

public class AndroidAndCardboard : MonoBehaviour
{
    void LateUpdate()
    {
        Cardboard.SDK.UpdateState();
        if (Cardboard.SDK.BackButtonPressed)
        {
            Application.Quit();
        }
    }
}
