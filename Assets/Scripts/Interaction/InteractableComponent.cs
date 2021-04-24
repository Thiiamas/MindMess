using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] GameObject interactionHelper;
    [SerializeField] int helperOffset = 2;
    public string Text = "interactable Text";

    private GameObject spawnedHelper;
    public virtual void OnInteraction()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayHelper()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += helperOffset; 
        spawnedHelper = Instantiate(interactionHelper, spawnPosition, Quaternion.identity, transform);
    }

    public void HideHelper()
    {
        Destroy(spawnedHelper);
    }
}
