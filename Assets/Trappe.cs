using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trappe : InteractableComponent
{
    //[SerializeField] Indestructable indestructable;
    [SerializeField] LevelLoader loader;
    [SerializeField] Dog dog;
    public int sceneIndex;
    public override void OnInteraction()
    {
        if (dog && dog.isDead)
        {
            //si chien mort, c'est pr forcer la bonne tp
            Indestructable.instance.prevScene = 200;
            loader.LoadNextLevel(sceneIndex);
        }
        else
        {
            Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        }
        loader.LoadNextLevel(sceneIndex);
    }
}
