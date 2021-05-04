using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeK47 : MonoBehaviour
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
    [SerializeField] Transform SpawnPoint;
    public float spread=20;
    public float bSpeed = 15;
    Vector2 DebugDirection;
    Transform playerTransform;
    public GameObject baby;
    Animator animator;

    public float launchDelay;
    float lastLaunched = 0;

    // Start is called before the first frame update
    void Start() { 
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastLaunched + launchDelay)
        {
            lastLaunched = Time.time;
            animator.SetTrigger("Throw");
        }
    }

    public void LaunchBaby()
    {
        GameObject instanciated = Instantiate(baby, SpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = instanciated.GetComponent<Rigidbody2D>();

        Vector2 direction = (playerTransform.position - SpawnPoint.position).normalized;
        direction += (Random.insideUnitCircle / spread);
        DebugDirection = direction;
        /*direction.x += Rand.NextGaussianDouble()/6;
        direction.y += Rand.NextGaussianDouble()/3;
        direction = direction.normalized;*/
        rb.velocity = direction * bSpeed;
    }

    public void OnHit()
    {
        if (sceneIndex == 16)
            Indestructable.instance.PlayHappyMusic();
        Indestructable.instance.PlayStrangeSound();
        Indestructable.instance.restartScene = sceneIndex;
        loader.LoadNextLevel(sceneIndex);
    }
}
