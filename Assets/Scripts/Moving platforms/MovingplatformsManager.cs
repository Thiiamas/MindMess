using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingplatformsManager : MonoBehaviour
{
    private static MovingplatformsManager instance = null;
    
    public static MovingplatformsManager Instance { get { return instance; } }

    private int pressedPlatesCount = 0;
    private GameObject[] movingPlatforms;
    public GameObject Item { get; set; }

    void Awake()
    {
        // If we don't have an instance set - set it now
        if (!instance)
            instance = this;
        // Otherwise, its a double, we dont need it - destroy
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        movingPlatforms = GameObject.FindGameObjectsWithTag("MovingPlatform");
    }

    public void IncreasePressedPlateCount()
    {
        pressedPlatesCount++;
        UpdatePlatforms();
    }

    public void DecreasePressedPlateCount()
    {
        pressedPlatesCount--;
        UpdatePlatforms();
    }

    private void UpdatePlatforms()
    {
        foreach (GameObject platform in movingPlatforms)
        {
            platform.GetComponent<MovingPlatformComponent>().OnPressedPlateCountChanged(pressedPlatesCount);
        }
    }

}
