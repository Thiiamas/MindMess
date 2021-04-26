using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wine : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
    public override void OnInteraction()
    {
        Indestructable.instance.PlayStrangeSound();
        //then loadnext
        Indestructable.instance.restartScene = sceneIndex;
        loader.LoadNextLevel(sceneIndex);
        
        if(sceneIndex == 2){
            Indestructable.instance.PlayMadMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
