using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : InteractableComponent
{
    bool isOpen = false;
    public override void OnInteraction()
    {
        Text = "Press A to";

        isOpen = !isOpen;

        Text += isOpen ? "to close" : "to open";
        Debug.Log("This is a desk wow");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
