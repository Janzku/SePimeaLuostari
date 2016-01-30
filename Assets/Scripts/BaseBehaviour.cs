using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{

    private Vector3 startingPosition;

    public bool isPlayerLooking = false;

    void Start()
    {
        startingPosition = transform.localPosition;
        SetGazedAt(false);
    }

    public void SetGazedAt(bool gazedAt)
    {
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
        isPlayerLooking = gazedAt;
    }

    public bool PlayerLooking(float angle, bool ignoreHeight = true)
    {
        if (isPlayerLooking)
            return true;

        var look = Camera.main.transform.forward;
        var pos = transform.position;

        if (ignoreHeight)
        {
            look.y = 0;
            pos.y = 0;
        }

        var ang = Vector3.Angle(Camera.main.transform.forward, transform.position);

        return ang < angle;
    }
}
