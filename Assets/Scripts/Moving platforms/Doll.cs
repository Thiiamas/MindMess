using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doll : InteractableComponent
{
    [SerializeField] public bool IsIronDoll;
    private Vector3 velocity = Vector3.zero;
    private CharacterController2D characterController;

    public override void OnInteraction()
    {
        MovingplatformsManager.Instance.SetItem(IsIronDoll);
        Destroy(gameObject);
    }

    public override bool CanInteract()
    {
        return MovingplatformsManager.Instance.Item == null;
    }

    private void Start()
    {
        characterController = this.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if(!characterController.isGrounded)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
        characterController.move(velocity * Time.deltaTime);
	}
}
