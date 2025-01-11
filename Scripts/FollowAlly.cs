using UnityEngine;

public class FollowAlly : MonoBehaviour
{

    public GameObject ally;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ally.transform.position;
    }
}
