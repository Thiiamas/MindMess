using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    const string STATE_IDLE = "player_idle";
    const string STATE_WALK = "player_walk";

    const string STATE_JUMP = "player_jump";

    //const string STATE_DEATH = "Player_Die";
    const string STATE_ATTACK = "player_attack";


    [SerializeField] Transform player;
    Animator animator;
    PlayerController playerController;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    string currentState = "";
    Vector2 speed = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttack = player.GetComponent<PlayerAttack>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed.x = Mathf.Abs(playerMovement.Velocity.x);
        speed.y = playerMovement.Velocity.y;


        if(playerAttack.IsAttacking)
        {
            ChangeState(STATE_ATTACK);
        }

        else if( !playerMovement.IsGrounded && speed.y > 0)
        {
            ChangeState(STATE_JUMP);
        }   

        else if( playerMovement.IsGrounded && speed.x < 0.1)
        {
            ChangeState(STATE_IDLE);
        }   

        else if( playerMovement.IsGrounded && speed.x >= 0.1)
        {
            ChangeState(STATE_WALK);
        }

    }

    void ChangeState(string newState)
    {
        if(currentState == newState){
            return;
        }
        currentState = newState;
        animator.Play(newState);
    }
    
    public void FinishAttackAnimation()
    {
        playerAttack.FinishAttack();
    }

    public void FinishDeathAnimation()
    {
        playerController.Die();
    }


}
