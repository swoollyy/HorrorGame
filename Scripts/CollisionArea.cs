using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using PrimeTween;

public class CollisionArea : MonoBehaviour
{

    public bool lightUp = false;
    bool dimLight;



    public Image xpBar;
    public float currentXP;
    float maxXP = 200f;

    public bool isHoldingXP;

    public AllyXPBar allyXPBar;

    public Light light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightUp)
            LightUp();


        if(dimLight)
            DimLight();


        if(allyXPBar.particleXP > 0)
            isHoldingXP = true;
        else isHoldingXP = false;




    }


    void LightUp()
    {
        light.intensity = 5f;
        dimLight = true;
        lightUp = false;
    }

    void DimLight()
    {
        float intensity = light.intensity;
        light.intensity = Mathf.Lerp(intensity, 0f, 5 * Time.deltaTime);
    }

    public float AddXP(float xp)
    {
        currentXP += xp;
        if(currentXP > maxXP)
            currentXP = maxXP;
        return currentXP;
    }


}
