using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Parameters
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject attackZone;

    //Components
    private Rigidbody2D rb;
    private Animator animator;

    private float xInput;

    private float scaleX;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        scaleX = transform.localScale.x;    
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        SetAnimation();
        HandleFlip(xInput);
        HandleAttack();
    }

    private void HandleAttack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
        }
    }

    private void HandleFlip(float xInput)
    {
        if(xInput != 0)
        {
            transform.localScale = new Vector2(xInput * scaleX, transform.localScale.y);
        }
    }

    private void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void EnableAttackZone()
    {
        Debug.Log("Enable");
        attackZone.SetActive(true);
    }

    private void DiableAttackZone()
    {
        Debug.Log("Diable");
        attackZone.SetActive(false);
    }
}
