using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Parameters
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Collider2D attackZone;
    [SerializeField] private Collider2D strikeZone;

    [SerializeField] private float knockBackForce;
    [SerializeField] private Vector2 knockBackDir;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private Slider healthSlider;

    //Fireball
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float fireBallMoveSpeed;

    //Damage
    public int attackDamage;
    public int strikeDamage;
    public int fireballDamage;

    //Components
    private Rigidbody2D rb;
    private Animator animator;

    private float xInput;

    private float scaleX;
    private int facingDir;
    private bool isGrounded;

    private bool isBeingHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        scaleX = transform.localScale.x;
        facingDir = 1;

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        SetAnimation();
        HandleFlip(xInput);
        HandleAttack();
        HandleStrike();
        HandleCast();
    }

    private void HandleStrike()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("strike");
        }
    }

    private void FixedUpdate()
    {
        if (!isBeingHit)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void HandleCast()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("cast");
        }
    }

    private void ShootFireBall()
    {
        GameObject fireBallGameObject = Instantiate(fireBallPrefab, shootPosition.position, transform.rotation);

        Fireball fireball = fireBallGameObject.GetComponent<Fireball>();
        if (fireball != null)
        {
            fireball.SetUpFireball(this, fireBallMoveSpeed, facingDir);
        }

        Destroy(fireBallGameObject, 2f);
    }

    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.J))
        {
            animator.SetBool("attack", true);
        }
    }

    private void HandleFlip(float xInput)
    {
        if (xInput != 0)
        {
            transform.localScale = new Vector2(xInput * scaleX, transform.localScale.y);

            if (xInput * scaleX > 0)
            {
                facingDir = 1;
            }
            else
            {
                facingDir = -1;
            }
        }
    }

    private void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void OnBeingAttacked()
    {
        animator.SetTrigger("hit");
        isBeingHit = true;

        Vector2 realKnockbackDir = new Vector2(knockBackDir.x * transform.localScale.x > 0 ? -1 : 1, knockBackDir.y);

        rb.AddForce(realKnockbackDir * knockBackForce, ForceMode2D.Impulse);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            animator.SetBool("dead", true);
        }

        OnBeingAttacked();
    }

    private void EnableAttackZone()
    {
        attackZone.enabled = true;
    }

    private void DiableAttackZone()
    {
        animator.SetBool("attack", false);
        attackZone.enabled = false;
    }

    private void EnableStrikeZone()
    {
        strikeZone.enabled = true;
    }

    private void DisableStrikeZone()
    {
        strikeZone.enabled &= false;
    }

    private void EndBeingHit()
    {
        isBeingHit = false;
    }

    private void DeActiveGameObject()
    {
        gameObject.SetActive(false);
    }
}
