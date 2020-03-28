using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerLevel1 : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float jumpForce = 1.8f;
    public new Rigidbody2D rigidbody2D;
    public LayerMask groundLayer;
    public Animator animator;
    public GameObject youWinPanel;
    public GameObject keyPanel;
    public GameObject startGamePanel;

    private bool _isWalking;
    private bool _isFacingRight;
    private float _killOffset = 0.2f;
    private Vector2 _startPosition;
    private int _lives = 3;
    private bool _foundKey;

    // Start is called before the first frame update
    private void Start()
    {
        _isFacingRight = true;
        rigidbody2D.freezeRotation = true;
        youWinPanel.SetActive(false);
        keyPanel.SetActive(false);
        startGamePanel.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.GS_GAME)
        {
            startGamePanel.SetActive(false);
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

            if (FoundChest() && _foundKey)
            {
                YouWin();
            }

            if (FoundChest() && !_foundKey)
            {
                KeyNotFoundPanel();
            }

            animator.SetBool("isGrounded", IsGrounded());
            animator.SetBool("isWalking", _isWalking);
        }
    }


    private bool FoundChest()
    {
        return Math.Abs(transform.position.x - 58.56) < 0.1 && Math.Abs(transform.position.y - (-12.11)) < 0.01;
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log("jumping");
        _startPosition = transform.position;
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
            other.gameObject.SetActive(false);
            GameManager.instance.AddCoins();
        }

        if (other.CompareTag("Heart"))
        {
            _lives++;
            GameManager.instance.AddHeart();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Key"))
        {
            _foundKey = true;
            GameManager.instance.FoundKey();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy") && IsGrounded())
        {
            _lives--;
            GameManager.instance.RemoveHeart();
            if (_lives == 0)
            {
                Debug.Log("GameOver");
                transform.position = _startPosition;
                GameManager.instance.GameOver();
            }
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
        return Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer.value);
    }

    private void YouWin()
    {
        youWinPanel.SetActive(true);
        GameManager.instance.LevelCompleted();
    }

    private async void KeyNotFoundPanel()
    {
        keyPanel.SetActive(true);
        await Task.Delay(1000);
        keyPanel.SetActive(false);
    }
}