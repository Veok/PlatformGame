using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float jumpForce = 1f;
    public new Rigidbody2D rigidbody2D;
    public LayerMask groundLayer;
    public Animator animator;

    private bool _isWalking;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        _isWalking = false;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            _isWalking = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            _isWalking = true;
        }

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", _isWalking);
    }

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log("jumping");
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer.value);
    }
}