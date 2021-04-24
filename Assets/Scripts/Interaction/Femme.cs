using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Femme : InteractableComponent
{

    public override void OnInteraction()
    {
        Debug.Log("OU EST LE 20");
        Indestructable.instance.femmeTrigger = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
