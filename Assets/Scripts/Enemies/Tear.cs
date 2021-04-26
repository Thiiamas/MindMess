using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private CharacterController2D characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController2D>();
    }
    private void Update()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        characterController.move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.layer == 6 || collision.transform.tag == "Enemy"){
            Destroy(gameObject);
        }
    }
}
