using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour
{

    float timer;

    public GameObject enemy;
    public GameObject ally;

    GameObject spawnedEnemy;

    public List<GameObject> enemies = new List<GameObject>(5);
    public GameObject closestEnemy;

    ReceiveXP playerXP;

    public GameObject spawnSFX;
    public GameObject spawnParticle;

    float spawnerTime = 7f;

    int activeEnemies;
    int count;

    bool addEnemy = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerXP = GameObject.FindWithTag("Player").GetComponent<ReceiveXP>();
        closestEnemy = spawnedEnemy;

        count++;
        spawnedEnemy = Instantiate(enemy, new Vector3(Random.Range(-6f, 6f), transform.position.y + .5f, Random.Range(-6f, 6f)), Quaternion.identity);
        Instantiate(spawnSFX, spawnedEnemy.transform.position, Quaternion.identity);
        Instantiate(spawnParticle, spawnedEnemy.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        addEnemy = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (count != 5)
        {
            timer += Time.deltaTime;
        }
        else timer = 0;


        print(count);

        if(playerXP.level != 1)
        {
            if (timer >= spawnerTime && count < 5)
        {
                count++;
                spawnedEnemy = Instantiate(enemy, new Vector3(Random.Range(-6f, 6f), transform.position.y + .5f, Random.Range(-6f, 6f)), Quaternion.identity);
                Instantiate(spawnSFX, spawnedEnemy.transform.position, Quaternion.identity);
                Instantiate(spawnParticle, spawnedEnemy.transform.position, Quaternion.Euler(-90f, 0f, 0f));
                addEnemy = true;
                timer = 0;
            }
        }
        else
        {
            if (timer >= 7f && count < 5)
        {
                count++;
                spawnedEnemy = Instantiate(enemy, new Vector3(Random.Range(-6f, 6f), transform.position.y + .5f, Random.Range(-6f, 6f)), Quaternion.identity);
                Instantiate(spawnSFX, spawnedEnemy.transform.position, Quaternion.identity);
                Instantiate(spawnParticle, spawnedEnemy.transform.position, Quaternion.Euler(-90f, 0f, 0f));
                addEnemy = true;
                timer = 0;
            }
        }




        if(addEnemy)
            AddEnemyToList(spawnedEnemy.gameObject);

    }

    void AddEnemyToList(GameObject enemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i] = enemy;
                addEnemy = false;
                break;
            }
        }
    }

    public void RemoveFromList(GameObject enemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == enemy)
            {
                enemies[i] = null;
                count--;
                for(int j = i; j < enemies.Count - 1; j++)
                {
                    enemies[j] = enemies[j + 1];
                }
            }
        }
    }

    public GameObject FindClosestEnemyToAlly(GameObject ally)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                if (i == 0)
                    closestEnemy = enemies[0];
                activeEnemies++;
                if (Vector3.Distance(enemies[i].transform.position, ally.transform.position) < Vector3.Distance(closestEnemy.transform.position, ally.transform.position))
                    closestEnemy = enemies[i];
            }
        }
        return closestEnemy;
    }

    public void LevelUp()
    {
        spawnerTime -= 2 / Mathf.Pow(playerXP.level, .45f);
    }

}
