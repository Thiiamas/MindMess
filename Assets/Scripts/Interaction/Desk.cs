using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : InteractableComponent
{
    public override void OnInteraction()
    {
        Debug.Log("This is a desk wow");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
