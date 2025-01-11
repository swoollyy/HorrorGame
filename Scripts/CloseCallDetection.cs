using UnityEngine;

public class CloseCallDetection : MonoBehaviour
{
    GameController gc;

    float cameraZoom;
    public bool zoomCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(zoomCam)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3.7f, 20 * Time.deltaTime);
            gc.SloMo();
        }

        if (gc.stopBulletZoom)
        {
            zoomCam = false;
            gc.stopBulletZoom = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            cameraZoom = Camera.main.orthographicSize;
            GetComponent<AudioSource>().pitch = Random.Range(.9f, 1.1f);
            if(!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
            zoomCam = true;


        }


    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            zoomCam = false;


        }


    }

}
