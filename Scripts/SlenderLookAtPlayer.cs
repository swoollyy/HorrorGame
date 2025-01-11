using UnityEngine;

public class SlenderLookAtPlayer : MonoBehaviour
{

    GameObject playerLocator;

    float rngDespawnTime;
    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLocator = GameObject.FindWithTag("PlayerLocatorForPuppetMan");
        rngDespawnTime = Random.Range(7f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.LookAt(new Vector3(playerLocator.transform.position.x, transform.position.y, playerLocator.transform.position.z));

        if (timer >= rngDespawnTime)
            Destroy(this.gameObject);

    }
}
