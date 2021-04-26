using UnityEngine;
using System.Collections;

public class Indestructable : MonoBehaviour
{
    [Header("Particle Effect")]
    [SerializeField] public GameObject HurtEffectPrefab;

    [Header("Materials")]
    [SerializeField] public Material WhiteMaterial;
    [SerializeField] public Material DefaultMaterial;
    [SerializeField] private AudioSource strangeSound;

    [SerializeField] private AudioSource happyMusic;
    [SerializeField] private AudioSource madMusic;
    [SerializeField] private AudioSource bossMusic;    

    public static Indestructable instance = null;

    public int prevScene = 6;

    public int restartScene;

    public bool hasHammer = false;

    [Header("Stats")]
    [SerializeField] public float maxHealth = 500;
    public float playerHealth;

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
        playerHealth = maxHealth;
        restartScene = 0;
    }

    private void Start()
    {
        if (happyMusic)
            happyMusic.Play();
    }

    public void PlayStrangeSound(){
        if (strangeSound)
        {
            strangeSound.Play();
        }
    }

    public void PlayMadMusic(){
        if (madMusic)
        {
            happyMusic.Stop();
            madMusic.Play();
        }
    }

    public void PlayBossMusic(){
        if (madMusic)
        {
            madMusic.Stop();
            bossMusic.Play();
        }
    }

    public void PlayHappyMusic(){
        if (madMusic)
        {
            bossMusic.Stop();
            happyMusic.Play();
        }
    }
}