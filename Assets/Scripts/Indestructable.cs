using UnityEngine;
using System.Collections;

public class Indestructable : MonoBehaviour
{
    [Header("Particle Effect")]
    [SerializeField] public GameObject HurtEffectPrefab;

    [Header("Materials")]
    [SerializeField] public Material WhiteMaterial;
    [SerializeField] public Material DefaultMaterial;

    public static Indestructable instance = null;

    public int prevScene = 6;

    public int restartScene = 0;

    public bool hasHammer = false;

    //Intro
    public bool femmeTrigger = false;

    //Act 02
    public bool dogDead = false;
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
        DontDestroyOnLoad(this.gameObject);
    }
}