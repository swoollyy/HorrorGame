using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPapers : MonoBehaviour
{

    public List<GameObject> papers = new List<GameObject>();

    public GameObject paper;

    bool stopThis;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!stopThis)
        {
            for (int i = 0; i < 12; i++)
            {
                Quaternion forwardRotation = Quaternion.Euler(0f, 0f, 0f);
                int paperLoc = Random.Range(0, papers.Count);
                while (papers[paperLoc].GetComponent<OccupyCheck>().isOccupied)
                {
                    paperLoc = Random.Range(0, papers.Count);
                }
                Vector3 paperRot = new Vector3(papers[paperLoc].transform.eulerAngles.x, papers[paperLoc].transform.eulerAngles.y + 90, papers[paperLoc].transform.eulerAngles.z);
                forwardRotation.eulerAngles = paperRot;
                GameObject instPaper = Instantiate(paper, papers[paperLoc].transform.position, forwardRotation, papers[paperLoc].transform);
                    papers[paperLoc].GetComponent<OccupyCheck>().isOccupied = true;
                if (i == 11)
                    stopThis = true;
            }
        }

    }
}
