using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FaceCamera : MonoBehaviour
{

    public TMP_Text text;
    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        text.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
