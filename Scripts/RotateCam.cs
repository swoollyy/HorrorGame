using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{

    public List<GameObject> cameraLocations = new List<GameObject>(4);

    public Transform center;
    public Transform player;

    public bool lockOnPlayer;


    public int currentCamLocation;

    bool moveLeft;
    bool moveRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.main.transform.position = cameraLocations[0].transform.position;
        currentCamLocation = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(lockOnPlayer)
            Camera.main.transform.LookAt(player.transform.position * .78f + center.transform.position * .22f);
        else
            Camera.main.transform.LookAt(player.transform.position * .5f + center.transform.position * .5f);

        //left arrow
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentCamLocation == 3)
                currentCamLocation = 0;
            else currentCamLocation++; 
            moveLeft = true;
        }
        if(moveLeft)
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentCamLocation == 0)
                currentCamLocation = 3;
            else currentCamLocation--;
            moveRight = true;
        }
        if (moveRight)
            MoveRight();


    }

    void MoveLeft()
    {
        Vector3 startValue = Camera.main.transform.position;
            Camera.main.transform.position = Vector3.Lerp(startValue, cameraLocations[currentCamLocation].transform.position, 10 * Time.deltaTime);
            if (Vector3.Distance(Camera.main.transform.position, cameraLocations[currentCamLocation].transform.position) < .01f)
            {
                moveLeft = false;
        }




    }

    void MoveRight()
    {
        Vector3 startValue = Camera.main.transform.position;
            Camera.main.transform.position = Vector3.Lerp(startValue, cameraLocations[currentCamLocation].transform.position, 10 * Time.deltaTime);
            if (Vector3.Distance(Camera.main.transform.position, cameraLocations[currentCamLocation].transform.position) < .01f)
            {
                moveRight = false;
        }


    }
}
