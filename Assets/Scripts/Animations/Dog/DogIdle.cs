using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIdle : StateMachineBehaviour
{
    float delay = 2.0f;
    Dog dog;
    Transform playerTransform;
    Rigidbody2D rb;
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
        float distanceX = Mathf.Abs(playerTransform.position.x - dog.transform.position.x);
        Vector2 direction;
        if (distanceX < 5.0f && distanceX > 3f)
        {
            //recule
            if (playerTransform.position.x - dog.transform.position.x < 0)
            {
                //joueur a hauche, va a droite
                direction = new Vector2(1,0);
            } else
            {
                direction = new Vector2(-1, 0);
            }
            //rb.AddForce(direction * 50 * Time.fixedDeltaTime);
            rb.velocity = direction * 100 * Time.fixedDeltaTime;
        } else if (distanceX < 3)
        {
            animator.SetTrigger("Attack");
        }

        /*if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= delay)
        {
            delay += delay;
            float gauss = Rand.NextGaussianDouble();
            if (gauss < 1)
            {
                Debug.Log("in");
                animator.SetTrigger("Attack");
            }
        }*/
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
