using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDog : StateMachineBehaviour
{
    Dog dog;
    Transform playerTransform;
    Transform dogTransform;
    Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dog = animator.gameObject.GetComponent<Dog>();
        playerTransform = dog.playerTransform;
        dogTransform = dog.transform;
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(playerTransform.position.x - dogTransform.position.x, playerTransform.position.y - dogTransform.position.y).normalized;
        Vector2 force = direction * 400f;
        force.y += 175f;
        rb.AddForce(force * 50f * Time.fixedDeltaTime);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
