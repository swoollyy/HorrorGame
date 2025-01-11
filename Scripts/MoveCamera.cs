using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPos;

    [Range(.001f, 1f)]
    public float amount;
    [Range(1f, 30f)]
    public float frequency;
    [Range(0, 100f)]
    public float smooth;

    //sprint
    [Range(.001f, 1f)]
    public float sprintamount;
    [Range(1f, 30f)]
    public float sprintfrequency;
    [Range(0, 100f)]
    public float sprintsmooth;

    float sinAmountY;

    public PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal"));
        if (inputVector.magnitude > 0)
        {
            Vector3 pos = Vector3.zero;
            if(playerMovement.walkBob)
            {
                amount = amount;
                frequency = frequency;
                smooth = smooth;
            }
            else if (playerMovement.crouchBob)
            {
                amount = .6f;
                frequency = 6f;
                smooth = 5f;
            }
            else if (playerMovement.sprintBob)
            {
                amount = sprintamount;
                frequency = sprintfrequency;
                smooth = sprintsmooth;
            }
            pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
            pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, cameraPos.position + pos, 25 * Time.deltaTime);
        }
        else StopHeadbob();
    }
    private void StopHeadbob()
    {
        if (transform.position == cameraPos.position) return;
        transform.position = Vector3.Lerp(transform.position, cameraPos.position, 25 * Time.deltaTime);
    }
}
