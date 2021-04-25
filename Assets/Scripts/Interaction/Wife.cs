using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : InteractableComponent
{

    public override void OnInteraction()
    {
        Debug.Log("OU EST LE 20");
        Indestructable.instance.femmeTrigger = true;
    }


}
