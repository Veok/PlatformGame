using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float moveSpeed = 2.5f;
    private float _startPositionX;
    private SpriteRenderer mySpriteRenderer;
    private bool _isMovingRight = true;
    public float XMin = 2.2f;
    public float XMax = 5.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMovingRight)
        {
            if (transform.position.x < _startPositionX + XMax)
            {
                MoveRight();
                mySpriteRenderer.flipX = false;
            }
            else
            {
                _isMovingRight = false;
                MoveLeft();
                mySpriteRenderer.flipX = true;
            }
        }
        else
        {
            if (transform.position.x > _startPositionX - XMin)
            {
                MoveLeft();
                mySpriteRenderer.flipX = true;
            }
            else
            {
                _isMovingRight = true;
                MoveRight();
                mySpriteRenderer.flipX = false;
            }
        }
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPositionX = transform.position.x;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void MoveLeft()
    {
        if (_rigidbody.velocity.x > moveSpeed)
        {
            _rigidbody.velocity = new Vector2(-moveSpeed, _rigidbody.velocity.y);
            _rigidbody.AddForce(Vector2.left * 0.6F, ForceMode2D.Impulse);
        }
    }

    private void MoveRight()
    {
        if (_rigidbody.velocity.x < moveSpeed)
        {
            _rigidbody.velocity = new Vector2(moveSpeed, _rigidbody.velocity.y);
            _rigidbody.AddForce(Vector2.right * 0.6F, ForceMode2D.Impulse);
        }
    }

}
