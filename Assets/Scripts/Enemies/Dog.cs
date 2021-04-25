using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Trappe trappe;
    public int indexAfterDeath;
    public bool isDead = false;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        isDead = true;
        Indestructable.instance.dogDead = true;
        if (trappe)
        {
            trappe.gameObject.SetActive(true);
        }
    }
}
