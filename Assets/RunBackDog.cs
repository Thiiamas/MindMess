using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBackDog : StateMachineBehaviour
{
    Dog dog;
    Transform playerTransform;
    Rigidbody2D rb;
    Collider2D boxColl;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dog = animator.GetComponent<Dog>();
        playerTransform = dog.playerTransform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 direction;
        if (playerTransform.position.x - dog.transform.position.x < 0)
        {
            //joueur a hauche, va a droite
            direction = new Vector2(1, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 2f)
        {
            rb.velocity = direction * 200f * Time.deltaTime;
        } else
        {
            animator.SetTrigger("EndRunBack");
        }
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
