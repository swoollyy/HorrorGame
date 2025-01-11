using UnityEngine;
using PrimeTween;

public class GameController : MonoBehaviour
{

    bool freezeTimer;
    float freezeGameTimer;

    bool slomoTimer;
    float slomoGameTimer;

    public GameObject xSpawner1;
    public GameObject xSpawner2;
    public GameObject zSpawner1;
    public GameObject zSpawner2;

    public bool stopBulletZoom;
    public bool firstLevel;

    public float currentPlayerLevel;

    public GammaRayBlast gammaRay;

    public ReceiveXP playerLevel;

    public RotateCam cameraControl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xSpawner1.GetComponent<ShootHomingBullet>().enabled = false;
        xSpawner2.GetComponent<ShootHomingBullet>().enabled = false;
        zSpawner1.GetComponent<ShootHomingBullet>().enabled = false;
        zSpawner2.GetComponent<ShootHomingBullet>().enabled = false;

        PrimeTweenConfig.SetTweensCapacity(1000);
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;

        gammaRay.light.enabled = false;
        gammaRay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeTimer)
            freezeGameTimer += Time.deltaTime;
        else freezeGameTimer = 0f;
        if (freezeGameTimer >= .013f)
        {
            Time.timeScale = 1;
            freezeTimer = false;
            ShakeCamera(8f, .5f, 75);
        }

        if (slomoTimer)
            slomoGameTimer += Time.deltaTime;
        else slomoGameTimer = 0f;
        if (slomoGameTimer >= .12f)
        {
            Time.timeScale = 1;
            Camera.main.orthographicSize = 5.12f;
            slomoTimer = false;
            cameraControl.lockOnPlayer = false;
            stopBulletZoom = true;
        }


        if (playerLevel.level == 1)
        {
            firstLevel = true;
        }
        else firstLevel = false;

        if (playerLevel.level == 2)
            EnableHomingBullets();

        if (playerLevel.level == 3)
            EnableGammaRay();

        currentPlayerLevel = playerLevel.level;
    }

    public void FreezeEffect()
    {
            freezeTimer = true;
            Time.timeScale = 1 / 20f;
    }

    public void ShakeCamera(float strength, float duration, int frequency)
    {
        Tween.ShakeCamera(Camera.main, strengthFactor: strength, duration: duration, frequency: frequency);
    }

    public void SloMo()
    {
        cameraControl.lockOnPlayer = true;
        slomoTimer = true;
        Time.timeScale = 1 / 8f;
    }

    public void EnableHomingBullets()
    {
        xSpawner1.GetComponent<ShootHomingBullet>().enabled = true;
        xSpawner2.GetComponent<ShootHomingBullet>().enabled = true;
        zSpawner1.GetComponent<ShootHomingBullet>().enabled = true;
        zSpawner2.GetComponent<ShootHomingBullet>().enabled = true;
    }

    public void EnableGammaRay()
    {
        gammaRay.enabled = true;
    }

}
