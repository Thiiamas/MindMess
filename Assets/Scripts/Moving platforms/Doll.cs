using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doll : InteractableComponent
{

    private Vector3 velocity = Vector3.zero;
    private CharacterController2D characterController;

    public override void OnInteraction()
    {
        GameObject player = GameObject.Find("NewPlayer");
        Transform itemHUD = player.transform.Find("PlayerCanvas/Item");
        Image image = itemHUD.GetComponent<Image>();

        GameManager.Instance.Item = gameObject;
        image.enabled = true;
        Destroy(gameObject);
    }

    private void Start()
    {
        characterController = this.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        //characterController.move(velocity * Time.deltaTime);
    }
}
