using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerLevel1 : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float jumpForce = 1.5f;
    public new Rigidbody2D rigidbody2D;
    public LayerMask groundLayer;
    public Animator animator;
    public int score;
    public Text youWinText;
    public GameObject youWinPanel;

    private bool _isWalking;
    private bool _isFacingRight;

    // Start is called before the first frame update
    private void Start()
    {
        _isFacingRight = true;
        rigidbody2D.freezeRotation = true;
        youWinPanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        _isWalking = false;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!_isFacingRight)
            {
                Flip();
            }

            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            _isWalking = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (_isFacingRight)
            {
                Flip();
            }

            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            _isWalking = true;
        }

        if (Math.Abs(transform.position.x - 48.1) < 0.1 && Math.Abs(transform.position.y - (-8.0)) < 0.01)
        {
            YouWin();
        }

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", _isWalking);
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log("jumping");
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 1;
            Debug.Log($"Score {score}");
            other.gameObject.SetActive(false);
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer.value);
    }

    private void YouWin()
    {
        youWinPanel.SetActive(true);
    }
}