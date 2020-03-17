using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.Play("CoinRotation", 0, Random.Range(0.0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
    }
}