using UnityEngine;

public class MouseToMove : MonoBehaviour
{

    public LayerMask layerMask;

    bool moveToPoint;
    bool lowerMoveSound;

    public AudioSource mouseMoveAudio;

    public ParticleSystem clickParticle;

    int particleCount = 0;

    Vector3 reference = new Vector3(0f, 0f, 0f);
    Vector3 hitPoint;
    Vector3 position;

    public Transform posXBound;
    public Transform negXBound;
    public Transform posZBound;
    public Transform negZBound;

    KeyCode leftClick = KeyCode.Mouse0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetKey(leftClick))
        {
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                print(hit.transform.gameObject);
                hitPoint = hit.point;
                moveToPoint = true;
            }
        }

        if(Input.GetKeyDown(leftClick) && !mouseMoveAudio.isPlaying)
        {
            PlayMoveAudio();
            particleCount++;
        }
        else if(Input.GetKeyDown(leftClick) && mouseMoveAudio.isPlaying)
        {
            lowerMoveSound = true;
            particleCount++;
        }

        if(Input.GetKeyUp(leftClick))
            particleCount = 0;


        if (lowerMoveSound)
        {
            mouseMoveAudio.volume = Mathf.Lerp(mouseMoveAudio.volume, 0f, 30 * Time.deltaTime);
            if(mouseMoveAudio.volume < .001f)
            {
                PlayMoveAudio();
                lowerMoveSound = false;
            }
        }

        if (moveToPoint)
            MoveToPoint(hitPoint);

        if (Vector3.Distance(hitPoint, transform.position) < .1f)
            moveToPoint = false;


    }

    void MoveToPoint(Vector3 point)
    {

        if (InBounds())
        {
            if(particleCount == 1)
            {
                Instantiate(clickParticle, hitPoint, Quaternion.Euler(-90f, 0f, 0f));
                particleCount++;
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(point.x, transform.position.y, point.z), 15 * Time.deltaTime);
        }
        if(!InBounds())
        {
            GetInBounds();
        }


    }

    void PlayMoveAudio()
    {
        mouseMoveAudio.pitch = Random.Range(.9f, 1.1f);
        mouseMoveAudio.volume = .085f;
        mouseMoveAudio.Play();
    }
    
    bool InBounds()
    {
        if (hitPoint.x < negXBound.position.x)
            return false;
        else if (hitPoint.x > posXBound.position.x)
            return false;

        else if (hitPoint.z < negZBound.position.z)
            return false;
        else if (hitPoint.z > posZBound.position.z)
            return false;
        else return true;
    }

    void GetInBounds()
    {
        if (hitPoint.x < negXBound.position.x)
            hitPoint.x = negXBound.position.x;
        else if (hitPoint.x > posXBound.position.x)
            hitPoint.x = posXBound.position.x;

        if (hitPoint.z < negZBound.position.z)
            hitPoint.z = negZBound.position.z;
        else if (hitPoint.z > posZBound.position.z)
            hitPoint.z = posZBound.position.z;
        if (particleCount == 0)
        {
            Instantiate(clickParticle, hitPoint, Quaternion.Euler(-90f, 0f, 0f));
            particleCount++;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(hitPoint.x, transform.position.y, hitPoint.z), 15 * Time.deltaTime);
        if (Vector3.Distance(position, transform.position) < .25f)
            moveToPoint = false;

    }

}
