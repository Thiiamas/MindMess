using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
    [SerializeField] Dog dog;

    private bool isOpen;
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public override void OnInteraction()
    {
        if(isOpen)
        {
            if (dog && dog.isDead)
            {
                //si chien mort, c'est pr forcer la bonne tp
                Indestructable.instance.prevScene = 200;
            }
            else
            {
                Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            }
            loader.LoadNextLevel(sceneIndex);
        } 
        else 
        {
            animator.SetBool("isOpen", true);
            isOpen = true;
            AudioSource audio = GetComponent<AudioSource>();
            if(audio != null){
                audio.Play();
            }
        }
    }
}
