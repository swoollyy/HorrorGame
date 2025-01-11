using UnityEngine;

public class ShootHomingBullet : MonoBehaviour
{

    float timer;

    public GameObject homingBullet;

    public GameController gc;

    float randomShootTime;

    float minShootValue = 5f;
    float maxShootValue = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (gc.currentPlayerLevel == 1)
            randomShootTime = Random.Range(5f, 10f);
        else
            randomShootTime = Random.Range(minShootValue, maxShootValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer += Time.deltaTime;
        }

        if (timer >= randomShootTime)
        {
            Instantiate(homingBullet, transform.position, transform.rotation);
            randomShootTime = Random.Range(minShootValue, maxShootValue);
            timer = 0;
        }
    }

    public void LevelUp()
    {
        minShootValue -= 2 / Mathf.Pow(gc.currentPlayerLevel, 1.95f);
        maxShootValue -= 2 / Mathf.Pow(gc.currentPlayerLevel, 1.95f);
    }

}
