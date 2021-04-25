using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableComponent : MonoBehaviour
{
    [SerializeField] GameObject interactionHelper;
    [SerializeField] int uiOffset = 2;

    private GameObject spawnedHelper;
    public abstract void OnInteraction();

    public void DisplayHelper()
    {
        if(spawnedHelper != null)
        {
            Destroy(spawnedHelper);
            spawnedHelper = null;
        }
        spawnedHelper = Instantiate(interactionHelper, GetUISpawnPosition(), Quaternion.identity, transform);
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
