using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Vcam0 : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    Transform playerTransform;
    [SerializeField] Transform CamTriggerX;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x < CamTriggerX.position.x)
        {
            virtualCamera.Priority = 10;
        }
        else
        {
            virtualCamera.Priority = 0;
        }
    }
}
