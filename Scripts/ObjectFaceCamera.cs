using UnityEngine;
using UnityEngine.UI;

public class ObjectFaceCamera : MonoBehaviour
{

    Camera cam;

    public GameObject ally;

    public RotateCam rotateCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateCam.currentCamLocation == 0)
        transform.position = new Vector3(ally.transform.position.x - .4f, ally.transform.position.y + .15f, ally.transform.position.z);
        else if(rotateCam.currentCamLocation == 1)
        transform.position = new Vector3(ally.transform.position.x + .4f, ally.transform.position.y - .15f, ally.transform.position.z);
        else if (rotateCam.currentCamLocation == 2)
            transform.position = new Vector3(ally.transform.position.x, ally.transform.position.y - .15f, ally.transform.position.z - .4f);
        else if (rotateCam.currentCamLocation == 3)
            transform.position = new Vector3(ally.transform.position.x, ally.transform.position.y + .15f, ally.transform.position.z - .4f);

        transform.LookAt(cam.transform);

    }


    public void UpdateRotation()
    {
        Quaternion currentRotation = transform.rotation;
        currentRotation.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.rotation = currentRotation;
        Vector3 lookAtCam = new Vector3(0f, cam.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Euler(lookAtCam);
    }

}
