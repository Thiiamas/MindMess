using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    public int sceneIndex;

    public override void OnInteraction()
    {
        //si dans 00_1
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0 && !Indestructable.instance.femmeTrigger)
        {
            return;
        }
        Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        loader.LoadNextLevel(sceneIndex);
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
