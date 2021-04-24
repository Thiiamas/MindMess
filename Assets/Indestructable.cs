using UnityEngine;
using System.Collections;

public class Indestructable : MonoBehaviour
{

    public static Indestructable instance = null;

    public int prevScene = 6;

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
        Debug.Log("ici");
        DontDestroyOnLoad(this.gameObject);
    }
}