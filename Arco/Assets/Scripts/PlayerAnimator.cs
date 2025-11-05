using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;
    public PlayerMovement playerMovement;
    public bool hasJumped;
    public bool isFalling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        hasJumped = false;
        isFalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        // walk animation
        if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && Input.GetKey(KeyCode.LeftShift) == false && hasJumped == false)
        {
            anim.SetInteger("AnimationPar", 1);
            anim.SetInteger("RunPar", 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (controller.velocity.y < -5 && hasJumped == false)
            {
                Falling();
            }
        }
        
        // run animation
        else if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && Input.GetKey(KeyCode.LeftShift) && hasJumped == false)
        {
            anim.SetInteger("RunPar", 1);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (controller.velocity.y < -5 && hasJumped == false)
            {
                Falling();
            }
        }

        // jump animation
        else if (Input.GetKeyDown(KeyCode.Space) && playerMovement.isGrounded && hasJumped == false)
        {
            Jump();
        }

        // land animation
        else if ((hasJumped == true && playerMovement.isGrounded) || (isFalling == true && playerMovement.isGrounded))
        {
            anim.SetInteger("LandPar", 1);
            anim.SetInteger("JumpPar", 0);
            hasJumped = false;
            isFalling = false;
        }

        // else back to idle animation
        else
        {
            anim.SetInteger("AnimationPar", 0);
            anim.SetInteger("RunPar", 0);
        }
        Debug.Log(playerMovement.isGrounded);
        Debug.Log(controller.velocity.y);
    }

    // jump function
    void Jump()
    {
        hasJumped = true;
        anim.SetInteger("LandPar", 0);
        anim.SetInteger("JumpPar", 1);
    }

    // fall function
    void Falling()
    {
        anim.SetInteger("AnimationPar", 0);
        anim.SetInteger("RunPar", 0);
        anim.SetInteger("IsFalling", 1);
        isFalling = true;
    }
}
