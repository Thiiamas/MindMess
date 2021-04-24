using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossRoomLight : MonoBehaviour
{
    public Light2D light;
    bool CR = false;
    public float initIntensity;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        initIntensity = light.intensity;
    }

    //a faire en routine ?
    public IEnumerator Dimlight()
    {
        if (!CR)
        {
            CR = true;
            while (light.intensity > 0.2f)
            {
                light.intensity -= 0.5f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            CR = false;
        }
    }

    public IEnumerator ResetInt()
    {
        if (!CR)
        {
            CR = true;
            while (light.intensity < initIntensity)
            {
                light.intensity += 0.5f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            CR = false;
        }
    }
}
