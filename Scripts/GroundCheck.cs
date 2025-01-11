using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool onGround;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Terrain")
        {
            onGround = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        onGround = false;
    }
}
