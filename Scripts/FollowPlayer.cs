using UnityEngine;
using UnityEngine.AI;
using PrimeTween;



public class FollowPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject player;

    bool beginMoving;
    bool currentlyEnraged;
    bool shrinkDown;

    float rageTime;
    float rageTimer;
    float enragedTamer;


    void Start()
    {
        GetComponent<NavMeshAgent>().speed = 1.5f;
        transform.localScale = new Vector3(0f, 0f, 0f);
        player = GameObject.FindWithTag("Player");
        Tween.Scale(transform, endValue: .7f, duration: .15f).OnComplete(() =>
        Tween.Scale(transform, endValue: .5f, duration: .15f, endDelay: .1f).OnComplete(() =>
        EnableMovement()));
    }

    // Update is called once per frame
    void Update()
    {
        if (beginMoving)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            if (!currentlyEnraged)
                rageTimer += Time.deltaTime;
        }
        else GetComponent<NavMeshAgent>().SetDestination(transform.position);

        if(rageTimer >= 6f)
        {
            rageTime = Random.Range(0, 5);
            rageTimer = 0f;
        }

        if(rageTime == 1)
        {
            enragedTamer += Time.deltaTime;
            GetComponent<NavMeshAgent>().speed = 4.5f;
            currentlyEnraged = true;
        }
        if(enragedTamer >= 8f)
        {
            GetComponent<NavMeshAgent>().speed = 1.5f;
            enragedTamer = 0;
            currentlyEnraged = false;
            shrinkDown = true;
        }

        if (currentlyEnraged)
            Tween.Scale(transform, endValue: .8f, duration: 8f);

        if(shrinkDown)
        {
            beginMoving = false;
            Tween.Scale(transform, endValue: .4f, duration: .15f).OnComplete(() =>
Tween.Scale(transform, endValue: .5f, duration: .10f).OnComplete(() =>
EnableMovement()));
        }

    }

    void EnableMovement()
    {
        shrinkDown = false;
        beginMoving = true;
        
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
}
