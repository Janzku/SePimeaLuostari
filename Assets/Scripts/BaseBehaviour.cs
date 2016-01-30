using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public bool PlayerLooking(float angle = 10, bool ignoreHeight = true)
    {
        var look = Camera.main.transform.forward;
        var pos = transform.position;

        if (ignoreHeight)
        {
            look.y = 0;
            pos.y = 0;
        }

        var ang = Vector3.Angle(Camera.main.transform.forward, transform.position);

        GetComponent<Renderer>().material.color = ang < angle ? Color.green : Color.red;

        return ang < angle;
    }
}
