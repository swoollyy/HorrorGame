using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool   isCollected;
    public bool hasOutline;

    public Material[] currentMaterials = new Material[2];
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentMaterials = GetComponent<Renderer>().materials;


        if (currentMaterials.Length > 1)
            hasOutline = true;

        if (isCollected)
            gameObject.SetActive(false);

    }

    public void RemoveOutline()
    {
        Material[] defaultMaterial = new Material[1];
        defaultMaterial[0] = currentMaterials[0];
        GetComponent<Renderer>().materials = defaultMaterial;
        hasOutline = false;

    }

}
