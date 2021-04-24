using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKnightRoom : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] BlueKnight blueKnight;

    [Header("Room")]
    [SerializeField] SunRayBoss sunRay1;
    [SerializeField] SunRayBoss sunRay2;
    [SerializeField] BossRoomLight roomLight;
    public int SunPhasecount = 0;
    [SerializeField] GameObject FiringSun;
    [SerializeField] Transform SunKnightPoint;

    const int SUN_HURT_COUNT = 1;
    int hurtCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (blueKnight.IsTired)
        {
            TiredPhase();
        } else
        {
            defaultPhase();
        }
        //sunPhase

        if (!blueKnight.SunPhase && blueKnight.health < blueKnight.maxHealth - 20 && SunPhasecount == 0)
        {
            SunPhasecount = 1;
            blueKnight.SunPhase = true;
        } else if (!blueKnight.SunPhase && blueKnight.health < blueKnight.maxHealth - 40 && SunPhasecount == 1)
        {
            SunPhasecount = 2;
            blueKnight.SunPhase = true;
        }

        if (blueKnight.SunPhase && FiringSun.active != true)
        {
            FiringSun.SetActive(true);
        } else if (!blueKnight.SunPhase && FiringSun.active == true)
        {
            FiringSun.SetActive(false);
        }

        if (blueKnight.SunPhase)
        {
            SunPhase();
        }
    }

    void defaultPhase()
    {
        if (roomLight.light.intensity <= roomLight.initIntensity)
        {
            StartCoroutine(roomLight.ResetInt());
        }
        if (!sunRay1.lightTransform.Equals(sunRay1.initTransform))
        {
            sunRay1.ResetTransform();
        }
        if (!sunRay2.lightTransform.Equals(sunRay2.initTransform))
        {
            sunRay2.ResetTransform();
        }
    }
    void TiredPhase()
    {
        if (roomLight.light.intensity >= 0.2f)
        {
            StartCoroutine(roomLight.Dimlight());
        }
        sunRay1.PointsTowards(blueKnight.transform);
        sunRay2.PointsTowards(blueKnight.transform);
    }

    void SunPhase()
    {
        blueKnight.Stick(SunKnightPoint.position);
    }
    public void HurtWhileSun()
    {
        hurtCount++;
        if (hurtCount >= SUN_HURT_COUNT)
        {
            blueKnight.SunPhase = false;
            hurtCount = 0;
        }
    }
    public void InitSunPhase()
    {
        FiringSun.SetActive(true);
        transform.position = SunKnightPoint.position;
    }

    public void endSunPhase()
    {
        FiringSun.SetActive(false);
    }
}
