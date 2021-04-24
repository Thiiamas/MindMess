using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    public override void OnInteraction()
    {
        loader.LoadNextLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
