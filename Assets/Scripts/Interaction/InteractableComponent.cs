using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] GameObject interactionHelper;
    [SerializeField] int uiOffset = 2;

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
        spawnedHelper = Instantiate(interactionHelper, GetSpawnPosition(), Quaternion.identity, transform);
    }

    public virtual void HideUI()
    {
        Destroy(spawnedHelper);
        spawnedHelper = null;
    }

    protected Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += uiOffset;
        return spawnPosition;
    }
}
