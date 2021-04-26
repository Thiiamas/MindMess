using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camLeft;
    [SerializeField] CinemachineVirtualCamera camRight;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerTransform.position.x - transform.position.x);
        if (playerTransform.position.x - transform.position.x <= 0)
        {
            //joueur a gauche
            camLeft.enabled = true;
            camLeft.Priority = 10;
            camRight.Priority = 5;
            camRight.enabled = false;
        }
        else
        {
            camRight.enabled = true;
            camLeft.Priority = 5;
            camRight.Priority = 10;
            camLeft.enabled = false;
        }
    }
}
