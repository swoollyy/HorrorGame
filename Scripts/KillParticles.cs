using UnityEngine;

public class KillParticles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float timer;

    public float killTimer;

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > killTimer)
            Destroy(this.gameObject);
    }
}
