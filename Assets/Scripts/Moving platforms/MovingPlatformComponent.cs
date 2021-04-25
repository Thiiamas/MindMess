using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformComponent : MonoBehaviour
{
    [SerializeField] private bool clockwiseRotation;
    [SerializeField] private int RequiredPressedPlateCount;

    private bool isRotated;
    public void OnPressedPlateCountChanged(int newCount)
    {
        if (!isRotated && newCount >= RequiredPressedPlateCount)
        {
            isRotated = true;
            if(clockwiseRotation)
            {
                transform.Rotate(new Vector3(0, 0, -90));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        }
        else if (isRotated && newCount < RequiredPressedPlateCount)
        {
            isRotated = false;
            if (clockwiseRotation)
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
}
