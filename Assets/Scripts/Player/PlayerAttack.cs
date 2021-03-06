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
    [SerializeField] LayerMask enemyLayer;


    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackDamage;
    [SerializeField] float attackRadius;

    [SerializeField] AudioSource audioHammer;
    [SerializeField] AudioSource audioWallBreak;



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
        if(Indestructable.instance.hasHammer && context.performed)
        {
            StartCoroutine(playerController.InputBuffer(() => playerController.CanAttack(), AttackHammer));
        }
    }


    void AttackHammer()
	{
        if(audioHammer != null)
        {
            audioHammer.Play();
        }
        isAttacking = true;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Breakable")
            {
                if (audioWallBreak != null)
                {
                    audioWallBreak.Play();
                }
                Destroy(hitColliders[i].gameObject);
            }
            else if (hitColliders[i].gameObject.tag == "Dog")
            {
                Dog dog = hitColliders[i].gameObject.GetComponent<Dog>();
                dog.TakeDamage(1);
                return;
            }
            else if (hitColliders[i].gameObject.tag == "Baby")
            {
                Destroy(hitColliders[i].gameObject);
            }
            else if (hitColliders[i].gameObject.tag == "Wife")
            {
                hitColliders[i].GetComponent<WifeK47>().OnHit();
            }else if (hitColliders[i].gameObject.tag == "Toupie")
            {
                hitColliders[i].GetComponent<Toupie>().Hurt();
            }
        }

        /*
        List<Collider2D> hits = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layer);
        Physics2D.OverlapCollider(collider, filter, hits);

        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.gameObject.name);
        }
        attackCollider.gameObject.SetActive(false);
        */
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


    public void FinishAttack()
    {
        isAttacking = false;
    }


}
