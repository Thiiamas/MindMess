using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableComponent
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
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
            loader.LoadNextLevel(sceneIndex);
        } 
        else 
        {
            animator.SetBool("isOpen", true);
            isOpen = true;
        }
    }

}
