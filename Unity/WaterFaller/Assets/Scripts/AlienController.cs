using UnityEngine;
using Prime31;

public class AlienController : MonoBehaviour
{
    // Movement configuration
    public float Gravity = -15f;
    public float RunSpeed = 8f;

    private CharacterController2D _controller;
    private Animator _animator;
    private bool _facingRight = true;

    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _animator.speed = 6;
    }

    private void Update()
    {
        // grab current velocity to use as a base for all calculations
        var velocity = _controller.velocity;
        var grounded = _controller.isGrounded;

        if (grounded)
        {
            _animator.SetBool("Ground", true);
            velocity.y = 0;
        }

        // horizontal input
        var horiz = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat("Speed",  Mathf.Abs(horiz));

        if (horiz > 0)
        {
            velocity.x = RunSpeed;
            if(!_facingRight)
                Flip();
        }
        else if (horiz < 0)
        {
            velocity.x = -RunSpeed;
            if (_facingRight)
                Flip();
        }
        else
        {
            velocity.x = 0;
        }

        // jump input
        if (grounded && Input.GetButtonDown("Jump"))
        {
            var targetJumpHeight = 3f;
            velocity.y = Mathf.Sqrt(2f * targetJumpHeight * -Gravity);
            _animator.SetBool("Ground", false);
        }

        velocity.y += Gravity * Time.deltaTime;

        _controller.move(velocity * Time.deltaTime);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
