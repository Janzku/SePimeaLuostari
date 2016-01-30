using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{

    private Vector3 startingPosition;

    private bool isPlayerLooking = false;

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
}
