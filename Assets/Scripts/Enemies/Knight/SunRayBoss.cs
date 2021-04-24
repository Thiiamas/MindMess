using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRayBoss : MonoBehaviour
{
    [SerializeField] Transform transform;
    public Transform initTransform;
    public Transform lightTransform;
    private void Start()
    {
        lightTransform = GetComponentInChildren<Transform>();
        initTransform = lightTransform;
    }
    public void PointsTowards(Transform target)
    {
        //Makes light point toward the player
        Vector3 vector = (transform.position - target.position);
        Quaternion rotation = lightTransform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        eulerAngles.z = 180 - Vector2.Angle(transform.up, vector);
        if (lightTransform.position.x - target.position.x < 0)
        {
            eulerAngles.z = -eulerAngles.z;
        }
        rotation.eulerAngles = eulerAngles;
        lightTransform.rotation = rotation;
    }

    public void ResetTransform() 
    {
        lightTransform.position = initTransform.position;
        lightTransform.rotation = initTransform.rotation;
    }

}

