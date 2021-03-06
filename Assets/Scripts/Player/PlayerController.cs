using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    CharacterController2D characterController;
    PlayerAttack playerAttack;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    bool isDead = false;
    
    [Header("Game Object")]
    [SerializeField] public GameObject GFX;
    [SerializeField] BarController healthBar;


    [Header("Invincibility")]
    [SerializeField] float invincibilityTime = 1.5f;
    [SerializeField] float invincibilityDeltaTime = 0.15f;
    bool isInvincible = false;


    [Header("Slow motion")]
    [SerializeField] float slowMotionFactor = 0.01f;
    [SerializeField] float slowMotionTime = 1f;


    [Header("Camera Shake")]
    [SerializeField] float shakeTime = 1f;
    [SerializeField] float shakeIntensity = 1f;


    [Header("Buffer")]
    [SerializeField] float bufferTime = .2f;
    Timer bufferTimer;

    private GameObject currentInteractionObject;

    #region getters

    public bool IsDead { get { return isDead; } }

    #endregion

    
    void Start()
    {
        characterController = this.GetComponent<CharacterController2D>();
        playerAttack = this.GetComponent<PlayerAttack>();
        playerMovement = this.GetComponent<PlayerMovement>();
        spriteRenderer = GFX.GetComponent<SpriteRenderer>();

        bufferTimer = new Timer(bufferTime);
    }

    private void Update()
    {
        
    }

    #region Inputs

    public void ActionInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if(!interactWithObject())
            {
                MovingplatformsManager platformManager = MovingplatformsManager.Instance;
                if (platformManager != null)
                {
                    platformManager.DropItem();
                }
            }
        }
    }

    #endregion Inputs

    bool interactWithObject()
    {
        if (currentInteractionObject == null)
        {
            Debug.Log("Nothing to interact with");
        }
        else
        {
            InteractableComponent interactableComponent = currentInteractionObject.GetComponent<InteractableComponent>();
            if(interactableComponent.CanInteract())
            {
                interactableComponent.OnInteraction();
                return true;
            }
        }
        return false;
    }

    #region damage

    public void Die()
    {
        Indestructable.instance.playerHealth = Indestructable.instance.maxHealth;
        isDead = true;
        playerAttack.enabled = false;
        playerMovement.enabled = false;
        this.enabled = false;
        SceneManager.LoadScene(Indestructable.instance.restartScene);
    }

    public void TakeDamage(Transform damageDealer, float damage, bool ignoreInvicible = false)
    {
        if(isInvincible && !ignoreInvicible) { 
            return;
        }

        Indestructable.instance.playerHealth -= damage;
        healthBar.SetValue(Indestructable.instance.playerHealth / Indestructable.instance.maxHealth);

        // hurt prefab
        Instantiate(Indestructable.instance.HurtEffectPrefab, transform.position, Quaternion.identity);

        /*// knockback
        Vector3 direction = (transform.position - damageDealer.position).normalized;
        StartCoroutine(playerMovement.KnockBack(direction));*/
        
        StartCoroutine(DamageCoroutine());
    }

    public void TakeDamageWithoutKB(Transform damageDealer, float damage, bool ignoreInvicible = false)
    {
        if (isInvincible && !ignoreInvicible)
        {
            return;
        }

        Indestructable.instance.playerHealth -= damage;
        healthBar.SetValue(Indestructable.instance.playerHealth / Indestructable.instance.maxHealth);

        // hurt prefab
        Instantiate(Indestructable.instance.HurtEffectPrefab, transform.position, Quaternion.identity);

        StartCoroutine(DamageCoroutine());
    }

    public void TakeDamageLitleKB(Transform damageDealer, float damage, bool ignoreInvicible = false)
    {
        if (isInvincible && !ignoreInvicible)
        {
            return;
        }

        Indestructable.instance.playerHealth -= damage;
        healthBar.SetValue(Indestructable.instance.playerHealth / Indestructable.instance.maxHealth);

        // hurt prefab
        Instantiate(Indestructable.instance.HurtEffectPrefab, transform.position, Quaternion.identity);

        // knockback
        Vector3 direction = (transform.position - damageDealer.position).normalized;
        StartCoroutine(playerMovement.KnockBackLittle(direction));

        StartCoroutine(DamageCoroutine());
    }

    public IEnumerator DamageCoroutine()
    {
        spriteRenderer.material = Indestructable.instance.WhiteMaterial;
        if(Indestructable.instance.playerHealth <= 0)
        {
            TimeManager.SlowMotion(slowMotionFactor);

            yield return new WaitForSecondsRealtime(slowMotionTime);
            TimeManager.RestoreTime();
        }

        spriteRenderer.material = Indestructable.instance.DefaultMaterial;

        // shake camera
        if (VirtualCameraManager.Instance)
        {
            VirtualCameraManager.Instance.ShakeCamera(shakeIntensity, shakeTime);
        }

        if (Indestructable.instance.playerHealth <= 0) {
            Die();
        } else {
            StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;

        for (float i = 0; i < invincibilityTime; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (GFX.transform.localScale == Vector3.one) {
                GFX.transform.localScale = Vector3.zero;
            } 
            else {
                GFX.transform.localScale = Vector3.one;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        GFX.transform.localScale = Vector3.one;
        isInvincible = false;
    }

    #endregion


    public IEnumerator InputBuffer ( Func<bool> conditionFunction, Action actionFunction ) 
    {
        // stop every other instance of input buffer (not the current one)
        StopCoroutine( "InputBuffer" );

        bufferTimer.Start();
        while ( bufferTimer.IsOn ) 
        {
            bufferTimer.Decrease();
            bool condition = conditionFunction();
            if ( condition ) {
                bufferTimer.Stop();
                actionFunction?.Invoke();
                yield break;
            } else {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public bool CanJump()
    {
        MovingplatformsManager platformManager = MovingplatformsManager.Instance;
        if (platformManager != null)
        {
            if (platformManager.CarryIronDoll())
                return false;
        }

        return (playerMovement.IsGrounded || playerMovement.IsCoyoteTimerOn) && !playerMovement.IsJumping && !playerAttack.IsAttacking;
    }

    public bool CanAttack()
    {
        return !playerAttack.IsAttacking;
    }

    public RaycastHit2D CheckFantome(LayerMask layerMask)
    {
        RaycastHit2D hit;
        Vector2 below = new Vector2(0, -1);
        hit = Physics2D.Raycast(transform.position, below, 0.9f, layerMask);
        return hit;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            if (currentInteractionObject != null)
            {
                currentInteractionObject.GetComponent<InteractableComponent>().HideUI();
            }
            currentInteractionObject = col.gameObject;
            currentInteractionObject.GetComponent<InteractableComponent>().DisplayHelper();
        }
        if (col.gameObject.tag == "Toupie")
        {
            Toupie toupie = col.gameObject.GetComponent<Toupie>();
            TakeDamage(col.transform, toupie.damage);
        }
        if (col.gameObject.tag == "Dog")
        {
            Dog dog = col.gameObject.GetComponent<Dog>();
            TakeDamage(col.transform, dog.Damage);
        }
        if (col.gameObject.tag == "Enemy" )
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            TakeDamage(col.transform, enemy.Damage);
        } 
        if(col.gameObject.tag == "Fall")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            TakeDamage(col.transform, enemy.Damage, true);
        }
        if(col.gameObject.tag == "Baby")
        {
            Baby baby = col.gameObject.GetComponent<Baby>();
            TakeDamageWithoutKB(col.transform, baby.Damage);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            if(currentInteractionObject != null)
            {
                currentInteractionObject.GetComponent<InteractableComponent>().HideUI();
            }
            currentInteractionObject = null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
