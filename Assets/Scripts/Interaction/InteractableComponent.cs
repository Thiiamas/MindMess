using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableComponent : MonoBehaviour
{
    [SerializeField] GameObject interactionHelper;
    [SerializeField] int uiOffset = 2;

    private GameObject spawnedHelper;
    public abstract void OnInteraction();

    public virtual bool CanInteract()
    {
        return true;
    }

    public void DisplayHelper()
    {
        if(spawnedHelper != null)
        {
            HideUI();
        }
        spawnedHelper = Instantiate(interactionHelper, GetUISpawnPosition(), Quaternion.identity);
    }

    public virtual void HideUI()
    {
        Destroy(spawnedHelper);
        spawnedHelper = null;
    }

    protected Vector3 GetUISpawnPosition()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += uiOffset;
        return spawnPosition;
    }
}
