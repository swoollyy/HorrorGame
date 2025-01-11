using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CollectPapers : MonoBehaviour
{

    public LayerMask layerMask;

    public Material outlineMat;

    public Transform orientation;

    bool addOutline;
    bool removeOutline;

    public Image crossHair;
    public TMP_Text paperText;
    public TMP_Text paperCountText;

    public int collectedPapers = 0;

    public SpawnCreatures spawnCreatures;

    Camera cam;

    GameObject currentObj;
    GameObject currentPaper;

    public GameObject paperCollectSFX;
    public GameObject paperCollectSFX2;

    public RandomSFX sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        crossHair.enabled = false;
        paperText.enabled = false;
        spawnCreatures.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        paperCountText.text = "Papers Collected: " + collectedPapers;

        print(currentObj);

        RaycastHit hit;
        Ray ray;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit))
        {
            currentObj = hit.transform.gameObject;


            if (currentObj.tag == "Papers")
            {
                if (Vector3.Distance(transform.position, currentObj.transform.position) < 8f)
                {
                    crossHair.enabled = true;
                    paperText.text = "Click to collect";
                    currentPaper = currentObj;
                    if (!currentPaper.GetComponent<Item>().hasOutline)
                    {
                        paperText.text = "Click to collect";
                        paperText.enabled = true;
                        addOutline = true;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        currentPaper.GetComponent<Item>().isCollected = true;
                        collectedPapers++;
                        sfx.lowerValues = true;
                        spawnCreatures.lowerValues = true;
                        Instantiate(paperCollectSFX, hit.point, Quaternion.identity);
                        Instantiate(paperCollectSFX2, hit.point, Quaternion.identity);
                        currentPaper = null;
                    }
                }
            }
            else if (currentObj.tag == "PuppetMan")
                crossHair.enabled = true;
            else
            {
                paperText.text = "";
                crossHair.enabled = false;
                if (currentPaper != null && currentPaper.GetComponent<Item>().currentMaterials.Length != 1)
                    currentPaper.GetComponent<Item>().RemoveOutline();
                crossHair.enabled = false;
                currentObj = null;
                currentPaper = null;
                paperText.text = "";
                paperText.enabled = false;
            }
        }
        else
        {
        }

        if (collectedPapers == 1)
            spawnCreatures.enabled = true;


        if (addOutline)
        {
            Material[] materials = currentPaper.GetComponent<Renderer>().materials;
            Material[] newMaterials = new Material[materials.Length + 1];

            materials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = outlineMat;

            currentPaper.GetComponent<Renderer>().materials = newMaterials;
            addOutline = false;
        }
    }
}
