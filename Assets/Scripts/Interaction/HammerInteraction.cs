using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerInteraction : InteractableComponent
{
    public override void OnInteraction()
    {
        Indestructable.instance.hasHammer = true;
        Destroy(gameObject);
    }
}
