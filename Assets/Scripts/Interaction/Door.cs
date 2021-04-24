using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
    public override void OnInteraction()
    {
        loader.LoadNextLevel(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
