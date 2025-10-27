using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    private Animator animator;
    // private UnityEngine.CharacterController controller;
    void Start()
    {
        animator = GetComponent<Animator>();
        // controller = GetComponent<Animator>();
        // GetComponent<UnityEngine.CharacterController>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKey(KeyCode.Space) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        if (Input.GetKeyUp(KeyCode.Space) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isGrounded", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isGrounded", true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }
    }
}