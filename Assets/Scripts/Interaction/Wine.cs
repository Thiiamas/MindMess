using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wine : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
    public override void OnInteraction()
    {
        //show end of act 01

        //then loadnext
        loader.LoadNextLevel(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
