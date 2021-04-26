using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Door door;
    public bool isDead = false;
    [NonSerialized] public Transform playerTransform;
    bool isFacingRight = true;
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] Collider2D hitboxCollider;

    public int health = 3;
    public float Damage = 250f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            FlipTowardPlayer();
        }
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
    }
    protected void FlipTowardPlayer()
    {
        bool playerIsRight = false;
        if (playerTransform.position.x - transform.position.x > 0)
        {
            playerIsRight = true;
        }
        if ((isFacingRight && !playerIsRight) || (!isFacingRight && playerIsRight))
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    void Die()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        hitboxCollider.enabled = false;
        Indestructable.instance.dogDead = true;
        if (door)
        {
            door.gameObject.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        health -= damage;
        if (health <= 0){
            Die();
        }
        Vector2 direction;
        if (playerTransform.position.x - transform.position.x < 0)
        {
            //joueur a gauche, va a droite
            direction = new Vector2(1, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
        }
        rb.AddForce(direction * 5000 * Time.fixedDeltaTime);
    }
}
