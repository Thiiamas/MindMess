using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeK47 : MonoBehaviour
{
    [SerializeField] LevelLoader loader;
    [SerializeField] int sceneIndex;
    [SerializeField] Transform SpawnPoint;

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

        Vector2 direction = (playerTransform.position - transform.position);
        direction.x += Rand.NextGaussianDouble()/6;
        direction.y += Rand.NextGaussianDouble();
        direction = direction.normalized;
        rb.velocity = direction * 4000 * Time.deltaTime;
    }

    public void OnHit()
    {
        Indestructable.instance.PlayStrangeSound();
        Indestructable.instance.restartScene = sceneIndex;
        loader.LoadNextLevel(sceneIndex);
    }
}
