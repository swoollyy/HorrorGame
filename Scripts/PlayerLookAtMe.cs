using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLookAtMe : MonoBehaviour
{
    public bool playerIsLooking;

    Camera cam;

    GameObject hitPuppet;

    public Image crosshair;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit))
        {
            if (hit.transform.gameObject.tag == "PuppetMan")
            {
                hitPuppet = hit.transform.gameObject;

                playerIsLooking = true;
                hitPuppet.GetComponent<LookAtPlayer>().hasBeenStaredAt = true;
                
            }
            else 
            {
                playerIsLooking = false;
                if(hitPuppet != null)
                hitPuppet.GetComponent<LookAtPlayer>().hasBeenStaredAt = false;
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "PuppetMan")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

}
