﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _isFacingRight = true;
    private bool _isMovingRight = true;
    private bool _isWalking;
    private float _startPositionX;
    private Rigidbody2D _rigidbody;

    public float moveSpeed = 2.5f;
    public float XMin = 2.2f;
    public float XMax = 5.5f;
    public Animator animator;
    private float _killOffset = 0.2f;
    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPositionX = transform.position.x;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.transform.position.y > transform.position.y + _killOffset)
            {
                Debug.Log("Enemy is dead");
                GameManager.instance.EnemyKilledCounter();
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}