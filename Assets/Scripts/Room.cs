using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] int fromEntry1;
    [SerializeField] int fromEntry2;
    [SerializeField] int fromEntry3;
    [SerializeField] Transform Entry1;
    [SerializeField] Transform Entry2;
    [SerializeField] Transform Entry3;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (Entry1 != null)
        {
            if (Indestructable.instance.prevScene == fromEntry1)
            {
                playerTransform.position = Entry1.position;
            }
        }
        

        if (Entry2 != null)
        {
            if (Indestructable.instance.prevScene == fromEntry2)
            {
                playerTransform.position = Entry2.position;
            }
        }

        if (Entry3 != null)
        {
            if (Indestructable.instance.prevScene == fromEntry3)
            {
                playerTransform.position = Entry3.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
