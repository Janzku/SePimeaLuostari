using UnityEngine;
using System.Collections;

public class customController : MonoBehaviour 
{
    private Quaternion cameraRotation;
    public  Vector3 mousePos;

    private bool customControlEnabled = false;
    void Start()
    {
        customControlEnabled = !Cardboard.SDK.VRModeEnabled;
        cameraRotation = gameObject.transform.localRotation ;
    }

    private void mouseControl()
    {
        var childTransform = transform.GetChild(0);
        mousePos.x = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Mouse X");
        mousePos.y = Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Mouse Y");
        //mousePos = Input.mousePosition;
        transform.localRotation = Quaternion.EulerRotation(0, mousePos.x * 0.01f, 0) * transform.localRotation;
        childTransform.localRotation *= Quaternion.EulerRotation(mousePos.y * -0.01f, 0, 0);

        childTransform.localRotation = new Quaternion(
            Mathf.Clamp(childTransform.localRotation.x, -0.6f, 0.6f),
            childTransform.localRotation.y,
            childTransform.localRotation.z,
            childTransform.localRotation.w
            ); 

        cameraRotation[0] = mousePos.x;
        

        //gameObject.transform.localRotation = cameraRotation;
       
 
    }

    void Update()
    {
        if (customControlEnabled)
        {
            mouseControl();
 
        }
       
    }

}
