using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Trappe trappe;
    public int indexAfterDeath;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        isDead = true;
        //trappe.sceneIndex = indexAfterDeath;
    }
}
