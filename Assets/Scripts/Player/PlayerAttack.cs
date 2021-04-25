using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack: MonoBehaviour
{
    CharacterController2D characterController;
    PlayerController playerController;
    PlayerMovement playerMovement;
    public bool isAttacking = false;


    [Header("Stats")]
    [SerializeField] float attackSpeedMultiplier = 1f;
    [SerializeField] LayerMask enemyLayer;


    [Header("Attack")]
    [SerializeField] Collider2D attackCollider;
    [SerializeField] float attackDamage;



    #region getters

    public bool IsAttacking { get { return isAttacking; } }

    #endregion
    


    private void Start()
    {
        characterController = this.GetComponent<CharacterController2D>();
        playerController = this.GetComponent<PlayerController>();
        playerMovement = this.GetComponent<PlayerMovement>();
    }
    

    // Update is called once per frame
    void Update()
    {
    }


    public void AttackInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            StartCoroutine(playerController.InputBuffer(() => playerController.CanAttack(), AttackHammer));
        }
    }


    void AttackHammer()
	{
        isAttacking = true;
        List<Collider2D> hitten = GetCollidersInCollider(attackCollider, enemyLayer);

        foreach (Collider2D hit in hitten)
        {
            /* do something */
        }
    }


    List<Collider2D> GetCollidersInCollider(Collider2D collider, LayerMask layer)
    {
        List<Collider2D> hits = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layer);
        Physics2D.OverlapCollider(collider, filter, hits);
        return hits;
    }


    public void FinishAttack()
    {
        isAttacking = false;
    }


}
