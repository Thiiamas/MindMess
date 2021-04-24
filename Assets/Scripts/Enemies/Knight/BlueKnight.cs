using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKnight : Enemy
{

    [Header("Stats")]
    [SerializeField] const float MAX_ENDURANCE = 3f;
    [SerializeField] float decisionSpeed = 5f;
    float lastDecisionTime = 0f;

    [Header("Movement")]
    [SerializeField] float walkAcceleration = 2f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float gravityModifier = 2;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 50;
    [SerializeField] float dashTime = .1f;
    [SerializeField] float dashCooldown = 1f;

    [Header("Attack")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject DiveAttackPrefab;
    [SerializeField] Transform DiveAttackPoint;
    [SerializeField] float diveAttackDamage = 10f;
    [SerializeField] GameObject basicAttackprefab;
    [SerializeField]  public float basicAttackRange = 1.0f;
    float basicAttackDamage = 10.0f;
    [SerializeField] GameObject FlurryAttackPrefab;
    public float flurryCooldown = 4f;
    float lastTimeFlurry;

    bool sunPhase;
    [SerializeField] BlueKnightRoom room;
    bool dashHasReset = false;
    bool dashHasCooldown = true;
    Timer dashTimer;

    bool isAgro = true;
    float agroRange = 10.0f;
    Vector2 playerDirection;
    public bool isJumping;
    public bool isEnraged = false;
    public bool isDashing;

    PolygonCollider2D polyCollider;

    
    

    public bool IsKnockBacked { get { return isKnockbacked; } }
    public bool IsDashing { get { return isDashing; } }
    public bool IsTired { get { return isTired; } }
    public bool SunPhase { get { return sunPhase; } set { sunPhase = value; } }
    public Animator Animator { get { return animator; } }

    public Transform PlayerTransform { get { return playerTransform; } }
    protected override void Setup()
    {
        animator = GetComponentInChildren<Animator>();
        dashTimer = new Timer(dashTime);
        lastTimeFlurry = Time.time;
        currentEndurance = MAX_ENDURANCE;
        polyCollider = GetComponent<PolygonCollider2D>();
        enduranceRechargeMultiplicator = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentEndurance < 0 && !isTired)
        {
            isTired = true;
            polyCollider.enabled = false;
            animator.SetTrigger("tired");
            animator.SetBool("isTired", true);
        }

        if (isTired)
        {
            currentEndurance += Time.deltaTime * enduranceRechargeMultiplicator / 1;
            if (currentEndurance >= MAX_ENDURANCE)
            {
                isTired = false;
                polyCollider.enabled = true;
                animator.SetTrigger("unTired");
                animator.SetBool("isTired", false);
            }

        }else if (currentEndurance < MAX_ENDURANCE)
        {
            currentEndurance += Time.deltaTime * enduranceRechargeMultiplicator;
        }

        animator.SetBool("sunPhase", sunPhase);

        //Movements
        if (IsGrounded)
        {
            velocity.y = 0;
        }
        if (isAgro)
        {
            FlipTowardPlayer();
        }
        animator.SetFloat("velocityX", velocity.x);
        animator.SetFloat("velocityY", characterController.velocity.y);
        animator.SetBool("isAerial", !IsGrounded);
        bool isStill;
        if (velocity.x == 0)
        {
            isStill = true;
        } else
        {
            isStill = false;
        }
        animator.SetBool("isStill", isStill);
    }

    override public void TakeDamage(Transform damageDealer, float damage)
    {
        
        animator.SetTrigger("hurt");
        isDashing = false;
        base.TakeDamage(damageDealer, damage);

        if (sunPhase)
        {
            room.HurtWhileSun();
        }
        
        if (!isTired)
        {
            currentEndurance -= 1f;
        }
    }

    public void ApplyGravity()
    {
        if (isKnockbacked)
        {
            velocity.y += (Physics2D.gravity.y / gravityModifier) * fallMultiplier * Time.deltaTime;
            return;
        }

        if (velocity.y < 0)
        {
            velocity.y += Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
    }

    public void ApplyCustomGravity(float modifier)
    {
        /*if (isKnockbacked)
        {
            velocity.y += (Physics2D.gravity.y / gravityModifier) * fallMultiplier * Time.deltaTime;
            return;
        }*/

        if (velocity.y < 0)
        {
            velocity.y += Physics2D.gravity.y * modifier * Time.deltaTime;
        }
        else
        {
            velocity.y += Physics2D.gravity.y * modifier * Time.deltaTime;
        }
    }
    #region Attacks


    public void basicAttack()
    {
        List<Collider2D> hitten = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(playerLayer);
        filter.useTriggers = true;
        GameObject basicAttackObject = Instantiate(basicAttackprefab, transform);
        PolygonCollider2D attackCollider = basicAttackObject.GetComponent<PolygonCollider2D>();
        Physics2D.OverlapCollider(attackCollider, filter, hitten);

        foreach (Collider2D hit in hitten)
        {
            if (hit.gameObject.tag == "Player")
            {
                hit.GetComponent<PlayerController>().TakeDamage(this.transform, basicAttackDamage);
            }
        }
    }

    public void DiveAttack()
    {
        List<Collider2D> hitten = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(playerLayer);
        filter.useTriggers = true;
        GameObject diveAttackObject = Instantiate(DiveAttackPrefab, transform);
        PolygonCollider2D attackCollider = diveAttackObject.GetComponent<PolygonCollider2D>();
        Physics2D.OverlapCollider(attackCollider, filter, hitten);

        foreach (Collider2D hit in hitten)
        {
            if (hit.gameObject.tag == "Player")
            {
                hit.GetComponent<PlayerController>().TakeDamage(this.transform, diveAttackDamage);
            }
        }
    }

    public void Flurry()
    {
        GameObject attack = Instantiate(FlurryAttackPrefab, transform.position, transform.rotation);
        Vector3 direction = (playerTransform.position - attack.transform.position);
        //attack.transform.rotation = Quaternion.LookRotation(Vector3.forward, -direction);
        direction.y += Rand.NextGaussianDouble()/0.8f;
        direction = direction.normalized;
        attack.GetComponent<Flurry>().Launch(direction);
    }

    //need FiringSun in the room
    

    #endregion Attacks

    #region Movements

    public void StartDash()
    {
        isDashing = true;
        dashHasReset = false;
        Vector2 dSpeed;
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;

        dSpeed = playerDirection * dashSpeed;

        StartCoroutine(Dash(dSpeed));
    }

    public void StartDashTarget(Vector3 target)
    {
        isDashing = true;
        dashHasReset = false;
        Vector2 dSpeed;
        Vector3 direction = (target - transform.position).normalized;

        dSpeed = playerDirection * dashSpeed;

        StartCoroutine(Dash(dSpeed));
    }

    public IEnumerator Dash(Vector2 dSpeed)
    {
        dashTimer.Start();
        while (dashTimer.IsOn)
        {
            // velocity.y = 0;
            velocity.y = dSpeed.y;
            velocity.x = dSpeed.x;
            dashTimer.Decrease();
            if (!isDashing)
            {
                dashTimer.Stop();
            }
            yield return new WaitForEndOfFrame();
        }
        isDashing = false;
        velocity = Vector3.zero;
        dashHasCooldown = false;

        // Cooldown
        yield return new WaitForSeconds(dashCooldown);
        dashHasCooldown = true;
    }

    public IEnumerator Jump(Vector3 direction, Vector2 force)
    {
        isJumping = true;
        float jumpDeceleration = 10f;
        velocity = direction * force;
        characterController.move(velocity * Time.deltaTime);

        while (velocity.x > .1f || velocity.x < -.1f)
        {
            if ((playerTransform.position - transform.position).y > -0.1f)
            {
                Debug.Log("exited in jump");
                animator.Play("Idle");
                yield break;
            }
            velocity.x = Mathf.MoveTowards(velocity.x, 0, jumpDeceleration * Time.deltaTime);
            characterController.move(velocity * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isJumping = false;
    }

    public void StartDive(Vector3 target)
    {
        isDashing = true;
        Vector2 dSpeed;
        //assert that he woon't "dive" towards the sky
        if ((target - transform.position).normalized.y > -0.1f)
        {
            animator.Play("Idle");
            return;
        }
        dSpeed = (target - transform.position).normalized * dashSpeed;
        StartCoroutine(Dive(dSpeed));
    }

    public IEnumerator Dive(Vector2 dSpeed)
    {
        dashTimer.Start();
        while (!IsGrounded)
        {
            // velocity.y = 0;
            velocity.y = dSpeed.y;
            velocity.x = dSpeed.x;
            yield return new WaitForEndOfFrame();
        }
        isDashing = false;
        velocity = Vector3.zero;
        dashHasCooldown = false;

        // Cooldown
        yield return new WaitForSeconds(dashCooldown);
        dashHasCooldown = true;
    }

    public void RunTowardsPlayer(bool fast)
    {
        playerDirection = (playerTransform.position - transform.position).normalized;
        Debug.Log(playerDirection);
        if (!fast)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, playerDirection.x * speed, walkAcceleration * Time.deltaTime);
        } else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, playerDirection.x * speed * 2, walkAcceleration * Time.deltaTime);
        }
        characterController.move(velocity * Time.deltaTime);
    }

    public void Stick(Vector3 position)
    {
        transform.position = position;
    }

    #endregion Movements

    //Very basic implementation (au moins ça marche)
    //return string pour peut etre plus tard
    public string TakeDecision()
    {
        
        string decision = "";
        if (Time.time >= lastDecisionTime + decisionSpeed)
        {
            float rand = Random.Range(0f, 2f);
            if (!IsGrounded)
            {
                if (rand < 1.9f && Time.time > lastTimeFlurry + flurryCooldown)
                {
                    lastTimeFlurry = Time.time;
                    animator.Play("Flurry");
                    decision = "Flurry";
                }
                return decision;
            }
            if (rand < 1.5f)
            {
                if (rand < 0.75f)
                {
                    //dive attack
                    animator.Play("Jump");
                    decision = "Dive";
                } else
                {
                    animator.Play("RunHit");
                    decision = "runhit";
                }
                
            }else if (Time.time > lastTimeFlurry + flurryCooldown)
            {
                lastTimeFlurry = Time.time;
                animator.Play("JumpFlurry");

                decision = "JumpFlurry";
            }
            lastDecisionTime = Time.time;
        }
        //Debug.Log("Decision :" + decision);
        return decision;
    }
}
